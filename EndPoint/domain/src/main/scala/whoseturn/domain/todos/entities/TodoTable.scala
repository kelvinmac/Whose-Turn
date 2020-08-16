package whoseturn.domain.todos.entities

import java.util.UUID

import com.datastax.driver.core.Session
import com.outworkers.phantom.dsl._

import scala.concurrent.Future

trait TodosTable extends Table[TodosTable, TodoEntity] {

  object id extends UUIDColumn with PartitionKey

  object createdOn extends DateTimeColumn

  object createdBy extends UUIDColumn

  object dueOn extends DateTimeColumn

  object task extends StringColumn

  object assignedTo extends ListColumn[UUID]

  object isCompleted extends BooleanColumn

  object completedOn extends OptionalDateTimeColumn

  object version extends StringColumn

}

abstract class TodosDatabase(cassandraSession: Session) extends TodosTable {
  val todosCassandraKeySpace = "todos"

  override implicit def space: KeySpace = KeySpace(todosCassandraKeySpace)

  override implicit def session: Session = cassandraSession

  val cassandraTodoTableName = "todos_v1"

  override def tableName: String = cassandraTodoTableName

  override def fromRow(row: Row): TodoEntity =
    TodoEntity(
      id = id(row),
      createdOn = createdOn(row),
      createdBy = createdBy(row),
      dueOn = dueOn(row),
      task = task(row),
      assignedTo = assignedTo(row),
      isCompleted = isCompleted(row),
      completedOn = completedOn(row),
      version = version(row)
    )

  def writeRecordToCassandra(entity: TodoEntity): Future[ResultSet] = {
    insert
      .value(_.id, entity.id)
      .value(_.createdOn, entity.createdOn)
      .value(_.createdBy, entity.createdBy)
      .value(_.dueOn, entity.dueOn)
      .value(_.task, entity.task)
      .value(_.assignedTo, entity.assignedTo)
      .value(_.isCompleted, entity.isCompleted)
      .value(_.completedOn, entity.completedOn)
      .value(_.version, entity.version)
      .future()
  }

  def readRecordByIdFromCassandra(uuid: UUID): Future[Option[TodoEntity]] = {
    select.where(_.id eqs uuid).one()
  }

}

object TodosDatabase {}
