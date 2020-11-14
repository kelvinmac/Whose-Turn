package whoseturn.todos

import java.util.UUID

import org.scalatest.matchers.must.Matchers
import org.scalatest.wordspec.AnyWordSpec
import whoseturn.domain.todos.TodoValidationFailure.{InvalidDescription, ParseError}
import whoseturn.domain.todos.{CreateTodoRequestBody, Todo, WhoseTurnTodoFactory}
import whoseturn.test.support.todos.DomainFixtures._
import whoseturn.test.support.todos.CreateTodoRequestBodyFixture

class WhoseTurnTodoFactorySpec extends AnyWordSpec with Matchers with CreateTodoRequestBodyFixture {

  import WhoseTurnTodoFactorySpec._

  "WhoseTurnTodoFactory.create" when {

    "NewTodo instance is valid" should {
      "create valid Todo from new todo" in {
        val buildResult = WhoseTurnTodoFactory.fromCreateTodoRequestBody(defaultCreateTodoRequestBody).toEither

        buildResult.isRight mustBe true

        val todo = buildResult.right.get

        todo.dueOn.toString() mustEqual defaultDueOn.toString()
        todo.assignedTo mustEqual defaultAssignedTo
        todo.task mustEqual defaultTodoTask
      }
    }

    "NewTodo instance invalid" should {
      "return validation errors" in {
        val buildResult = WhoseTurnTodoFactory
          .fromCreateTodoRequestBody(
            CreateTodoRequestBody(
              dueOn = "INVALID_DATE",
              task = "",
              assignedTo = UUID.randomUUID()
            )
          )
          .toEither

        buildResult.isRight mustBe false

        val validationErrors = buildResult.left.get.toList
        validationErrors must contain(ParseError("date"))
        validationErrors must contain(InvalidDescription())
      }
    }
  }
}

object WhoseTurnTodoFactorySpec {}
