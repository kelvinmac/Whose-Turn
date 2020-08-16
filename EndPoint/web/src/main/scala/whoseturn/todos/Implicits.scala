package whoseturn.todos

import io.circe.generic.semiauto.{deriveDecoder, deriveEncoder}
import io.circe.{Decoder, Encoder}
import whoseturn.domain.todos.CreateTodoRequestBody

object Implicits {
  implicit val decodeNewTodo: Decoder[CreateTodoRequestBody] = deriveDecoder[CreateTodoRequestBody]
  implicit val encodeNewTodo: Encoder[CreateTodoRequestBody] = deriveEncoder[CreateTodoRequestBody]
}
