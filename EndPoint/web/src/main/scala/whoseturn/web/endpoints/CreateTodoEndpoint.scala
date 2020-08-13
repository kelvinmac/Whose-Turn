package whoseturn.web.endpoints

import cats.data.NonEmptyList
import cats.effect.IO
import com.typesafe.scalalogging.LazyLogging
import io.circe.generic.decoding.DerivedDecoder.deriveDecoder
import io.circe.generic.semiauto.deriveEncoder
import io.circe.syntax._
import io.circe.{Encoder, Json}
import io.finch.circe.decodeCirce
import io.finch.{BadRequest, Endpoint, Ok, ValidationRule}
import whoseturn.domain.CustomEncoders._
import whoseturn.domain.TodoRepository
import whoseturn.domain.todos.{NewTodo, Todo, TodoValidationFailure, WhoseTurnTodoFactory}
import whoseturn.web.endpoints.CreateTodoEndpoint._

import scala.concurrent.Await
import scala.concurrent.duration.Duration

class CreateTodoEndpoint(todoRepo: TodoRepository) extends Endpoint.Module[IO] with LazyLogging {

  private def body: Endpoint[IO, NewTodo] = {
    jsonBody[NewTodo]
      .should(haveValidDueDate)
  }

  def endpoint: Endpoint[IO, Json] = {
    post("todos" :: body) { (newTodo: NewTodo) =>
      {
        logger.info(s"Creating todo for [assignedTo=${newTodo.assignedTo}]")

        IO(
          WhoseTurnTodoFactory
            .create(newTodo)
            .fold(e => BadRequest(createError(newTodo, e)), t => Ok(submit(t).asJson))
        )
      }
    }
  }

  def createError(newTodo: NewTodo, validationErrors: NonEmptyList[TodoValidationFailure]): Exception = {
    logger.warn(s"There were validation errors while creating Todo for user ${newTodo.assignedTo}")
    new Exception("There was an error")
  }

  def submit(todo: Todo): Todo = {
    submitToKafka(todo)
    Await.result(todoRepo.addTodo(todo), Duration.Inf)
  }

  def submitToKafka(todo: Todo): Unit = {}
}

object CreateTodoEndpoint {
  implicit val encodeTodo: Encoder[Todo] = deriveEncoder[Todo]

  def haveValidDueDate = ValidationRule[NewTodo]("have valid due date")(b => false)
}
