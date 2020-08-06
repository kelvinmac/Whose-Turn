package whoseturn.web.endpoints

import cats.effect.IO
import io.finch.{Endpoint, ValidationRule}
import io.finch.circe.decodeCirce
import com.twitter.finagle.http.Status
import com.twitter.finagle.http.Response
import whoseturn.web.todos.Implicits._
import whoseturn.domain.Todos.{NewTodo, Todo}
import CreateTodoEndpoint._
import whoseturn.domain.TodoRepository

class CreateTodoEndpoint(todoRepo: TodoRepository) extends Endpoint.Module[IO] {

  private def body: Endpoint[IO, NewTodo] = {
    jsonBody[NewTodo]
      .should(haveValidDueData)
  }

  def endpoint: Endpoint[IO, Response] = {
    post("todos" :: body) { (body: NewTodo) =>
      {
        todoRepo.addNewTodo(makeTodoFrom(body))
        IO(Response(Status.Ok))
      }
    }
  }
}

object CreateTodoEndpoint {
  def haveValidDueData = ValidationRule[NewTodo]("have valid due date")(b => true)

  def makeTodoFrom(newTodo: NewTodo): Todo = {
    Todo(
      )
  }
}
