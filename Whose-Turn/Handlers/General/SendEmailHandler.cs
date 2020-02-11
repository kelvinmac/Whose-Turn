using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using SendGrid;
using Whose_Turn.Servicebus;
using System.Collections.Generic;
using SendGrid.Helpers.Mail;
using Whose_Turn.ConfigModels;

namespace Whose_Turn.Handlers.General
{
    public class SendEmailHandler : IHandleMessages<SendEmail>
    {
        public static class LogEvents
        {
            public static readonly EventId HandlingSendEmailRequest = new EventId(1, nameof(Handle));
        }

        private readonly ISendGridClient _sendGrid;
        private readonly ILogger _logger;

        public SendEmailHandler(IServiceProvider serviceProvider)
        {
            var sgConfig = serviceProvider.GetService<SendGridConfiguration>();
            _sendGrid = new SendGridClient(sgConfig.ApiKey);

            _logger = serviceProvider.GetService<ILogger<SendEmailHandler>>();
        }

        public async Task Handle(SendEmail message, IMessageHandlerContext context)
        {
            message.LoggerId = message.LoggerId == Guid.Empty
                ? Guid.NewGuid()
                : message.LoggerId;

            using var _ = _logger.BeginScope(new Dictionary<string, object>()
            {
                ["loggerId"] = message.LoggerId
            });

            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress(message.Sender, "Whoseturn Team"));
            msg.AddTos(new List<EmailAddress>
                {
                    new EmailAddress(message.Recipient)
                });

            // Basic configuration
            msg.SetSubject(message.Subject);
            msg.AddContent(MimeType.Html, message.EmailContent);

            var response = await _sendGrid.SendEmailAsync(msg);
            var responseBody = await response.Body.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(responseBody))
            {
                _logger.LogInformation(LogEvents.HandlingSendEmailRequest, "Email was sent with errors, response '{response}'", responseBody);
                return;
            }

            _logger.LogInformation(LogEvents.HandlingSendEmailRequest, "Email was successfully send to {email} using SendGrid", message.Recipient);
        }
    }
}
