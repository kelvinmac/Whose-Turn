package whoseturn.domain

import whoseturn.domain.Todos.{NewTodo, Todo}

trait TodoRepository {
  def addNewTodo(newTodo: Todo): Todo
}

trait UserRepository {}

trait TodoFactory {
  def create(newTodo: NewTodo): Todo
}
