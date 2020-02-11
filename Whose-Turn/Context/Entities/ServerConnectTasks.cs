using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class ServerConnectTasks
    {
        public Guid Id { get; set; }
        public Guid ServerId { get; set; }
        public string TaskType { get; set; }
        public string Payload { get; set; }

        public virtual Servers Server { get; set; }
    }
}
