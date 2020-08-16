package whoseturn.web.support

import io.circe.{Json, JsonLong, JsonNumber}
import whoseturn.domain.todos.CreateTodoRequestBody
import whoseturn.todos.DomainFixtures._

trait WebFixture {
  val createTodoRequestBody: CreateTodoRequestBody = CreateTodoRequestBody(
    dueOn = defaultDueOn.toString(),
    task = defaultTodoTask,
    assignedTo = defaultAssignedTo.head
  )

  def setJsonString(s: String): Json => Json = _.mapString(_ => s)

  def setJsonInt(i: Int): Json => Json = _.mapNumber(_ => JsonNumber.fromIntegralStringUnsafe(i.toString))

  def modifyJsonObject(jsonObj: Json, field: String, modifier: Json => Json): Json = {
    jsonObj.hcursor
      .downField(field)
      .withFocus(modifier)
      .top
      .get
  }

}
