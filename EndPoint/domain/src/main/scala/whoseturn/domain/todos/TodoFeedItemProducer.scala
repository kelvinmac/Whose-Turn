package whoseturn.domain.todos

import whoseturn.domain.kafka.todo.NewTodo

import scala.concurrent.Future

trait TodoFeedItemProducer {
  def addTodoFeedItem(newTodo: NewTodo): Future[Unit]
}
