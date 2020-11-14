Hello everyone, I have the following simple code: 

```scala
def t: ValidatedNel[String, String] = {
    Applicative[ValidatedNel[String, ?]].map2(Right("").toValidatedNel, Right(4).toValidatedNel)((_, _) => "")
}
```
However, when I try to compile it I get the following errors,
```
not found: type ?
    Applicative[ValidatedNel[String, ?]].map2(Right("").toValidatedNel, Right(4).toValidatedNel)((_, _) => "")

cats.data.ValidatedNel[String,<error>] takes no type parameters, expected: one
    Applicative[ValidatedNel[String, ?]].map2(Right("").toValidatedNel, Right(4).toValidatedNel)((_, _) => "")
```
I have the following in my build.sbt, I suspect i'm missing a configuration as that same function compiles on a project from work.
```
lazy val commonSettings = Seq(
  organization := "com.kevo",
  scalacOptions += "-Ypartial-unification",
  resolvers += "Confluent" at "https://packages.confluent.io/maven/",
  libraryDependencies ++= List(
    "org.typelevel"              %% "cats-core"     % "2.1.1",
    "org.typelevel"              %% "cats-effect"   % "2.1.4",
    "com.typesafe.scala-logging" %% "scala-logging" % "3.9.2",
    "org.slf4j"                  % "slf4j-api"      % "1.7.5",
    "org.slf4j"                  % "slf4j-simple"   % "1.7.5",
    "com.github.nscala-time"     %% "nscala-time"   % "2.24.0",
    "org.scalatest"              %% "scalatest"     % "3.2.0" % "test"
  ),
  publish := {}
)
```