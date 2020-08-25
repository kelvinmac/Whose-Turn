package whoseturn.domain.todos.kafka.messages

import java.util.UUID

import org.joda.time.DateTime

case class NewTodo(
    todoId: UUID,
    createdOn: DateTime,
    assignedTo: UUID
)
