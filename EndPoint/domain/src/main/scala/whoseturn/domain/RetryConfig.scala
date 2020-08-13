package whoseturn.domain

import java.util.concurrent.TimeUnit

import scala.concurrent.duration.FiniteDuration

trait Retry[F[_]] {
  type Lazy[A] = () => F[A]
  def retry[A](attempt: Lazy[A])(config: RetryConfig, taskName: String): F[A]
}

object Retry {
  val defaultRetryConfig: RetryConfig = RetryConfig(3, new FiniteDuration(200, TimeUnit.MILLISECONDS))

  implicit def toRetryOps[F[_], A](eval: () => F[A]): RetryOps[F, A] = new RetryOps[F, A](eval)

  sealed class RetryOps[F[_], A](eval: () => F[A]) {
    def retry(config: RetryConfig, taskName: String)(implicit R: Retry[F]): F[A] =
      R.retry(eval)(config, taskName)
  }
}

final case class RetryConfig(maxAttempts: Int, delay: FiniteDuration)
