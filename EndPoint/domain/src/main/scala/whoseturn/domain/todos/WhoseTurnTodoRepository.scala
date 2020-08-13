package whoseturn.domain.todos

import java.util.UUID

import com.datastax.driver.core.Session
import whoseturn.domain.{RetryConfig, TodoRepository}

import scala.concurrent.Future

class WhoseTurnTodoRepository(retryConfig: RetryConfig, session: Session) extends TodoRepository {
  def addTodo(newTodo: Todo): Future[Todo] = ???

  def update(feedItems: Todo): Future[Unit] = ???

  def read(todoId: UUID): Future[Todo] = ???
}
