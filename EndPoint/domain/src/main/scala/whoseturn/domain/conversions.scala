package whoseturn.domain

import java.util.concurrent.Future

import scala.concurrent.ExecutionContext.Implicits.global

object JavaFutureConversions {
  implicit class JavaFuture[A](val tf: Future[A]) extends AnyVal {
    def asScala: scala.concurrent.Future[A] = {
      scala.concurrent.Future[A](tf.get())
    }
  }
}
