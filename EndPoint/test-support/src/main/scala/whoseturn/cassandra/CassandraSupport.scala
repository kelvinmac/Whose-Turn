package whoseturn.cassandra

import com.datastax.driver.core.{Cluster, Session}
import com.typesafe.scalalogging.LazyLogging
import io.circe.{Decoder, ParsingFailure, parser}
import io.circe.generic.semiauto.deriveDecoder
import org.scalatest.concurrent.ScalaFutures
import org.scalatest.{BeforeAndAfterAll, Suite}
import whoseturn.cassandra.CassandraSupport.cassandraQueriesDecoder

import scala.io.Source

trait CassandraSupport extends ScalaFutures with BeforeAndAfterAll with LazyLogging {
  this: Suite =>

  private var cassandraSession: Option[Session] = None
  private var cassandraCluster: Option[Cluster] = None

  def withCassandraSession[U](initialiseResource: String = "")(fn: Session => U): U = {
    val session = stickySession(initialiseResource)
    fn(session)
  }

  override protected def beforeAll(): Unit = {
    logger.info("Preparing embedded Cassandra cluster")
    val configUrl = getClass.getResource("/cassandra/cassandra.yaml").toString
    cassandraCluster = Some(EmbeddedCassandra.startAndConnect(configUrl))
    ()
  }

  override protected def afterAll(): Unit = {
    logger.info("Cleaning up embedded Cassandra cluster")

    cassandraSession.foreach(session => session.close())
    cassandraCluster.foreach(cluster => cluster.close())
  }

  def stickySession(initialiseResource: String): Session = {
    cassandraSession match {
      case Some(session) => session
      case _ =>
        val session = connectToCluster(cassandraCluster.get)
        cassandraSession = Some(session)

        initialiseCassandra(initialiseResource, session)
        session
    }
  }

  private def connectToCluster(cluster: Cluster): Session = cluster.connect()

  private def initialiseCassandra(initialiseResource: String, session: Session): Unit = {
    if (initialiseResource.isEmpty)
      return
    logger.info(s"Initialising Cassandra with [resource=$initialiseResource]")

    readInitialisationFile(initialiseResource).Queries
      .foreach(session.execute)

  }

  private def readInitialisationFile(fileName: String): CassandraQueries = {
    val fileContent = getClass.getResourceAsStream(fileName)
    val str         = Source.fromInputStream(fileContent).getLines.mkString("\n")

    parseToCassandraQueries(str)
  }

  private def parseToCassandraQueries(str: String): CassandraQueries = {
    parser.decode[CassandraQueries](str) match {
      case Right(queries) => queries
      case Left(error) =>
        throw new Exception(
          s"Error while parsing cassandra initialisation queries Json: ${error.getLocalizedMessage}"
        )
    }
  }
}

object CassandraSupport {
  implicit val cassandraQueriesDecoder: Decoder[CassandraQueries] = deriveDecoder[CassandraQueries]
}
