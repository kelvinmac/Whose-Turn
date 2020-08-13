package whoseturn.domain.todos

import cats.data._

final case class TodoBuildResult(validationErrors: List[TodoValidationFailure], todo: Option[Todo])

object TodoBuildResult {
  def validationFailuresOnly(failures: List[TodoValidationFailure]): TodoBuildResult = TodoBuildResult(failures, None)
  def todoOnly(todo: Todo): TodoBuildResult                                          = TodoBuildResult(Nil, Some(todo))

  def fromValidatedNel(validation: ValidatedNel[TodoValidationFailure, Todo]): TodoBuildResult =
    validation.leftMap(_.toList).fold(validationFailuresOnly, todoOnly)
}
