package whoseturn.domain

import java.util.UUID

import cats.data._
import whoseturn.domain.todos.{CreateTodoRequestBody, Todo}
import scala.concurrent.Future

trait TodoFactory {
  def create(newTodo: CreateTodoRequestBody): Validated[String, Todo]
}

trait TodoRepository {
  def write(newTodo: Todo): Future[Unit]

  def readById(todoId: UUID): Future[Option[Todo]]

  def update(feedItems: Todo): Future[Unit]
}
