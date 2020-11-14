package whoseturn.todos

import java.util.{Properties, UUID}

import com.dimafeng.testcontainers.{ForAllTestContainer, KafkaContainer}
import io.confluent.kafka.serializers.KafkaAvroSerializer
import org.apache.kafka.common.serialization.StringSerializer
import org.scalatest.matchers.must.Matchers
import org.scalatest.wordspec.AnyWordSpec
import whoseturn.domain.Retry.Implicits.defaultRetryConfig
import whoseturn.domain.todos.TodoFeedItemProducer
import whoseturn.test.support.kafka.KafkaSupport
import whoseturn.test.support.todos._
import whoseturn.todos.TodoFeedItemProducerSpec.{defaultKafkaProperties, defaultSchemaPath}

class KafkaNewTodoNotificationServiceSpec
    extends AnyWordSpec
    with ForAllTestContainer
    with KafkaSupport
    with Matchers
    with NewTodoNotificationFixture
    with NewTodoFixture {

  val container = new KafkaContainer()

  def withProducer[A](schema: String = defaultSchemaPath)(fn: TodoFeedItemProducer => A): A = {
    registerSchema(getClass.getResource(schema))

    val producerConfig: NewTodoNotificationServiceConfig = NewTodoNotificationServiceConfig(
      kafkaProperties = defaultKafkaProperties,
      topicName = "Test_Topic",
      namespace = "Test",
      triggeredBy = "Test",
      retryConfig = defaultRetryConfig
    )

    KafkaNewTodoNotificationService(producerConfig)
      .map(fn)
      .unsafeRunSync()
  }

  "TodoFeedItemProducer.addTodoFeedItem" should {
    "add item to kafka" in {
      withProducer() { producer: TodoFeedItemProducer =>
//        producer.addTodoFeedItem(defaultNewTodo).futureValue
      }
    }
  }
}

object TodoFeedItemProducerSpec {

  val defaultSchemaPath = "/schemas/todos_feed-value.avcs"

  def defaultKafkaProperties: Properties = {
    val properties = new Properties()
    properties.put("bootstrap.servers", "localhost:6001")
    properties.put("topicName", "testTopic")
    properties.put("key.serializer", classOf[StringSerializer].getCanonicalName)
    properties.put("value.serializer", classOf[KafkaAvroSerializer].getCanonicalName)
    properties.put("schema.registry.url", "http://localhost:6002/subjects/todos_feed-value/versions/latest")
    properties.put("client.id", UUID.randomUUID().toString)
    properties.put("batch.size", "0") // For testing purposes
    properties
  }
}
