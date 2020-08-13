package whoseturn.domain

import cats.data.NonEmptyList
import io.circe.Decoder.Result
import io.circe.{Decoder, Encoder, HCursor, Json}
import org.joda.time.DateTime

object CustomEncoders {
  implicit val DateTimeFormat: Encoder[DateTime] with Decoder[DateTime] =
    new Encoder[DateTime] with Decoder[DateTime] {
      override def apply(d: DateTime): Json =
        Encoder.encodeString.apply(d.toString())

      override def apply(c: HCursor): Result[DateTime] =
        Decoder.decodeString.map(s => DateTime.parse(s)).apply(c)
    }

}
