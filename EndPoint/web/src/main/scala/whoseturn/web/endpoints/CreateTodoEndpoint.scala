package whoseturn.web.endpoints

import cats.data.NonEmptyList
import cats.effect.IO
import com.typesafe.scalalogging.{LazyLogging, Logger}
import io.circe.generic.decoding.DerivedDecoder.deriveDecoder
import io.circe.generic.semiauto.deriveEncoder
import io.circe.syntax._
import io.circe.{Encoder, Json}
import io.finch.circe.decodeCirce
import io.finch.{BadRequest, Endpoint, Ok, ValidationRule}
import org.joda.time.DateTime
import org.slf4j.LoggerFactory
import whoseturn.domain.CustomEncoders._
import whoseturn.domain.TodoRepository
import whoseturn.domain.todos.{CreateTodoRequestBody, Todo, TodoValidationFailure, WhoseTurnTodoFactory}
import whoseturn.web.endpoints.CreateTodoEndpoint._
import whoseturn.domain.CustomEncoders._
import scala.concurrent.{Await, Future}
import scala.concurrent.duration.Duration
import scala.concurrent.ExecutionContext.Implicits.global
import scala.util.{Failure, Success, Try}

class CreateTodoEndpoint(todoRepo: TodoRepository) extends Endpoint.Module[IO] with LazyLogging {

  private def body: Endpoint[IO, CreateTodoRequestBody] = {
    jsonBody[CreateTodoRequestBody]
      .should(haveValidDueDate)
  }

  def endpoint: Endpoint[IO, Json] = {
    post("todos" :: body) { (newTodo: CreateTodoRequestBody) =>
      {
        logger.info(s"Creating todo for [assignedTo=${newTodo.assignedTo}]")

        IO(
          WhoseTurnTodoFactory
            .fromCreateTodoRequestBody(newTodo)
            .fold(e => BadRequest(createError(newTodo, e)), t => Ok(submit(t).asJson))
        )
      }
    }
  }

  def createError(newTodo: CreateTodoRequestBody, validationErrors: NonEmptyList[TodoValidationFailure]): Exception = {
    logger.warn(s"There were validation errors while creating Todo for user ${newTodo.assignedTo}")
    new Exception("There was an error")
  }

  def submit(todo: Todo): Todo = {
    for {
      a <- submitToKafka(todo)
      b <- todoRepo.write(todo)
    } yield (a, b)

    todo
  }

  def submitToKafka(todo: Todo): Future[Unit] = Future()
}

object CreateTodoEndpoint {
  implicit val encodeTodo: Encoder[Todo] = deriveEncoder[Todo]

  def haveValidDueDate: ValidationRule[CreateTodoRequestBody] = {
    val logger = Logger(LoggerFactory.getLogger(getClass.getName))

    ValidationRule[CreateTodoRequestBody]("have valid dueOn") { body =>
      Try(DateTime.parse(body.dueOn)) match {
        case Failure(exception) =>
          logger.warn(s"Could not parse [date=${body.dueOn}] with [exception=${exception.getMessage}]")
          false
        case _ => true
      }
    }
  }
}
