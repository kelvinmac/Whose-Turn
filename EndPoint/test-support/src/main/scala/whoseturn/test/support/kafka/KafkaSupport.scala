package whoseturn.test.support.kafka

import java.net.URL

import org.apache.commons.io.FilenameUtils
import org.scalatest.{BeforeAndAfterAll, Suite}
import com.typesafe.scalalogging.LazyLogging
import io.circe.Encoder
import io.circe.generic.semiauto.deriveEncoder
import scalaj.http.{Http, HttpRequest}
import whoseturn.test.support.kafka.KafkaSupport.SchemaRegistryBodyEncoder

import scala.io.Source

trait KafkaSupport extends BeforeAndAfterAll with LazyLogging with EmbeddedKafka {
  this: Suite =>

  override def beforeAll(): Unit = {}

  override def afterAll(): Unit = {}

  def registerSchema(schemaResourcePath: URL): Unit = {
    val schemaName = FilenameUtils.getName(schemaResourcePath.getPath)
    val schema = SchemaRegistryBody(
      schema = Source.fromFile(schemaResourcePath.getPath).mkString
    )

  }

  private def buildRequest[T](path: String, body: T)(implicit ec: Encoder[T]): HttpRequest =
    Http(path)
      .headers("Content-Type" -> "application/vnd.schemaregistry.v1+json")
      .headers("User-Agent" -> "Kafka-Test")
      .postData(ec(body).noSpaces)
}

case class SchemaRegistryBody(schema: String)

object KafkaSupport {
  implicit val SchemaRegistryBodyEncoder: Encoder[SchemaRegistryBody] = deriveEncoder[SchemaRegistryBody]
}
