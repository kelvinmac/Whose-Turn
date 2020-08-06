package config

case class ServiceConfig(port: Int)

case class AppConfig (serviceConfig: ServiceConfig)

