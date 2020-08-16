package whoseturn.domain.todos.entities

import java.util.UUID

import org.joda.time.DateTime

final case class TodoEntity(
    id: UUID,
    createdOn: DateTime,
    createdBy: UUID,
    dueOn: DateTime,
    task: String,
    assignedTo: List[UUID],
    isCompleted: Boolean,
    completedOn: Option[DateTime],
    version: String
)
