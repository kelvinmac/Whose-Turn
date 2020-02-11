using System;
using System.Collections.Generic;

namespace Whose_Turn.Context
{
    public partial class Servers
    {
        public Servers()
        {
            ServerCommandLogs = new HashSet<ServerCommandLogs>();
            ServerConnectTasks = new HashSet<ServerConnectTasks>();
            ServerFacets = new HashSet<ServerFacets>();
            ServerSeasons = new HashSet<ServerSeasons>();
            ServerToLanguages = new HashSet<ServerToLanguages>();
        }

        public Guid ServerId { get; set; }
        public string ServerName { get; set; }
        public Guid CurrentSeasonId { get; set; }
        public string ServerStatementDescriptor { get; set; }
        public Guid OwnerId { get; set; }
        public Guid? Addon { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? CreateBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool Archived { get; set; }
        public DateTime? ArchiveDate { get; set; }
        public Guid? ArchivedBy { get; set; }
        public int ConnectionStatus { get; set; }
        public Guid SettingsId { get; set; }
        public Guid GameId { get; set; }
        public Guid LanguageId { get; set; }
        public string Website { get; set; }

        public virtual ServerSettings Settings { get; set; }
        public virtual ICollection<ServerCommandLogs> ServerCommandLogs { get; set; }
        public virtual ICollection<ServerConnectTasks> ServerConnectTasks { get; set; }
        public virtual ICollection<ServerFacets> ServerFacets { get; set; }
        public virtual ICollection<ServerSeasons> ServerSeasons { get; set; }
        public virtual ICollection<ServerToLanguages> ServerToLanguages { get; set; }
    }
}
