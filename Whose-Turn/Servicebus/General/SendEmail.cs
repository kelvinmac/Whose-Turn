using System;
using NServiceBus;

namespace Whose_Turn.Servicebus
{
    public class SendEmail : IMessage
    {
        /// <summary>
        /// Gets or sets the email recipient
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Gets or sets the sender
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// Gets or sets the loggerId
        /// </summary>
        public Guid LoggerId { get; set; }

        /// <summary>
        /// Gets or sets the subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the email content
        /// </summary>
        public string EmailContent { get; set; }
    }
}
