package whoseturn.todos

import whoseturn.domain.todos.CreateTodoRequestBody
import whoseturn.todos.DomainFixtures._

trait NewTodoFixture {
  val defaultNewTodo: CreateTodoRequestBody = CreateTodoRequestBody(
    dueOn = defaultDueOn.toString(),
    task = defaultTodoTask,
    assignedTo = defaultAssignedTo.head
  )
}
