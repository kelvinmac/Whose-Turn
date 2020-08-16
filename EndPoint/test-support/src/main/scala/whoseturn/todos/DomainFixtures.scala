package whoseturn.todos

import java.util.UUID
import org.joda.time.{DateTime, DateTimeZone}

object DomainFixtures {
  val defaultTodoTask: String       = "Test this todo"
  val defaultAssignedTo: List[UUID] = List(UUID.randomUUID())
  val defaultCreatedBy: UUID        = UUID.randomUUID()
  val defaultCreatedOn: DateTime    = new DateTime(DateTimeZone.UTC)
  val defaultDueOn: DateTime        = defaultCreatedOn.plusDays(1)
  val defaultTodoId: UUID           = UUID.randomUUID()
}
