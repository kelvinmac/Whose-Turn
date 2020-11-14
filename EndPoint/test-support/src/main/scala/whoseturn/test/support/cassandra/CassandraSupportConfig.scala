package whoseturn.test.support.cassandra

final case class CassandraSupportConfig(
    dataFolder: String = "target/cassandraData",
    startupTimeout: Long = 20000,
    yamlResource: String = null
)

object CassandraSupportConfig {
  implicit val defaultCassandraSupportConfig = CassandraSupportConfig()
}
