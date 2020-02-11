using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class ServerSeasons
    {
        public ServerSeasons()
        {
            PlayerSessions = new HashSet<PlayerSessions>();
            ServerSessions = new HashSet<ServerSessions>();
        }

        public Guid SeasonId { get; set; }
        public string SeasonName { get; set; }
        public DateTime SeasonStart { get; set; }
        public DateTime SeasonEnd { get; set; }
        public Guid ServerId { get; set; }
        public Guid GameId { get; set; }

        public virtual Games Game { get; set; }
        public virtual Servers Server { get; set; }
        public virtual ICollection<PlayerSessions> PlayerSessions { get; set; }
        public virtual ICollection<ServerSessions> ServerSessions { get; set; }
    }
}
