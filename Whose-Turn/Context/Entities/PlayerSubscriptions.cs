using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class PlayerSubscriptions
    {
        public PlayerSubscriptions()
        {
            PlayerSessions = new HashSet<PlayerSessions>();
        }

        public Guid SubscriptionId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string Status { get; set; }
        public bool CanRedeem { get; set; }
        public DateTime? RedeemDate { get; set; }
        public bool Editable { get; set; }
        public string SubscriptionProvider { get; set; }
        public string SubscriptionProviderId { get; set; }
        public bool IsSuspended { get; set; }
        public string SuspendReason { get; set; }
        public Guid? CreateBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? EndNotBefore { get; set; }
        public bool Archived { get; set; }
        public DateTime? ArchiveDate { get; set; }
        public Guid? ArchivedBy { get; set; }
        public Guid CompleteTransactionId { get; set; }
        public Guid RedeemHistoryId { get; set; }
        public bool HasRenewal { get; set; }

        public virtual SubscriptionData SubscriptionData { get; set; }
        public virtual ICollection<PlayerSessions> PlayerSessions { get; set; }
    }
}
