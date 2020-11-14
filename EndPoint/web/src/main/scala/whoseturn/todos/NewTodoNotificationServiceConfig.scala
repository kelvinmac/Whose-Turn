package whoseturn.todos

import java.util.Properties

import whoseturn.domain.Retry.RetryConfig

final case class NewTodoNotificationServiceConfig(
    kafkaProperties: Properties,
    topicName: String,
    namespace: String,
    triggeredBy: String,
    retryConfig: RetryConfig
)
