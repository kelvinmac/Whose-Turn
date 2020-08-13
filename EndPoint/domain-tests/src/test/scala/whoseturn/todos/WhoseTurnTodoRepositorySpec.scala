package whoseturn.todos

import org.scalatest.matchers.must.Matchers
import org.scalatest.wordspec.AnyWordSpec
import whoseturn.cassandra.CassandraSupport
import whoseturn.domain.RetryConfig
import whoseturn.domain.todos.WhoseTurnTodoRepository

import scala.concurrent.duration.DurationInt

class WhoseTurnTodoRepositorySpec extends AnyWordSpec with Matchers with NewTodoFixture with CassandraSupport {

  private def withCache[A](fn: WhoseTurnTodoRepository => A): A = withCassandraSession("/todos-test-schema.cql") {
    session =>
      logger.info("cassandra ready")
      fn(new WhoseTurnTodoRepository(RetryConfig(1, 10 seconds), session))
  }

  "WhoseTurnTodoRepository.addTodo" should {
    "add new entity" when {
      "Todo entity valid" in withCache { todo =>
      }
    }
  }

}
