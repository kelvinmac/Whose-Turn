using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class SubscriptionData
    {
        public Guid Id { get; set; }
        public decimal PlayerSteamId { get; set; }
        public Guid VipLevelLevelId { get; set; }
        public Guid PlayerSubscriptionId { get; set; }

        public virtual Players PlayerSteam { get; set; }
        public virtual PlayerSubscriptions PlayerSubscription { get; set; }
        public virtual BaseLevel VipLevelLevel { get; set; }
    }
}
