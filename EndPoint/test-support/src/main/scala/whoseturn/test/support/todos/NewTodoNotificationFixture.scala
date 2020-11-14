package whoseturn.test.support.todos

import java.util.UUID

import org.joda.time.DateTime
import whoseturn.domain.kafka.todo._

trait NewTodoNotificationFixture {
  lazy val defaultPayload: Payload = Payload(
    todoId = UUID.randomUUID().toString,
    namespace = "TODO_NAMESPACE",
    createdOn = DateTime.now().toString,
    assignedTo = UUID.randomUUID().toString
  )

  lazy val defaultTodoNotification: NewTodoNotification = NewTodoNotification(
    companion = Companion(triggerBy = "TEST"),
    payload = defaultPayload
  )
}
