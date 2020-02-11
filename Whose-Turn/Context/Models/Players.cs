using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class Players
    {
        public Players()
        {
            PlayerSessions = new HashSet<PlayerSessions>();
            ServerCommandLogs = new HashSet<ServerCommandLogs>();
            SubscriptionData = new HashSet<SubscriptionData>();
        }

        public decimal SteamId { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string Currency { get; set; }
        public string SecurityToken { get; set; }

        public virtual ICollection<PlayerSessions> PlayerSessions { get; set; }
        public virtual ICollection<ServerCommandLogs> ServerCommandLogs { get; set; }
        public virtual ICollection<SubscriptionData> SubscriptionData { get; set; }
    }
}
