import sbt._
object Dependencies {

  object Circe {
    private val version = "0.12.3"

    lazy val circeCore    = "io.circe" %% "circe-core"    % version
    lazy val circeGeneric = "io.circe" %% "circe-generic" % version
    lazy val circeParse   = "io.circe" %% "circe-parser"  % version
  }

  object Cats {
    lazy val cats       = "org.typelevel" %% "cats-core"   % "2.1.1"
    lazy val catsEffect = "org.typelevel" %% "cats-effect" % "2.1.4"
  }

  object Logging {
    lazy val scalaLogging = "com.typesafe.scala-logging" %% "scala-logging" % "3.9.2"
  }

  object Time {
    lazy val nScalaTime = "com.github.nscala-time" %% "nscala-time" % "2.24.0"
  }

  object Finagle {
    private lazy val version = "0.32.1"

    lazy val finagleCore  = "com.github.finagle" %% "finchx-core"  % version
    lazy val finagleCirce = "com.github.finagle" %% "finchx-circe" % version
  }

  object Config {
    lazy val pureConfig = "com.github.pureconfig" %% "pureconfig" % "0.13.0"
  }

  object Cassandra {
    lazy val cassandraDriverCore = "com.datastax.cassandra" % "cassandra-driver-core" % "3.10.2"
    lazy val cassandraAll        = "org.apache.cassandra"   % "cassandra-all"         % "3.11.4"
  }

  object Phantom {
    lazy val phantomDsl = "com.outworkers" %% "phantom-dsl" % "2.59.0"
  }

  object Akka {
    lazy val akkaStreams = "com.typesafe.akka" %% "akka-stream" % "2.6.8"
  }

  object Apache {
    lazy val commons = "org.apache.commons"  % "commons-io"   % "1.3.2"
    lazy val avro    = "org.apache.avro"     % "avro"         % "1.9.2"
    lazy val avro4s  = "com.sksamuel.avro4s" %% "avro4s-core" % "3.1.1"
  }

  object Kafka {
    lazy val kafkaClients        = "org.apache.kafka" % "kafka-clients"         % "2.5.0"
    lazy val kafkaAvroSerializer = "io.confluent"     % "kafka-avro-serializer" % "5.5.1"
  }

  object Http {
    lazy val scalaJHttp = "org.scalaj" %% "scalaj-http" % "2.4.2"
  }

  lazy val testFramework = "org.scalatest" %% "scalatest" % "3.2.0" % "test"

  object TestContainer {
    private lazy val testcontainersScalaVersion = "0.38.4"
    lazy val testContainer                      = "com.dimafeng" %% "testcontainers-scala-scalatest" % testcontainersScalaVersion
    lazy val testContainerKafka                 = "com.dimafeng" %% "testcontainers-scala-kafka" % testcontainersScalaVersion
  }

  lazy val scopt = "com.github.scopt" %% "scopt" % "4.0.0-RC2"

  lazy val overrides = List(
    "com.google.guava" % "guava" % "18.0"
  )
  lazy val exclusions = List(
    "log4j"     % "log4j",
    "org.slf4j" % "slf4j-log4j12"
  )
}
