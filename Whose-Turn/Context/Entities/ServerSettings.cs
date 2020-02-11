using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class ServerSettings
    {
        public Guid SettingsId { get; set; }
        public Guid ServerId { get; set; }
        public string IntervalType { get; set; }
        public int RestartIntervalDuration { get; set; }
        public string RestartMessage { get; set; }

        public virtual Servers Servers { get; set; }
    }
}
