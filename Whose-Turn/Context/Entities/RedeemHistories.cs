using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class RedeemHistories
    {
        public Guid RedeemId { get; set; }
        public DateTime RedeemDate { get; set; }
        public string RedeemItem { get; set; }
        public int? RedeemCount { get; set; }
        public string RedeemLocation { get; set; }
        public Guid SubscriptionId { get; set; }
        public Guid PlayerSessionId { get; set; }
        public Guid? CancelledRedeemId { get; set; }
        public bool IsSuccessful { get; set; }

        public virtual CancelledRedeem CancelledRedeem { get; set; }
        public virtual PlayerSessions PlayerSession { get; set; }
    }
}
