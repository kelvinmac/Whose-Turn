package whoseturn.domain.todos

import java.util.UUID

final case class CreateTodoRequestBody(dueOn: String, task: String, assignedTo: UUID)
