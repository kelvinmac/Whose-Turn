import sbt.file

name := "Whose turn"
version := "0.1"
scalaVersion := "2.13.3"

lazy val commonSettings = Seq(
  organization := "com.kevo",
  libraryDependencies ++= List(
    "org.typelevel"              %% "cats-effect"   % "2.1.3",
    "org.typelevel"              %% "cats-core"     % "2.1.1",
    "io.circe"                   %% "circe-generic" % "0.13.0",
    "com.typesafe.scala-logging" %% "scala-logging" % "3.9.2",
    "org.slf4j"                  % "slf4j-api"      % "1.7.5",
    "org.slf4j"                  % "slf4j-simple"   % "1.7.5",
    "com.github.nscala-time"     %% "nscala-time"   % "2.24.0"
  ),
  publish := {}
)

lazy val httpDependancies = Seq(
  libraryDependencies ++= List(
    "com.github.finagle"    %% "finchx-core"  % "0.32.1",
    "com.github.finagle"    %% "finchx-circe" % "0.32.1",
    "com.github.pureconfig" %% "pureconfig"   % "0.13.0"
  )
)

lazy val domain = (project in file("domain"))
  .settings(commonSettings)
  .settings(
    name := "domain",
    libraryDependencies ++= List(
      )
  )

lazy val web = (project in file("web"))
  .dependsOn(domain)
  .settings(commonSettings)
  .settings(httpDependancies)
  .settings(
    name := "web",
    libraryDependencies ++= List(
      )
  )

lazy val application = (project in file("application"))
  .dependsOn(web, domain)
  .settings(commonSettings)
  .settings(httpDependancies)
  .settings(
    name := "application",
    libraryDependencies ++= List(
      )
  )
