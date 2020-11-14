import cats._
import cats.data._
import cats.implicits._

import cats.Applicative
import cats.data.{NonEmptyList, ValidatedNel}
import cats.syntax.either._
import cats.syntax.option._
import cats.effect.{ExitCode, IO, IOApp, Resource}
import com.typesafe.scalalogging.LazyLogging
import scala.concurrent.ExecutionContext.Implicits.global
import scala.concurrent.Future

object IoMain extends IOApp with LazyLogging {

  override def run(args: List[String]): IO[ExitCode] = {
    IO(ExitCode.Success)
  }
}
