package whoseturn.todos

import whoseturn.domain.todos.Todo
import whoseturn.todos.DomainFixtures._

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
