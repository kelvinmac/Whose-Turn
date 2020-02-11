using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class Games
    {
        public Games()
        {
            ServerSeasons = new HashSet<ServerSeasons>();
            ServerSessions = new HashSet<ServerSessions>();
        }

        public Guid GameId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ServerSeasons> ServerSeasons { get; set; }
        public virtual ICollection<ServerSessions> ServerSessions { get; set; }
    }
}
