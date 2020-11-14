package whoseturn.domain.kafka.todo

final case class NewTodoNotification(companion: Companion, payload: Payload)

final case class Companion(triggerBy: String)
final case class Payload(todoId: String, namespace: String, createdOn: String, assignedTo: String)
