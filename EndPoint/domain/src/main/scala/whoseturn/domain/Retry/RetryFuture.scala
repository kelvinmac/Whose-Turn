package whoseturn.domain.Retry

import com.typesafe.scalalogging.LazyLogging
import scala.concurrent.ExecutionContext.Implicits.global
import scala.concurrent.Future

class RetryFuture extends Retry[Future] with LazyLogging {

  override def run[T](remainingAttempts: Int, taskName: String)(op: () => Future[T])(
      implicit config: RetryConfig
  ): Future[T] = op() recoverWith {
    case throwable: Throwable if remainingAttempts > 0 =>
      val delay = config.retryDelay(remainingAttempts).toMillis

      logger.info(
        s"Task [taskName=$taskName] failed with [exception=${throwable.getLocalizedMessage}]. " +
          s"Retrying [attempt=$remainingAttempts of ${config.maxReattempts}] with [delay=${delay}ms]"
      )

      Thread.sleep(delay)
      run(remainingAttempts - 1, taskName)(op)
  }
}
