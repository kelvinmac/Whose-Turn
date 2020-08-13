package whoseturn.domain

import java.util.UUID

import cats.data._
import whoseturn.domain.todos.{NewTodo, Todo}

import scala.concurrent.Future

trait TodoFactory {
  def create(newTodo: NewTodo): Validated[String, Todo]
}

trait TodoRepository {
  def addTodo(newTodo: Todo): Future[Todo]
  def read(todoId: UUID): Future[Todo]
  def update(feedItems: Todo): Future[Unit]
}
