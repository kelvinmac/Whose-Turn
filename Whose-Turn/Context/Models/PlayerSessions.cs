using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class PlayerSessions
    {
        public PlayerSessions()
        {
            RedeemHistories = new HashSet<RedeemHistories>();
        }

        public Guid SessionId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid? PlayerSubscriptionId { get; set; }
        public Guid ServerSeasonId { get; set; }
        public decimal PlayerId { get; set; }

        public virtual Players Player { get; set; }
        public virtual PlayerSubscriptions PlayerSubscription { get; set; }
        public virtual ServerSeasons ServerSeason { get; set; }
        public virtual ICollection<RedeemHistories> RedeemHistories { get; set; }
    }
}
