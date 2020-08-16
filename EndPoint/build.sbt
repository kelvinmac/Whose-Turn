import sbt.file

name := "Whose turn"
version := "0.1"
scalaVersion := "2.13.3"

val circeVersion   = "0.12.3"
val phantomVersion = "2.59.0"

lazy val circeDeps = Seq(
  "io.circe" %% "circe-core",
  "io.circe" %% "circe-generic",
  "io.circe" %% "circe-parser"
).map(_ % circeVersion)

lazy val commonSettings = Seq(
  organization := "com.kevo",
  scalacOptions += "-Ypartial-unification",
  libraryDependencies ++= List(
    "org.typelevel"              %% "cats-core"     % "2.1.1",
    "org.typelevel"              %% "cats-effect"   % "2.1.4",
    "com.typesafe.scala-logging" %% "scala-logging" % "3.9.2",
    "org.slf4j"                  % "slf4j-api"      % "1.7.5",
    "org.slf4j"                  % "slf4j-simple"   % "1.7.5",
    "com.github.nscala-time"     %% "nscala-time"   % "2.24.0",
    "org.scalatest"              %% "scalatest"     % "3.2.0" % "test"
  ),
  libraryDependencies ++= circeDeps,
  publish := {}
)

lazy val httpDependancies = Seq(
  libraryDependencies ++= List(
    "com.github.finagle"    %% "finchx-core"  % "0.32.1",
    "com.github.finagle"    %% "finchx-circe" % "0.32.1",
    "com.github.pureconfig" %% "pureconfig"   % "0.13.0"
  )
)

lazy val cassandraDependancies = Seq(
  libraryDependencies ++= List(
    "com.datastax.cassandra" % "cassandra-driver-core" % "3.7.1",
    "org.apache.cassandra"   % "cassandra-all"         % "3.11.4",
    "com.outworkers"         %% "phantom-dsl"          % phantomVersion
  )
)

lazy val domain = (project in file("domain"))
  .settings(commonSettings, cassandraDependancies)
  .settings(
    name := "domain",
    libraryDependencies ++= List(
      )
  )

lazy val testSupport = (project in file("test-support"))
  .dependsOn(domain)
  .settings(cassandraDependancies, commonSettings)
  .settings(
    name := "Test-Support",
    libraryDependencies ++= List(
      "org.scalatest"      %% "scalatest" % "3.2.0",
      "org.apache.commons" % "commons-io" % "1.3.2"
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
  .settings(commonSettings, httpDependancies)
  .settings(
    name := "Web",
    libraryDependencies ++= List(
      )
  )

lazy val application = (project in file("application"))
  .dependsOn(web, domain, testSupport)
  .settings(commonSettings, httpDependancies)
  .settings(
    name := "application",
    libraryDependencies ++= List(
      )
  )
