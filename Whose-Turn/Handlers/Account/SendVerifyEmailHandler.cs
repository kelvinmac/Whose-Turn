using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using Whose_Turn.Servicebus;
using Whose_Turn.Managers;
using System.Collections.Generic;

namespace Whose_Turn.Handlers.Account
{
    public class SendVerifyEmailHandler : IHandleMessages<SendVerifyEmail>
    {
        public static class LogEvents
        {
            public static readonly EventId HandlingVerifyEmail = new EventId(1, nameof(Handle));
        }

        private readonly ILogger _logger;
        private readonly Usermanager _usermanager;

        public SendVerifyEmailHandler(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<SendVerifyEmailHandler>>();
            _usermanager = serviceProvider.GetService<Usermanager>();
        }

        public async Task Handle(SendVerifyEmail message, IMessageHandlerContext context)
        {
            var loggerId = Guid.NewGuid();

            using var _ = _logger.BeginScope(new Dictionary<string, object>
            {
                ["loggerId"] = loggerId
            });

            _logger.LogInformation(LogEvents.HandlingVerifyEmail, "Sending verification email to user {userId}", message.UserId);
            var user = await _usermanager.FindUserById(message.UserId);

            await context.Send(new SendEmail()
            {
                EmailContent = "Verify you email, Code: 1111",
                LoggerId = loggerId,
                Recipient = user.Email,
                Sender = "donotreply@whoseturn.co.uk",
                Subject = "Verify your email"
            });
        }
    }
}
