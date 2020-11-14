package whoseturn.test.support.todos

import DomainFixtures._
import whoseturn.domain.todos.Todo

trait TodoFixture {
  val defaultTodo: Todo = Todo(
    id = defaultTodoId,
    createdOn = defaultCreatedOn,
    createdBy = defaultCreatedBy,
    dueOn = defaultDueOn,
    task = defaultTodoTask,
    assignedTo = defaultAssignedTo,
    isCompleted = false,
    completedOn = None
  )
}
