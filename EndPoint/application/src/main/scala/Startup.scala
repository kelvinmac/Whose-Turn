import java.util.Properties

import cats.effect.{IO, Resource}
import com.datastax.driver.core.Session
import com.twitter.finagle.http.{Request, Response}
import com.twitter.finagle.{Http, ListeningServer, Service}
import com.typesafe.scalalogging.LazyLogging
import config.{AppConfig, ArgParameters, CassandraConfig, KafkaFeedItemProducerConfig}
import io.circe.Encoder
import io.confluent.kafka.serializers.KafkaAvroSerializer
import io.finch.circe._
import io.finch.{Application, Bootstrap}
import org.apache.kafka.common.serialization.StringSerializer
import whoseturn.domain.Retry.Implicits.defaultRetryConfig
import whoseturn.domain.cassandra.{CassandraConnectionConfig, CassandraHelper}
import whoseturn.domain.todos.{TodoFeedItemProducer, WhoseTurnTodoRepository}
import whoseturn.todos.KafkaTodoFeedItemProducer
import whoseturn.web.endpoints.CreateTodoEndpoint
import whoseturn.web.errors.ErrorHandler

object Startup extends LazyLogging {
  def configure(appConfig: AppConfig, envParams: ArgParameters): IO[Unit] = {
    val server = Resource.make {
      serve(appConfig, envParams)
    } { server =>
      IO.async { cb =>
        IO(logger.warn("Shutting down server"))
        server.close().onFailure(e => cb(Left(e))).onSuccess(_ => cb(Right()))
      }
    }

    server.use(_ => IO.never)
  }

  private def serve(appConfig: AppConfig, envParams: ArgParameters): IO[ListeningServer] = {
    for {
      cassandraSession <- initialiseCassandraSession(appConfig.cassandraConfig)
      todoEndpoint     <- initialiseCreateTodoEndpoint(appConfig, envParams, cassandraSession)
      server <- IO {
                 Http.server.serve(s":${appConfig.serviceConfig.port}", buildServices(todoEndpoint))
               }
    } yield server
  }

  private def initialiseCassandraSession(cassandraConfig: CassandraConfig): IO[Session] = {
    val helperConfig = toCassandraConnectionConfig(cassandraConfig)

    CassandraHelper.buildCluster(helperConfig).flatMap { cluster =>
      IO(cluster.connect())
    }
  }

  def toCassandraConnectionConfig(from: CassandraConfig): CassandraConnectionConfig = {
    CassandraConnectionConfig(
      hostAddress = from.hostAddress,
      port = from.port,
      userName = "",
      password = ""
    )
  }

  private def initialiseCreateTodoEndpoint(
      appConfig: AppConfig,
      envParams: ArgParameters,
      cassandraSession: Session
  ): IO[CreateTodoEndpoint] = {
    initialiseTodoFeedKafka(appConfig.kafkaFeedItemProducerConfig, envParams.instanceId).flatMap { producer =>
      IO(new WhoseTurnTodoRepository(cassandraSession)).flatMap { repo =>
        IO(new CreateTodoEndpoint(repo, producer))
      }
    }
  }

  private def initialiseTodoFeedKafka(
      kafkaConfig: KafkaFeedItemProducerConfig,
      instanceId: String
  ): IO[TodoFeedItemProducer] = {
    val properties = createTodoFeedKafkaProperties(kafkaConfig, instanceId)
    KafkaTodoFeedItemProducer(properties, kafkaConfig.schemaResourcePath)
  }

  private def createTodoFeedKafkaProperties(
      kafkaConfig: KafkaFeedItemProducerConfig,
      instanceId: String
  ): Properties = {
    val properties = new Properties()

    properties.put("bootstrap.servers", kafkaConfig.serverAddress)
    properties.put("topicName", kafkaConfig.topic)
    properties.put("key.serializer", classOf[StringSerializer].getCanonicalName)
    properties.put("value.serializer", classOf[KafkaAvroSerializer].getCanonicalName)
    properties.put("schema.registry.url", kafkaConfig.registrySchemaUrl)
    properties.put("client.id", instanceId)
    properties.put("batch.size", "0") // For testing purposes

    properties
  }

  private def buildServices(todoEndpoint: CreateTodoEndpoint): Service[Request, Response] = {
    implicit def encodeExceptionCirce: Encoder[Exception] = ErrorHandler.encodeExceptionCirce

    val jsonEndpoints = todoEndpoint.endpoint

    Bootstrap
      .serve[Application.Json](jsonEndpoints.handle(ErrorHandler.apiErrorHandlerAndLogger))
      .toService
  }
}
