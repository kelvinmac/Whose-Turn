package whoseturn.domain.cassandra

import cats.effect.IO
import com.datastax.driver.core.{AtomicMonotonicTimestampGenerator, Cluster, Session}
import com.typesafe.scalalogging.LazyLogging

import scala.concurrent.ExecutionContext.Implicits.global
import scala.concurrent.Future
import scala.util.{Failure, Success, Try}

final case class CassandraConnectionConfig(hostAddress: String, port: Int, userName: String, password: String)

object CassandraHelper extends LazyLogging {

  def buildCluster(config: CassandraConnectionConfig): IO[Cluster] = {
    IO.async { cb =>
      Future(
        Cluster.builder
          .addContactPoint(config.hostAddress)
          .withPort(config.port)
          .withCredentials(config.userName, config.password)
          .withTimestampGenerator(new AtomicMonotonicTimestampGenerator)
          .withoutMetrics
          .withoutJMXReporting
          .build
      ).onComplete(f => {
        logger.info("Cassandra done!")
        cb(f.toEither)
      })
    }
  }
}
