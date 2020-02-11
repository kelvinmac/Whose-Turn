using System;
using NServiceBus;

namespace Whose_Turn.Servicebus
{
    public class SendVerifyEmail : ICommand
    {
        /// <summary>
        /// Gets or sets the user Id
        /// </summary>
        public Guid UserId { get; set; }
    }
}
