package whoseturn.web.endpoints

import java.util.UUID

import com.twitter.finagle.http.{MediaType, Request, RequestBuilder, Response, Status}
import com.twitter.io.Buf
import com.twitter.util
import io.circe
import io.circe.generic.semiauto.{deriveDecoder, deriveEncoder}
import io.circe.syntax.EncoderOps
import io.circe.{Decoder, Encoder, Json, jawn}
import io.finch.circe._
import org.scalatest.matchers.must.Matchers
import org.scalatest.prop.TableDrivenPropertyChecks._
import org.scalatest.prop.Tables.Table
import org.scalatest.wordspec.AnyWordSpec
import whoseturn.cassandra.CassandraSupport
import whoseturn.domain.TodoRepository
import whoseturn.domain.todos.{CreateTodoRequestBody, Todo, TodoFeedItemProducer}
import whoseturn.todos.{NewTodoFixture, TodoFixture}
import whoseturn.web.endpoints.CreateTodoEndpointSpec._
import whoseturn.web.errors.{ErrorHandler, ErrorResponse}
import whoseturn.web.support.WebFixture

import scala.concurrent.ExecutionContext.Implicits.global
import scala.concurrent.{Future, Promise}

class CreateTodoEndpointSpec
    extends AnyWordSpec
    with Matchers
    with NewTodoFixture
    with TodoFixture
    with WebFixture
    with CassandraSupport {

  private val stubbedTodoRepo = new TodoRepository {
    override def write(newTodo: Todo): Future[Unit] = Future()

    override def readById(todoId: UUID): Future[Option[Todo]] = Future(Some(defaultTodo))

    override def update(feedItems: Todo): Future[Unit] = Future()
  }

  private val stubbedTodoFeedProducer = new TodoFeedItemProducer {}

  implicit def encodeExceptionCirce: Encoder[Exception] = ErrorHandler.encodeExceptionCirce

  private val service = new CreateTodoEndpoint(stubbedTodoRepo, stubbedTodoFeedProducer).endpoint.toService

  "CreateTodoEndpointSpec.endpoint" should {
    "return 400 bad request" when {
      "validation errors occur" in {
        val baseRequestBody = createTodoRequestBody.asJson

        val invalidInputs = Table(
          ("Field", "Modified Value"),
          ("dueOn", setJsonString("INVALID_DATE")),
          ("assignedTo", setJsonString("INVALID_UUID"))
        )

        def validateBadRequest(response: Response, shouldIncludeField: String) = {
          response.status mustBe Status.BadRequest

          val error = jawn.decode[ErrorResponse](response.contentString).right.get
          error.message must include(shouldIncludeField)
        }

        forAll(invalidInputs) { (field, malformedValue) =>
          withClue(s"Sending request with malformed [field=$field]") {
            val body     = modifyJsonObject(baseRequestBody, field, malformedValue)
            val request  = buildRequest(body)
            val response = service.apply(request).asScala.futureValue

            validateBadRequest(response, field)
          }
        }
      }
    }

    "return 200 Success" when {

      "provided with minimum valid body" in {
        val minimumBody = createTodoRequestBody.copy()
        val body        = minimumBody.asJson
        val request     = buildRequest(body)
        val response    = service.apply(request).asScala.futureValue

        response.status mustBe Status.Ok

        noException should be thrownBy
          decodeTodo.decodeJson(circe.jawn.parse(response.contentString).right.get).right.get
      }

      "provided with valid body" in {
        val body     = createTodoRequestBody.asJson
        val request  = buildRequest(body)
        val response = service.apply(request).asScala.futureValue

        response.status mustBe Status.Ok

        noException should be thrownBy
          decodeTodo.decodeJson(circe.jawn.parse(response.contentString).right.get).right.get
      }
    }
  }
}

object CreateTodoEndpointSpec { // Needed to decode Todo

  import whoseturn.domain.CustomEncoders._
  implicit val encodeCreateTodoRequestBody: Encoder[CreateTodoRequestBody] = deriveEncoder[CreateTodoRequestBody]
  implicit val decodeTodo: Decoder[Todo]                                   = deriveDecoder[Todo]

  def buildRequest(body: Json): Request = {
    RequestBuilder
      .create()
      .url("http://localhost:8080/todos")
      .addHeader("Content-Type", MediaType.Json)
      .buildPost(Buf.Utf8(body.noSpaces))
  }

  implicit class RichTwitterFuture[A](val tf: util.Future[A]) extends AnyVal {
    def asScala: Future[A] = {
      val promise: Promise[A] = Promise()
      tf.respond {
        case util.Return(value) =>
          promise.success(value)
          ()
        case util.Throw(exception) =>
          promise.failure(exception)
          ()
      }
      promise.future
    }
  }

}
