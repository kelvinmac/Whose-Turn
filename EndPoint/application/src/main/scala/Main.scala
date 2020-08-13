import cats.effect.IO.ioEffect
import cats.effect.{ExitCode, IO, IOApp, Resource}
import cats.implicits._
import com.datastax.driver.core.Session
import com.twitter.finagle.http.{Request, Response}
import com.twitter.finagle.{Http, ListeningServer, Service}
import com.twitter.util.Future
import com.typesafe.scalalogging.LazyLogging
import config.{AppConfig, CassandraConfig}
import io.circe.Encoder
import whoseturn.web.endpoints.CreateTodoEndpoint
import whoseturn.web.errors.ErrorHandler
import io.circe.generic.auto._
import io.finch.circe._
import io.finch.{Application, Bootstrap, ToAsync}
import pureconfig.ConfigSource
import pureconfig.generic.auto._
import pureconfig._
import whoseturn.domain.Retry.defaultRetryConfig
import whoseturn.domain.RetryConfig
import whoseturn.domain.todos.WhoseTurnTodoRepository

object Main extends IOApp with LazyLogging {
  def run(args: List[String]): IO[ExitCode] = {
    val config = loadConfig

    logger.info(s"Starting web service on port ${config.serviceConfig.port}")

    val server =
      Resource.make(serve(config.serviceConfig.port, config.cassandraConfig))(s =>
        IO.suspend(implicitly[ToAsync[Future, IO]].apply(s.close()))
      )
    server.use(_ => IO.never).as(ExitCode.Success)
  }

  def loadConfig: AppConfig = {
    ConfigSource.default.loadOrThrow[AppConfig]
  }

  def serve(port: Int, cassandraConfig: CassandraConfig): IO[ListeningServer] = {
    val cassandraSession        = createCassandraSession(cassandraConfig)
    val whoseTurnTodoRepository = new WhoseTurnTodoRepository(defaultRetryConfig, cassandraSession)

    val todoEndpoint = new CreateTodoEndpoint(whoseTurnTodoRepository)
    IO(Http.server.serve(s":$port", service(todoEndpoint)))
  }

  def service(todoEndpoint: CreateTodoEndpoint): Service[Request, Response] = {
    implicit def encodeExceptionCirce: Encoder[Exception] = ErrorHandler.encodeExceptionCirce
    val jsonEndpoints                                     = todoEndpoint.endpoint

    Bootstrap
      .serve[Application.Json](jsonEndpoints.handle(ErrorHandler.apiErrorHandlerAndLogger))
      .toService
  }

  def createCassandraSession(cassandraConfig: CassandraConfig): Session = ???
}
