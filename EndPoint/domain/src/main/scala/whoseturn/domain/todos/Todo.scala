package whoseturn.domain.todos

import java.util.UUID

import cats.data._
import org.joda.time.DateTime

final case class Todo(
    id: UUID,
    createdOn: DateTime,
    createdBy: UUID,
    dueOn: DateTime,
    task: String,
    assignedTo: List[UUID],
    isCompleted: Boolean,
    completedOn: Option[DateTime]
)
