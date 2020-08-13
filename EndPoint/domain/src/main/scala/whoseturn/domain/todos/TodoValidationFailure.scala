package whoseturn.domain.todos

sealed trait TodoValidationFailure {}

object TodoValidationFailure {
  final case class ParseError(field: String)     extends TodoValidationFailure
  final case class InvalidUserId(userId: String) extends TodoValidationFailure
  final case class InvalidDescription()          extends TodoValidationFailure
}
