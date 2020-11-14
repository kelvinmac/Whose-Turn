package whoseturn.domain.Retry

import cats.effect._
import cats.implicits._
import cats.implicits.catsSyntaxFlatMapOps
import com.typesafe.scalalogging.LazyLogging
import cats.effect.{IO, Timer}
import scala.concurrent.ExecutionContext.Implicits.global
class RetryIO extends Retry[IO] with LazyLogging {
  override def run[T](remainingAttempts: Int, taskName: String)(op: () => IO[T])(
      implicit config: RetryConfig
  ): IO[T] = {
    op().handleErrorWith {
      case throwable: Throwable if remainingAttempts > 0 =>
        val delay                     = config.retryDelay(remainingAttempts)
        implicit val timer: Timer[IO] = IO.timer(global)

        IO(
          logger.warn(
            s"IO Task [taskName=$taskName] failed with [exception=${throwable.getLocalizedMessage}]. " +
              s"Retrying [attempt=$remainingAttempts of ${config.maxReattempts}] with [delay=${delay}ms]",
            throwable
          )
        ) >> run(remainingAttempts - 1, taskName)(op).delayBy(delay)
    }
  }
}
