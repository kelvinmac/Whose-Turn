package whoseturn.domain.todos

import java.util.UUID

final case class NewTodo(dueOn: String, task: String, assignedTo: UUID)
