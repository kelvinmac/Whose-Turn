package whoseturn.web

import org.scalatest.matchers.must.Matchers
import org.scalatest.wordspec.AnyWordSpec
import whoseturn.todos.NewTodoFixture

class CreateTodoEndpointSpec extends AnyWordSpec with Matchers with NewTodoFixture {
  "CreateTodoEndpointSpec.endpoint" should {
    "return bad request" when {
      "validation errors occur" in {}
    }
  }
}
