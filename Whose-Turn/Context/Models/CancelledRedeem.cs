using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class CancelledRedeem
    {
        public Guid RedeemCancelId { get; set; }
        public DateTime CancelledOn { get; set; }
        public string Reason { get; set; }

        public virtual RedeemHistories RedeemHistories { get; set; }
    }
}
