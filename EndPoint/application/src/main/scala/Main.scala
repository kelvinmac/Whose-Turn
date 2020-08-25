import java.io.FileInputStream

import cats.effect.IO.ioEffect
import cats.effect.{ExitCode, IO, IOApp, Resource}
import com.twitter.finagle.ListeningServer
import com.twitter.util.Await
import com.typesafe.scalalogging.LazyLogging
import config.{AppConfig, ArgParameters}
import pureconfig.ConfigSource
import pureconfig.generic.auto._
import cats.syntax.either._
import scala.io.Source
import scala.util.{Failure, Success, Try}

object Main extends IOApp with LazyLogging {

  def run(args: List[String]): IO[ExitCode] = {
    val io = for {
      envParams <- ArgParameters.parse(args)
      config    <- loadConfig
      serverIo  <- Startup.configure(config, envParams)
    } yield serverIo

    io.runAsync {
        case Left(err) => IO(logger.error("There was an error while starting API", err))
      }
      .unsafeRunSync()

    IO(ExitCode.Success)
  }

  def loadConfig: IO[AppConfig] = {
    IO {
      ConfigSource.default.loadOrThrow[AppConfig]
    }
  }
}
