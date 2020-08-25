package whoseturn.todos

import java.util.Properties

import cats.effect.IO
import com.typesafe.scalalogging.LazyLogging
import org.apache.avro.Schema
import org.apache.avro.generic.GenericRecord
import org.apache.kafka.clients.producer.KafkaProducer
import whoseturn.domain.todos.TodoFeedItemProducer
import org.apache.avro.Schema.Parser
import org.apache.http.protocol.ExecutionContext

import scala.io.Source
import scala.util.Try

class KafkaTodoFeedItemProducer(producer: KafkaProducer[String, GenericRecord], schema: Schema)
    extends TodoFeedItemProducer
    with LazyLogging {}

object KafkaTodoFeedItemProducer {
  def apply(kafkaProperties: Properties, schemaPath: String): IO[KafkaTodoFeedItemProducer] = {
    for {
      producer         <- createProduder(kafkaProperties)
      schema           <- getSchema(schemaPath)
      todoFeedProducer <- IO(new KafkaTodoFeedItemProducer(producer, schema))
    } yield todoFeedProducer
  }

  private def getSchema(schemaPath: String): IO[org.apache.avro.Schema] =
    IO(Source.fromResource(schemaPath).mkString).flatMap(schemaStr => IO(new Parser().parse(schemaStr)))

  private def createProduder(kafkaProperties: Properties): IO[KafkaProducer[String, GenericRecord]] = {
    IO.async { cb =>
      cb(Try(new KafkaProducer[String, GenericRecord](kafkaProperties)).toEither)
    }
  }
}
