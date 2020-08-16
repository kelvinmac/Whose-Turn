package whoseturn.domain.Retry

import scala.concurrent.duration.{FiniteDuration, MILLISECONDS}

object Implicits {
  implicit val defaultRetryConfig: RetryConfig = RetryConfig(
    maxReattempts = 5,
    retryDelay = c => FiniteDuration(15 * c, MILLISECONDS)
  )

  implicit val defaultFutureRetry: RetryFuture = new RetryFuture()
}
