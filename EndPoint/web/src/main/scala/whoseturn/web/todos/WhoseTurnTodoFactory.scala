package whoseturn.web.todos

import whoseturn.domain.TodoFactory
import whoseturn.domain.Todos.{NewTodo, Todo}

class WhoseTurnTodoFactory extends TodoFactory {
  def create(newTodo: NewTodo): Todo = {}
}
