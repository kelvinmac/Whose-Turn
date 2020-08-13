package whoseturn.todos

import java.util.UUID

import cats.data.NonEmptyList
import org.joda.time.DateTime
import whoseturn.domain.todos.NewTodo

trait NewTodoFixture {
  val defaultTodoTask: String = "Test this todo"

  val defaultAssignedTo: List[UUID] = List(UUID.randomUUID())

  val defaultCreatedOn: DateTime = DateTime.now().plusDays(1)

  val defaultDueOn: DateTime = DateTime.now()

  val defaultNewTodo: NewTodo = NewTodo(
    dueOn = defaultDueOn.toString(),
    task = defaultTodoTask,
    assignedTo = defaultAssignedTo.head
  )
}
