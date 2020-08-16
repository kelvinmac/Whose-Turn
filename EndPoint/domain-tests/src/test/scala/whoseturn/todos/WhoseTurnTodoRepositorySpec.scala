package whoseturn.todos

import java.util.UUID

import com.datastax.driver.core.Session
import com.outworkers.phantom.connectors.KeySpace
import org.scalatest.concurrent.ScalaFutures.convertScalaFuture
import org.scalatest.matchers.must.Matchers
import org.scalatest.wordspec.AnyWordSpec
import whoseturn.cassandra.CassandraSupport
import whoseturn.domain.Retry.Implicits._
import whoseturn.domain.todos.entities.{TodoEntity, TodosDatabase, TodosTable}
import whoseturn.domain.todos.{WhoseTurnTodoFactory, WhoseTurnTodoRepository}
import whoseturn.todos.WhoseTurnTodoRepositorySpec.{addTodosToCassandra, readTodoFromCassandra}
import whoseturn.todos.DomainFixtures._

class WhoseTurnTodoRepositorySpec
    extends AnyWordSpec
    with Matchers
    with NewTodoFixture
    with TodoFixture
    with CassandraSupport {

  private def withCache[A](fn: WhoseTurnTodoRepository => A): A =
    withCassandraSession("/todos-cassandra-test-schema.json") { session =>
      fn(new WhoseTurnTodoRepository(session))
    }

  private def withSession[A, B](fn: Session => B): B = withCassandraSession() { session =>
    fn(session)
  }

  "WhoseTurnTodoRepository.addTodo" should {
    "add new entity" when {
      "Todo entity valid" in withCache { repository =>
        repository.write(defaultTodo).futureValue

        withSession { session =>
          val cassandraTodoEntity = readTodoFromCassandra(defaultTodoId, session).get
          val expectedEntity      = WhoseTurnTodoFactory.toTodoEntity(defaultTodo)

          cassandraTodoEntity mustBe expectedEntity
        }
      }
    }
  }

  "WhoseTurnTodoRepository" should {
    "saved and load todo" in withCache { repository =>
      repository.write(defaultTodo).futureValue
      val savedTodo = repository.readById(defaultTodo.id).futureValue.get

      val expectedTodo = defaultTodo
      savedTodo mustBe expectedTodo
    }
  }
}

object WhoseTurnTodoRepositorySpec {

  class TodoTableStub(session: Session) extends TodosDatabase(session) {}

  def readTodoFromCassandra(todoId: UUID, session: Session): Option[TodoEntity] = {
    val todoTable = new TodoTableStub(session)
    todoTable.readRecordByIdFromCassandra(todoId).futureValue
  }

  def addTodosToCassandra(todos: List[TodoEntity], session: Session): Unit = {
    val todoTable = new TodoTableStub(session)
    todos.foreach(todoTable.writeRecordToCassandra)
  }
}
