package whoseturn.domain.todos
import java.util.UUID

import cats._
import cats.data._
import cats.implicits._
import org.joda.time.DateTime
import scala.util.Try

import whoseturn.domain.todos.TodoValidationFailure._

object WhoseTurnTodoFactory {
  def create(newTodo: NewTodo): ValidatedNel[TodoValidationFailure, Todo] = {
    (
      date(newTodo.dueOn),
      user(newTodo.assignedTo),
      task(newTodo.task)
    ).mapN((dueDate, assignedUser, task) => {
      Todo(
        id = UUID.randomUUID,
        createdOn = DateTime.now(),
        createdBy = UUID.randomUUID(),
        dueOn = dueDate,
        task = task,
        assignedTo = List(assignedUser),
        isCompleted = false,
        completedOn = None
      )
    })
  }

  def date(date: String): ValidatedNel[TodoValidationFailure, DateTime] =
    Try(DateTime.parse(date)).toOption.toValidNel(ParseError("date"))

  def user(id: UUID): ValidatedNel[TodoValidationFailure, UUID] = Some(id).toValidNel(InvalidUserId(id.toString))

  def task(description: String): ValidatedNel[TodoValidationFailure, String] =
    someNonEmptyStr(description).toValidNel(InvalidDescription())

  def someNonEmptyStr(str: String): Option[String] =
    str match {
      case _ if str.isEmpty => None
      case s                => Some(s)
    }
}
