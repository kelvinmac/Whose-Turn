package whoseturn.web.todos

import io.circe.generic.semiauto.{deriveDecoder, deriveEncoder}
import io.circe.{Decoder, Encoder}
import whoseturn.domain.Todos.NewTodo

object Implicits {
  implicit val decodeNewTodo: Decoder[NewTodo] = deriveDecoder[NewTodo]
  implicit val encodeNewTodo: Encoder[NewTodo] = deriveEncoder[NewTodo]
}
