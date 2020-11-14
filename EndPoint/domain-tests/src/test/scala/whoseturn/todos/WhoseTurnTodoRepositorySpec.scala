package whoseturn.todos

import java.util.UUID

import com.datastax.driver.core.Session
import org.scalatest.concurrent.ScalaFutures.convertScalaFuture
import org.scalatest.matchers.must.Matchers
import org.scalatest.wordspec.AnyWordSpec
import whoseturn.domain.Retry.Implicits._
import whoseturn.domain.todos.entities.{TodoEntity, TodosDatabase}
import whoseturn.domain.todos.{WhoseTurnTodoFactory, WhoseTurnTodoRepository}
import WhoseTurnTodoRepositorySpec.readTodoFromCassandra
import cats.effect.IO
import whoseturn.domain.Retry.RetryIO
import whoseturn.test.support.cassandra.CassandraSupport
import whoseturn.test.support.todos.DomainFixtures.defaultTodoId
import whoseturn.test.support.todos.{CreateTodoRequestBodyFixture, TodoFixture}

class WhoseTurnTodoRepositorySpec
    extends AnyWordSpec
    with Matchers
    with CreateTodoRequestBodyFixture
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
