package whoseturn.domain.Retry

import com.typesafe.scalalogging.LazyLogging
import scala.concurrent.ExecutionContext
import scala.concurrent.duration._

final case class RetryConfig(maxReattempts: Int, retryDelay: Int => FiniteDuration)

trait Retry[F[_]] {
  def run[T](remainingAttempts: Int, taskName: String)(op: () => F[T])(implicit config: RetryConfig): F[T]
}

object Retry extends LazyLogging {
  def withRetry[F[_], T](
      taskName: String
  )(op: () => F[T])(implicit ec: ExecutionContext, retry: Retry[F], retryConfig: RetryConfig): F[T] =
    retry.run(retryConfig.maxReattempts, taskName)(op)
}
