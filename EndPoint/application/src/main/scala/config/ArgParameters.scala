package config

import cats.effect.IO
import scopt.{OParser, OParserBuilder}

final case class ArgParameters(
    instanceId: String = ""
)

object ArgParameters {
  private def makeBuilder: OParserBuilder[ArgParameters] = OParser.builder[ArgParameters]

  def parse(args: List[String]): IO[ArgParameters] = {
    parseArguments(createParser, args)
  }

  private def createParser: IO[OParser[Unit, ArgParameters]] = {
    IO {
      val builder = makeBuilder
      import builder._
      OParser.sequence(
        programName("scopt"),
        head("Whose-turn-API", "1.0"),
        opt[String]('i', "instanceId")
          .action((x, c) => c.copy(instanceId = x))
          .text("Specifies the unique instance Id")
      )
    }
  }

  private def parseArguments(parser: IO[OParser[Unit, ArgParameters]], args: List[String]): IO[ArgParameters] = {
    parser.flatMap { parser =>
      OParser.parse(parser, args, ArgParameters()) match {
        case Some(parsedArguments) => IO.pure(parsedArguments)
        case _                     => IO.raiseError(new Exception("Invalid program arguments"))
      }
    }
  }
}
