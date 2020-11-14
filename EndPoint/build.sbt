import sbt.file
import Dependencies._

name := "Whose turn"
version := "0.1"
scalaVersion := "2.13.3"

lazy val commonSettings = Seq(
  organization := "com.kevo",
  scalacOptions += "-Ypartial-unification",
  resolvers += "Confluent" at "https://packages.confluent.io/maven/",
  addCompilerPlugin("org.typelevel" %% "kind-projector" % "0.11.0" cross CrossVersion.full),
  libraryDependencies ++= List(
    Circe.circeCore,
    Circe.circeGeneric,
    Circe.circeParse,
    Cats.cats,
    Cats.catsEffect,
    Logging.scalaLogging,
    Time.nScalaTime,
    testFramework
  ),
  dependencyOverrides ++= overrides,
  excludeDependencies ++= exclusions,
  publish := {}
)

lazy val httpDependencies = Seq(
  libraryDependencies ++= List(
    Finagle.finagleCore,
    Finagle.finagleCirce,
    Config.pureConfig
  )
)

lazy val cassandraDependencies = Seq(
  libraryDependencies ++= List(
    Cassandra.cassandraDriverCore,
    Cassandra.cassandraAll,
    Phantom.phantomDsl
  )
)

lazy val domain = (project in file("domain"))
  .settings(cassandraDependencies, commonSettings)
  .settings(
    name := "domain",
    libraryDependencies ++= List(
      Akka.akkaStreams
    )
  )

lazy val testSupport = (project in file("test-support"))
  .dependsOn(domain)
  .settings(cassandraDependencies, commonSettings)
  .settings(
    resolvers ++= Seq(
      "confluent" at "https://packages.confluent.io/maven/",
      "jitpack" at "https://jitpack.io"
    ),
    name := "Test-Support",
    libraryDependencies ++= List(
      "org.scalatest" %% "scalatest" % "3.2.0",
      Apache.commons,
      Apache.avro,
      Akka.akkaStreams,
      Http.scalaJHttp,
      TestContainer.testContainer,
      TestContainer.testContainerKafka
    )
  )

lazy val domainTests = (project in file("domain-tests"))
  .dependsOn(testSupport, domain)
  .settings(commonSettings)
  .settings(
    name := "Domain-tests",
    libraryDependencies ++= List(
      )
  )

lazy val web = (project in file("web"))
  .dependsOn(domain, testSupport)
  .settings(commonSettings, httpDependencies)
  .settings(
    name := "Web",
    libraryDependencies ++= List(
      Apache.avro4s,
      Kafka.kafkaClients,
      Kafka.kafkaAvroSerializer
    )
  )

lazy val application = (project in file("application"))
  .dependsOn(web, domain, testSupport)
  .settings(commonSettings, httpDependencies)
  .settings(
    name := "application",
    libraryDependencies ++= List(
      scopt
    )
  )
