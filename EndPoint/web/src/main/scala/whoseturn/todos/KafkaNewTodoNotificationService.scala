package whoseturn.todos

import java.util.Properties

import cats.effect.IO
import com.typesafe.scalalogging.LazyLogging
import org.apache.avro.generic.GenericRecord
import org.apache.kafka.clients.producer.{KafkaProducer, ProducerRecord}
import whoseturn.domain.Retry.Retry._
import whoseturn.domain.Retry._
import whoseturn.domain.Retry.Implicits.defaultFutureRetry
import whoseturn.domain.kafka.todo.{Companion, NewTodo, NewTodoNotification, Payload}
import whoseturn.domain.todos.TodoFeedItemProducer
import whoseturn.domain.JavaFutureConversions._
import whoseturn.todos.KafkaNewTodoNotificationService._

import scala.concurrent.ExecutionContext.Implicits.global
import scala.concurrent.Future

class KafkaNewTodoNotificationService(
    producer: KafkaProducer[String, NewTodoNotification],
    triggeredBy: String,
    namespace: String,
    topic: String
)(implicit retryConfig: RetryConfig)
    extends TodoFeedItemProducer
    with LazyLogging {

  override def addTodoFeedItem(newTodo: NewTodo): Future[Unit] = {
    val key     = s"$namespace/${newTodo.todoId}"
    val payload = createPayload(newTodo, namespace)
    val value   = NewTodoNotification(Companion(triggeredBy), payload)
    val record  = new ProducerRecord(topic, key, value)

    val taskName = s"publishing Kafka[key=$key, triggeredBy=$triggeredBy]"
    withRetry(taskName) { () =>
      producer.send(record).asScala
    }.map(_ => ())
  }
}

object KafkaNewTodoNotificationService {
  def apply(producerConfig: NewTodoNotificationServiceConfig): IO[KafkaNewTodoNotificationService] = {
    for {
      producer <- createProducer(producerConfig.kafkaProperties)
      todoFeedProducer <- IO(
                           new KafkaNewTodoNotificationService(
                             producer = producer,
                             triggeredBy = producerConfig.triggeredBy,
                             namespace = producerConfig.namespace,
                             topic = producerConfig.topicName
                           )(producerConfig.retryConfig)
                         )
    } yield todoFeedProducer
  }

  private def createProducer(kafkaProperties: Properties): IO[KafkaProducer[String, NewTodoNotification]] =
    IO(new KafkaProducer[String, NewTodoNotification](kafkaProperties))

  private def createPayload(todo: NewTodo, namespace: String) = Payload(
    namespace = namespace,
    todoId = todo.todoId.toString,
    createdOn = todo.createdOn.toString,
    assignedTo = todo.assignedTo.toString
  )
}
