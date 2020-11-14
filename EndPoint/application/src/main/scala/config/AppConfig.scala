package config

case class ServiceConfig(port: Int)

case class CassandraConfig(hostAddress: String, port: Int, user: String = "cassandra", password: String = "cassandra")

case class KafkaFeedItemProducerConfig(
    registrySchemaUrl: String,
    serverAddress: String,
    topic: String
)

case class AppConfig(
    serviceConfig: ServiceConfig,
    cassandraConfig: CassandraConfig,
    kafkaFeedItemProducerConfig: KafkaFeedItemProducerConfig
)
