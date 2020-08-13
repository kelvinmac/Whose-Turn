package config

case class ServiceConfig(port: Int)

case class CassandraConfig()

case class AppConfig(serviceConfig: ServiceConfig, cassandraConfig: CassandraConfig)
