package whoseturn.domain.kafka.todo

import java.util.UUID

import org.joda.time.DateTime

case class NewTodo(
    todoId: UUID,
    createdOn: DateTime,
    assignedTo: UUID
)
