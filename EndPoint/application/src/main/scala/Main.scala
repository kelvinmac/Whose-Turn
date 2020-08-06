import cats.effect.IO.ioEffect
import cats.effect.{ExitCode, IO, IOApp, Resource}
import com.twitter.finagle.http.{Request, Response}
import com.twitter.finagle.{Http, ListeningServer, Service}
import com.twitter.util.Future
import com.typesafe.scalalogging.LazyLogging
import config.AppConfig
import whoseturn.web.endpoints.CreateTodoEndpoint
import whoseturn.web.errors.ErrorHandler
import io.circe.generic.auto._
import io.finch.circe._
import io.finch.{Application, Bootstrap, ToAsync}
import pureconfig.ConfigSource
import pureconfig.generic.auto._
import pureconfig._
import whoseturn.web.todos.WhoseTurnTodoRepository

object Main extends IOApp with LazyLogging {
  def run(args: List[String]): IO[ExitCode] = {
    val config = loadConfig

    logger.info(s"Starting web service on port ${config.serviceConfig.port}")

    val server =
      Resource.make(serve(config.serviceConfig.port))(s => IO.suspend(implicitly[ToAsync[Future, IO]].apply(s.close())))
    server.use(_ => IO.never).as(ExitCode.Success)
  }

  def loadConfig: AppConfig = {
    ConfigSource.default.loadOrThrow[AppConfig]
  }

  def serve(port: Int): IO[ListeningServer] = {
    val todoEndpoint = new CreateTodoEndpoint(new WhoseTurnTodoRepository())

    IO(Http.server.serve(s":$port", service(todoEndpoint)))
  }

  def service(todoEndpoint: CreateTodoEndpoint): Service[Request, Response] = {
    val jsonEndpoints = todoEndpoint.endpoint

    Bootstrap
      .serve[Application.Json](jsonEndpoints.handle(ErrorHandler.apiErrorHandlerAndLogger))
      .toService
  }

}
