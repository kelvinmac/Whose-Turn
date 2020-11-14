package whoseturn.test.support.cassandra
import com.dimafeng.testcontainers.{ForAllTestContainer, KafkaContainer}
import org.scalatest.wordspec.AnyWordSpec
import org.testcontainers.containers.wait.strategy.Wait

class DockerizedCassandra extends AnyWordSpec with ForAllTestContainer {
  override val container = KafkaContainer()

}
