package whoseturn.test.support.todos

import whoseturn.domain.kafka.todo.NewTodo
import whoseturn.test.support.todos.DomainFixtures._
trait NewTodoFixture {
  val defaultNewTodo: NewTodo =
    NewTodo(todoId = defaultTodoId, createdOn = defaultCreatedOn, assignedTo = defaultAssignedTo.head)
}
