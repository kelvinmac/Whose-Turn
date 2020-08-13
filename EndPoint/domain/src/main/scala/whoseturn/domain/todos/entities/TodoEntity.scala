package whoseturn.domain.todos.entities

import java.util.UUID

import com.datastax.driver.mapping.annotations.{PartitionKey, Table}
import org.joda.time.DateTime

@Table(keyspace = "todos", name = "todo")
final case class TodoEntity(
    @PartitionKey id: UUID,
    version: String,
    createdOn: DateTime,
    createdBy: UUID,
    dueOn: DateTime,
    task: String,
    assignedTo: List[UUID],
    isCompleted: Boolean,
    completedOn: Option[DateTime]
)
