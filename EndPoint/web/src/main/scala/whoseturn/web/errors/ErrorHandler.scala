package whoseturn.web.errors

import io.circe.Encoder
import io.circe.syntax._
import io.finch
import io.finch.{BadRequest, Output}
import org.slf4j.LoggerFactory

object ErrorHandler {

  private val logger = LoggerFactory.getLogger("whoseturn.web.endpoints")

  val apiErrorHandlerAndLogger: PartialFunction[Throwable, Output[Nothing]] = {
    case ex: finch.Error =>
      logger.warn(s"(client error) ${ex.getMessage}")
      BadRequest(ex)

    case ex: finch.Errors =>
      logger.warn(s"(client error) ${ex.getMessage}")
      BadRequest(ex)
  }

  val encodeExceptionCirce: Encoder[Exception] = Encoder.instance(e =>
    ErrorResponse(
      message = Option(e.getMessage).getOrElse("No error message available")
    ).asJson
  )
}
