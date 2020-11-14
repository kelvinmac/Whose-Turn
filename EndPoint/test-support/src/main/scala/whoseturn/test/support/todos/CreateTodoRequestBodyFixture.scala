package whoseturn.test.support.todos

import DomainFixtures._
import whoseturn.domain.kafka.todo.NewTodo
import whoseturn.domain.todos.CreateTodoRequestBody

trait CreateTodoRequestBodyFixture {
  val defaultCreateTodoRequestBody: CreateTodoRequestBody = CreateTodoRequestBody(
    dueOn = defaultDueOn.toString(),
    task = defaultTodoTask,
    assignedTo = defaultAssignedTo.head
  )
}
