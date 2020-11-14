package whoseturn.domain.todos

import java.util.UUID

import com.datastax.driver.core.Session
import com.outworkers.phantom.dsl.context
import whoseturn.domain.Retry.RetryConfig
import whoseturn.domain.TodoRepository
import whoseturn.domain.todos.entities.TodosDatabase
import whoseturn.domain.Retry.Implicits.defaultFutureRetry
import whoseturn.domain.Retry.Retry._

import scala.concurrent.Future

class WhoseTurnTodoRepository(session: Session)(implicit retryConfig: RetryConfig)
    extends TodosDatabase(session)
    with TodoRepository {
  def write(newTodo: Todo): Future[Unit] = {
    val todoEntity = WhoseTurnTodoFactory.toTodoEntity(newTodo)
    val taskName   = s"Creating new Todo with [id=${todoEntity.id} in ${getClass.getName}"

    withRetry(taskName) { () =>
      writeRecordToCassandra(todoEntity)
    }.map(_ => Future())
  }

  def update(feedItems: Todo): Future[Unit] = ???

  def readById(todoId: UUID): Future[Option[Todo]] = {
    val taskName = s"Reading Todo with [id=$todoId] in ${getClass.getName}"

    withRetry(taskName) { () =>
      readRecordByIdFromCassandra(todoId)
    } map {
      case None    => None
      case Some(t) => Some(WhoseTurnTodoFactory.fromTodoEntity(t))
    }
  }
}
