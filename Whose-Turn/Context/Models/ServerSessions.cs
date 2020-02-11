using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class ServerSessions
    {
        public Guid SessionId { get; set; }
        public DateTime? SessionStartTime { get; set; }
        public DateTime? SessionEndTime { get; set; }
        public string HubConnectionId { get; set; }
        public Guid RtsConnectionId { get; set; }
        public Guid? GameId { get; set; }
        public Guid SeasonId { get; set; }
        public Guid GameId1 { get; set; }
        public int PlayerCountOnDisconnect { get; set; }

        public virtual Games Game { get; set; }
        public virtual ServerSeasons Season { get; set; }
    }
}
