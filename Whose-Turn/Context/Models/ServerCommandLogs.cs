using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class ServerCommandLogs
    {
        public Guid Id { get; set; }
        public string Command { get; set; }
        public string Parameters { get; set; }
        public string Source { get; set; }
        public DateTime Time { get; set; }
        public decimal? PlayerId { get; set; }
        public Guid ServerId { get; set; }

        public virtual Players Player { get; set; }
        public virtual Servers Server { get; set; }
    }
}
