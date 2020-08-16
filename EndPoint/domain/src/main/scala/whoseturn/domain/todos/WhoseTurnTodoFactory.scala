package whoseturn.domain.todos
import java.util.UUID

import cats._
import cats.data._
import cats.implicits._
import org.joda.time.DateTime

import scala.util.Try
import whoseturn.domain.todos.TodoValidationFailure._
import whoseturn.domain.todos.entities.TodoEntity

object WhoseTurnTodoFactory {
  def fromCreateTodoRequestBody(newTodo: CreateTodoRequestBody): ValidatedNel[TodoValidationFailure, Todo] = {
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

  private def date(date: String): ValidatedNel[TodoValidationFailure, DateTime] =
    Try(DateTime.parse(date)).toOption.toValidNel(ParseError("date"))

  private def user(id: UUID): ValidatedNel[TodoValidationFailure, UUID] =
    Some(id).toValidNel(InvalidUserId(id.toString))

  private def task(description: String): ValidatedNel[TodoValidationFailure, String] =
    someNonEmptyStr(description).toValidNel(InvalidDescription())

  private def someNonEmptyStr(str: String): Option[String] =
    str match {
      case _ if str.isEmpty => None
      case s                => Some(s)
    }

  def toTodoEntity(todo: Todo): TodoEntity = {
    TodoEntity(
      id = todo.id,
      createdOn = todo.createdOn,
      createdBy = todo.createdBy,
      dueOn = todo.dueOn,
      task = todo.task,
      assignedTo = todo.assignedTo,
      isCompleted = todo.isCompleted,
      completedOn = todo.completedOn,
      version = "" // TODO, read version from config
    )
  }

  def fromTodoEntity(todoEntity: TodoEntity): Todo = {
    Todo(
      id = todoEntity.id,
      createdOn = todoEntity.createdOn,
      createdBy = todoEntity.createdBy,
      dueOn = todoEntity.dueOn,
      task = todoEntity.task,
      assignedTo = todoEntity.assignedTo,
      isCompleted = todoEntity.isCompleted,
      completedOn = todoEntity.completedOn
    )
  }
}
