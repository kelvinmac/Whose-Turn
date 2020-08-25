import java.io._

import cats.syntax.either._
import cats.effect.concurrent.Semaphore
import cats.effect.{ExitCode, IO, IOApp, Resource}
import cats.implicits.catsSyntaxFlatMapOps
import com.typesafe.scalalogging.LazyLogging
import scala.concurrent.ExecutionContext.global

object IoMain extends IOApp with LazyLogging {
  type FileResource[A] = Resource[IO, A]

  def inputStream(file: File, guard: Semaphore[IO]): FileResource[InputStream] =
    Resource.make {
      IO(new FileInputStream(file))
    } { stream =>
      guard.withPermit(
        IO(stream.close())
          .handleErrorWith(err => {
            logger.error("There was an error while closing input stream", err)
            IO.unit
          })
      )
    }

  def outputStream(file: File, guard: Semaphore[IO]): FileResource[OutputStream] =
    Resource.make {
      IO(new FileOutputStream(file))
    } { stream =>
      guard.withPermit(
        IO(stream.close())
          .handleErrorWith(err => {
            logger.error("There was an error while closing output stream", err)
            IO.unit
          })
      )
    }

  def inputOutputStreams(in: File, out: File, guard: Semaphore[IO]): Resource[IO, (InputStream, OutputStream)] = {
    for {
      inStream  <- inputStream(in, guard)
      outStream <- outputStream(out, guard)
    } yield (inStream, outStream)
  }

  def transfer(inputStream: InputStream, outputStream: OutputStream): IO[Long] = {
    for {
      buffer <- IO(new Array[Byte](1024 * 4))
      total  <- transmute(inputStream, outputStream, buffer, 0)
    } yield total
  }

  def transmute(origin: InputStream, destination: OutputStream, buffer: Array[Byte], acc: Long): IO[Long] = {
    for {
      amount <- IO(origin.read(buffer, 0, buffer.length))
      count <- if (amount > -1)
                IO(destination.write(buffer, 0, amount)) >> transmute(origin, destination, buffer, acc + amount)
              else IO(acc)
    } yield count
  }

  def copy(origin: File, destination: File): IO[Long] = {
    for {
      guard <- Semaphore[IO](1)
      count <- inputOutputStreams(origin, destination, guard).use {
                case (inStream, outStream) => guard.withPermit(transfer(inStream, outStream))
              }
    } yield count
  }

  override def run(args: List[String]): IO[ExitCode] = {
    val origin      = new File(getClass.getResource("input.txt").toString)
    val destination = new File(getClass.getResource("output.txt").toString)

    val copyIo = copy(origin, destination).runAsync {
      case Left(err) =>
        logger.error("There was an error copying file", err)
        IO.unit
    }

    copyIo.unsafeRunSync
    /*
    val cancellable = IO.cancelable[String] { cb =>
    }
     */

    IO(ExitCode.Success)
  }
}
