using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Whose_Turn.Context
{
    public partial class WhoseturnDbContext : DbContext
    {
        public WhoseturnDbContext()
        {
        }

        public WhoseturnDbContext(DbContextOptions<WhoseturnDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BaseLevel> BaseLevel { get; set; }
        public virtual DbSet<CancelledRedeem> CancelledRedeem { get; set; }
        public virtual DbSet<Facets> Facets { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<LevelManifests> LevelManifests { get; set; }
        public virtual DbSet<PlayerSessions> PlayerSessions { get; set; }
        public virtual DbSet<PlayerSubscriptions> PlayerSubscriptions { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<QrtzBlobTriggers> QrtzBlobTriggers { get; set; }
        public virtual DbSet<QrtzCalendars> QrtzCalendars { get; set; }
        public virtual DbSet<QrtzCronTriggers> QrtzCronTriggers { get; set; }
        public virtual DbSet<QrtzFiredTriggers> QrtzFiredTriggers { get; set; }
        public virtual DbSet<QrtzJobDetails> QrtzJobDetails { get; set; }
        public virtual DbSet<QrtzLocks> QrtzLocks { get; set; }
        public virtual DbSet<QrtzPausedTriggerGrps> QrtzPausedTriggerGrps { get; set; }
        public virtual DbSet<QrtzSchedulerState> QrtzSchedulerState { get; set; }
        public virtual DbSet<QrtzSimpleTriggers> QrtzSimpleTriggers { get; set; }
        public virtual DbSet<QrtzSimpropTriggers> QrtzSimpropTriggers { get; set; }
        public virtual DbSet<QrtzTriggers> QrtzTriggers { get; set; }
        public virtual DbSet<RedeemHistories> RedeemHistories { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<ServerCommandLogs> ServerCommandLogs { get; set; }
        public virtual DbSet<ServerConnectTasks> ServerConnectTasks { get; set; }
        public virtual DbSet<ServerFacets> ServerFacets { get; set; }
        public virtual DbSet<ServerSeasons> ServerSeasons { get; set; }
        public virtual DbSet<ServerSessions> ServerSessions { get; set; }
        public virtual DbSet<ServerSettings> ServerSettings { get; set; }
        public virtual DbSet<ServerToLanguages> ServerToLanguages { get; set; }
        public virtual DbSet<Servers> Servers { get; set; }
        public virtual DbSet<SubscriptionData> SubscriptionData { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersClaims> UsersClaims { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:projectyellowstone.database.windows.net,1433;Initial Catalog=pme.develop.clients;Persist Security Info=False;User ID=kevomacartney;Password=Cd68HeTdkt.UyHQCV7UbvrCVElo4xfDY;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseLevel>(entity =>
            {
                entity.HasKey(e => e.LevelId);

                entity.HasIndex(e => e.FacetId)
                    .HasName("IX_Levels_Facets_FacetId");

                entity.Property(e => e.LevelId).ValueGeneratedNever();

                entity.HasOne(d => d.Facet)
                    .WithMany(p => p.BaseLevel)
                    .HasForeignKey(d => d.FacetId)
                    .HasConstraintName("FK_dbo.Levels_dbo.Facets_FacetsId");
            });

            modelBuilder.Entity<CancelledRedeem>(entity =>
            {
                entity.HasKey(e => e.RedeemCancelId);

                entity.Property(e => e.RedeemCancelId).ValueGeneratedNever();

                entity.Property(e => e.Reason).HasMaxLength(1024);
            });

            modelBuilder.Entity<Facets>(entity =>
            {
                entity.HasKey(e => e.FacetId);

                entity.Property(e => e.FacetId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Games>(entity =>
            {
                entity.HasKey(e => e.GameId);

                entity.Property(e => e.GameId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Languages>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<LevelManifests>(entity =>
            {
                entity.HasIndex(e => e.LevelId)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 6)");

                entity.HasOne(d => d.Level)
                    .WithOne(p => p.LevelManifests)
                    .HasForeignKey<LevelManifests>(d => d.LevelId)
                    .HasConstraintName("FK_dbo.Levels_dbo.LevelManifest_ManifestId");
            });

            modelBuilder.Entity<PlayerSessions>(entity =>
            {
                entity.HasKey(e => e.SessionId);

                entity.HasIndex(e => e.PlayerId);

                entity.HasIndex(e => e.PlayerSubscriptionId);

                entity.HasIndex(e => e.ServerSeasonId);

                entity.Property(e => e.SessionId).ValueGeneratedNever();

                entity.Property(e => e.PlayerId)
                    .HasColumnName("Player_Id")
                    .HasColumnType("decimal(20, 0)");

                entity.Property(e => e.ServerSeasonId).HasColumnName("ServerSeason_Id");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerSessions)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_dbo.Player_dbo.PlayerSession_Id");

                entity.HasOne(d => d.PlayerSubscription)
                    .WithMany(p => p.PlayerSessions)
                    .HasForeignKey(d => d.PlayerSubscriptionId)
                    .HasConstraintName("FK_dbo.PlayerSubscriptions_dbo.PlayerSession_Id");

                entity.HasOne(d => d.ServerSeason)
                    .WithMany(p => p.PlayerSessions)
                    .HasForeignKey(d => d.ServerSeasonId)
                    .HasConstraintName("FK_dbo.ServerSeason_dbo.PlayerSessions_SeasonId");
            });

            modelBuilder.Entity<PlayerSubscriptions>(entity =>
            {
                entity.HasKey(e => e.SubscriptionId);

                entity.Property(e => e.SubscriptionId).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.RedeemDate).HasColumnType("datetime");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.Property(e => e.Status).IsRequired();

                entity.Property(e => e.SubscriptionProvider).IsRequired();

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Players>(entity =>
            {
                entity.HasKey(e => e.SteamId);

                entity.Property(e => e.SteamId).HasColumnType("decimal(20, 0)");
            });

            modelBuilder.Entity<QrtzBlobTriggers>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup });

                entity.ToTable("QRTZ_BLOB_TRIGGERS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.TriggerName)
                    .HasColumnName("TRIGGER_NAME")
                    .HasMaxLength(150);

                entity.Property(e => e.TriggerGroup)
                    .HasColumnName("TRIGGER_GROUP")
                    .HasMaxLength(150);

                entity.Property(e => e.BlobData).HasColumnName("BLOB_DATA");
            });

            modelBuilder.Entity<QrtzCalendars>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.CalendarName });

                entity.ToTable("QRTZ_CALENDARS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.CalendarName)
                    .HasColumnName("CALENDAR_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.Calendar)
                    .IsRequired()
                    .HasColumnName("CALENDAR");
            });

            modelBuilder.Entity<QrtzCronTriggers>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup });

                entity.ToTable("QRTZ_CRON_TRIGGERS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.TriggerName)
                    .HasColumnName("TRIGGER_NAME")
                    .HasMaxLength(150);

                entity.Property(e => e.TriggerGroup)
                    .HasColumnName("TRIGGER_GROUP")
                    .HasMaxLength(150);

                entity.Property(e => e.CronExpression)
                    .IsRequired()
                    .HasColumnName("CRON_EXPRESSION")
                    .HasMaxLength(120);

                entity.Property(e => e.TimeZoneId)
                    .HasColumnName("TIME_ZONE_ID")
                    .HasMaxLength(80);

                entity.HasOne(d => d.QrtzTriggers)
                    .WithOne(p => p.QrtzCronTriggers)
                    .HasForeignKey<QrtzCronTriggers>(d => new { d.SchedName, d.TriggerName, d.TriggerGroup })
                    .HasConstraintName("FK_QRTZ_CRON_TRIGGERS_QRTZ_TRIGGERS");
            });

            modelBuilder.Entity<QrtzFiredTriggers>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.EntryId });

                entity.ToTable("QRTZ_FIRED_TRIGGERS");

                entity.HasIndex(e => new { e.SchedName, e.InstanceName })
                    .HasName("IDX_QRTZ_FT_TRIG_INST_NAME");

                entity.HasIndex(e => new { e.SchedName, e.JobGroup })
                    .HasName("IDX_QRTZ_FT_JG");

                entity.HasIndex(e => new { e.SchedName, e.TriggerGroup })
                    .HasName("IDX_QRTZ_FT_TG");

                entity.HasIndex(e => new { e.SchedName, e.InstanceName, e.RequestsRecovery })
                    .HasName("IDX_QRTZ_FT_INST_JOB_REQ_RCVRY");

                entity.HasIndex(e => new { e.SchedName, e.JobName, e.JobGroup })
                    .HasName("IDX_QRTZ_FT_J_G");

                entity.HasIndex(e => new { e.SchedName, e.TriggerName, e.TriggerGroup })
                    .HasName("IDX_QRTZ_FT_T_G");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.EntryId)
                    .HasColumnName("ENTRY_ID")
                    .HasMaxLength(140);

                entity.Property(e => e.FiredTime).HasColumnName("FIRED_TIME");

                entity.Property(e => e.InstanceName)
                    .IsRequired()
                    .HasColumnName("INSTANCE_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.IsNonconcurrent).HasColumnName("IS_NONCONCURRENT");

                entity.Property(e => e.JobGroup)
                    .HasColumnName("JOB_GROUP")
                    .HasMaxLength(150);

                entity.Property(e => e.JobName)
                    .HasColumnName("JOB_NAME")
                    .HasMaxLength(150);

                entity.Property(e => e.Priority).HasColumnName("PRIORITY");

                entity.Property(e => e.RequestsRecovery).HasColumnName("REQUESTS_RECOVERY");

                entity.Property(e => e.SchedTime).HasColumnName("SCHED_TIME");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("STATE")
                    .HasMaxLength(16);

                entity.Property(e => e.TriggerGroup)
                    .IsRequired()
                    .HasColumnName("TRIGGER_GROUP")
                    .HasMaxLength(150);

                entity.Property(e => e.TriggerName)
                    .IsRequired()
                    .HasColumnName("TRIGGER_NAME")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<QrtzJobDetails>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.JobName, e.JobGroup });

                entity.ToTable("QRTZ_JOB_DETAILS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.JobName)
                    .HasColumnName("JOB_NAME")
                    .HasMaxLength(150);

                entity.Property(e => e.JobGroup)
                    .HasColumnName("JOB_GROUP")
                    .HasMaxLength(150);

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(250);

                entity.Property(e => e.IsDurable).HasColumnName("IS_DURABLE");

                entity.Property(e => e.IsNonconcurrent).HasColumnName("IS_NONCONCURRENT");

                entity.Property(e => e.IsUpdateData).HasColumnName("IS_UPDATE_DATA");

                entity.Property(e => e.JobClassName)
                    .IsRequired()
                    .HasColumnName("JOB_CLASS_NAME")
                    .HasMaxLength(250);

                entity.Property(e => e.JobData).HasColumnName("JOB_DATA");

                entity.Property(e => e.RequestsRecovery).HasColumnName("REQUESTS_RECOVERY");
            });

            modelBuilder.Entity<QrtzLocks>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.LockName });

                entity.ToTable("QRTZ_LOCKS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.LockName)
                    .HasColumnName("LOCK_NAME")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<QrtzPausedTriggerGrps>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerGroup });

                entity.ToTable("QRTZ_PAUSED_TRIGGER_GRPS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.TriggerGroup)
                    .HasColumnName("TRIGGER_GROUP")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<QrtzSchedulerState>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.InstanceName });

                entity.ToTable("QRTZ_SCHEDULER_STATE");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.InstanceName)
                    .HasColumnName("INSTANCE_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.CheckinInterval).HasColumnName("CHECKIN_INTERVAL");

                entity.Property(e => e.LastCheckinTime).HasColumnName("LAST_CHECKIN_TIME");
            });

            modelBuilder.Entity<QrtzSimpleTriggers>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup });

                entity.ToTable("QRTZ_SIMPLE_TRIGGERS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.TriggerName)
                    .HasColumnName("TRIGGER_NAME")
                    .HasMaxLength(150);

                entity.Property(e => e.TriggerGroup)
                    .HasColumnName("TRIGGER_GROUP")
                    .HasMaxLength(150);

                entity.Property(e => e.RepeatCount).HasColumnName("REPEAT_COUNT");

                entity.Property(e => e.RepeatInterval).HasColumnName("REPEAT_INTERVAL");

                entity.Property(e => e.TimesTriggered).HasColumnName("TIMES_TRIGGERED");

                entity.HasOne(d => d.QrtzTriggers)
                    .WithOne(p => p.QrtzSimpleTriggers)
                    .HasForeignKey<QrtzSimpleTriggers>(d => new { d.SchedName, d.TriggerName, d.TriggerGroup })
                    .HasConstraintName("FK_QRTZ_SIMPLE_TRIGGERS_QRTZ_TRIGGERS");
            });

            modelBuilder.Entity<QrtzSimpropTriggers>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup });

                entity.ToTable("QRTZ_SIMPROP_TRIGGERS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.TriggerName)
                    .HasColumnName("TRIGGER_NAME")
                    .HasMaxLength(150);

                entity.Property(e => e.TriggerGroup)
                    .HasColumnName("TRIGGER_GROUP")
                    .HasMaxLength(150);

                entity.Property(e => e.BoolProp1).HasColumnName("BOOL_PROP_1");

                entity.Property(e => e.BoolProp2).HasColumnName("BOOL_PROP_2");

                entity.Property(e => e.DecProp1)
                    .HasColumnName("DEC_PROP_1")
                    .HasColumnType("numeric(13, 4)");

                entity.Property(e => e.DecProp2)
                    .HasColumnName("DEC_PROP_2")
                    .HasColumnType("numeric(13, 4)");

                entity.Property(e => e.IntProp1).HasColumnName("INT_PROP_1");

                entity.Property(e => e.IntProp2).HasColumnName("INT_PROP_2");

                entity.Property(e => e.LongProp1).HasColumnName("LONG_PROP_1");

                entity.Property(e => e.LongProp2).HasColumnName("LONG_PROP_2");

                entity.Property(e => e.StrProp1)
                    .HasColumnName("STR_PROP_1")
                    .HasMaxLength(512);

                entity.Property(e => e.StrProp2)
                    .HasColumnName("STR_PROP_2")
                    .HasMaxLength(512);

                entity.Property(e => e.StrProp3)
                    .HasColumnName("STR_PROP_3")
                    .HasMaxLength(512);

                entity.Property(e => e.TimeZoneId)
                    .HasColumnName("TIME_ZONE_ID")
                    .HasMaxLength(80);

                entity.HasOne(d => d.QrtzTriggers)
                    .WithOne(p => p.QrtzSimpropTriggers)
                    .HasForeignKey<QrtzSimpropTriggers>(d => new { d.SchedName, d.TriggerName, d.TriggerGroup })
                    .HasConstraintName("FK_QRTZ_SIMPROP_TRIGGERS_QRTZ_TRIGGERS");
            });

            modelBuilder.Entity<QrtzTriggers>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup });

                entity.ToTable("QRTZ_TRIGGERS");

                entity.HasIndex(e => new { e.SchedName, e.CalendarName })
                    .HasName("IDX_QRTZ_T_C");

                entity.HasIndex(e => new { e.SchedName, e.JobGroup })
                    .HasName("IDX_QRTZ_T_JG");

                entity.HasIndex(e => new { e.SchedName, e.NextFireTime })
                    .HasName("IDX_QRTZ_T_NEXT_FIRE_TIME");

                entity.HasIndex(e => new { e.SchedName, e.TriggerGroup })
                    .HasName("IDX_QRTZ_T_G");

                entity.HasIndex(e => new { e.SchedName, e.TriggerState })
                    .HasName("IDX_QRTZ_T_STATE");

                entity.HasIndex(e => new { e.SchedName, e.JobName, e.JobGroup })
                    .HasName("IDX_QRTZ_T_J");

                entity.HasIndex(e => new { e.SchedName, e.MisfireInstr, e.NextFireTime })
                    .HasName("IDX_QRTZ_T_NFT_MISFIRE");

                entity.HasIndex(e => new { e.SchedName, e.TriggerGroup, e.TriggerState })
                    .HasName("IDX_QRTZ_T_N_G_STATE");

                entity.HasIndex(e => new { e.SchedName, e.TriggerState, e.NextFireTime })
                    .HasName("IDX_QRTZ_T_NFT_ST");

                entity.HasIndex(e => new { e.SchedName, e.MisfireInstr, e.NextFireTime, e.TriggerState })
                    .HasName("IDX_QRTZ_T_NFT_ST_MISFIRE");

                entity.HasIndex(e => new { e.SchedName, e.TriggerName, e.TriggerGroup, e.TriggerState })
                    .HasName("IDX_QRTZ_T_N_STATE");

                entity.HasIndex(e => new { e.SchedName, e.MisfireInstr, e.NextFireTime, e.TriggerGroup, e.TriggerState })
                    .HasName("IDX_QRTZ_T_NFT_ST_MISFIRE_GRP");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.TriggerName)
                    .HasColumnName("TRIGGER_NAME")
                    .HasMaxLength(150);

                entity.Property(e => e.TriggerGroup)
                    .HasColumnName("TRIGGER_GROUP")
                    .HasMaxLength(150);

                entity.Property(e => e.CalendarName)
                    .HasColumnName("CALENDAR_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(250);

                entity.Property(e => e.EndTime).HasColumnName("END_TIME");

                entity.Property(e => e.JobData).HasColumnName("JOB_DATA");

                entity.Property(e => e.JobGroup)
                    .IsRequired()
                    .HasColumnName("JOB_GROUP")
                    .HasMaxLength(150);

                entity.Property(e => e.JobName)
                    .IsRequired()
                    .HasColumnName("JOB_NAME")
                    .HasMaxLength(150);

                entity.Property(e => e.MisfireInstr).HasColumnName("MISFIRE_INSTR");

                entity.Property(e => e.NextFireTime).HasColumnName("NEXT_FIRE_TIME");

                entity.Property(e => e.PrevFireTime).HasColumnName("PREV_FIRE_TIME");

                entity.Property(e => e.Priority).HasColumnName("PRIORITY");

                entity.Property(e => e.StartTime).HasColumnName("START_TIME");

                entity.Property(e => e.TriggerState)
                    .IsRequired()
                    .HasColumnName("TRIGGER_STATE")
                    .HasMaxLength(16);

                entity.Property(e => e.TriggerType)
                    .IsRequired()
                    .HasColumnName("TRIGGER_TYPE")
                    .HasMaxLength(8);

                entity.HasOne(d => d.QrtzJobDetails)
                    .WithMany(p => p.QrtzTriggers)
                    .HasForeignKey(d => new { d.SchedName, d.JobName, d.JobGroup })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QRTZ_TRIGGERS_QRTZ_JOB_DETAILS");
            });

            modelBuilder.Entity<RedeemHistories>(entity =>
            {
                entity.HasKey(e => e.RedeemId);

                entity.HasIndex(e => e.CancelledRedeemId)
                    .IsUnique()
                    .HasFilter("([CancelledRedeemId] IS NOT NULL)");

                entity.HasIndex(e => e.PlayerSessionId);

                entity.Property(e => e.RedeemId).ValueGeneratedNever();

                entity.HasOne(d => d.CancelledRedeem)
                    .WithOne(p => p.RedeemHistories)
                    .HasForeignKey<RedeemHistories>(d => d.CancelledRedeemId)
                    .HasConstraintName("FK_dbo.RedeemHistory_dbo.CancelledRedeem_CancelledRedeem");

                entity.HasOne(d => d.PlayerSession)
                    .WithMany(p => p.RedeemHistories)
                    .HasForeignKey(d => d.PlayerSessionId)
                    .HasConstraintName("FK_dbo.RedeemHistory_dbo.PlayerSession_PlayerSessionId");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<ServerCommandLogs>(entity =>
            {
                entity.HasIndex(e => e.PlayerId);

                entity.HasIndex(e => e.ServerId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.PlayerId).HasColumnType("decimal(20, 0)");

                entity.Property(e => e.Source).IsRequired();

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.ServerCommandLogs)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_dbo.ServerCommandLog_dbo.Player_PlayerId");

                entity.HasOne(d => d.Server)
                    .WithMany(p => p.ServerCommandLogs)
                    .HasForeignKey(d => d.ServerId)
                    .HasConstraintName("FK_dbo.ServerCommandLog_dbo.Server_ServerId");
            });

            modelBuilder.Entity<ServerConnectTasks>(entity =>
            {
                entity.HasIndex(e => e.ServerId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TaskType).IsRequired();

                entity.HasOne(d => d.Server)
                    .WithMany(p => p.ServerConnectTasks)
                    .HasForeignKey(d => d.ServerId)
                    .HasConstraintName("FK_dbo.ServerConnectTask_dbo.Servers_ServerId");
            });

            modelBuilder.Entity<ServerFacets>(entity =>
            {
                entity.HasKey(e => new { e.ServerId, e.FacetId });

                entity.HasIndex(e => e.FacetId)
                    .HasName("IX_ServerFacet_FacetId");

                entity.HasIndex(e => e.ServerId)
                    .HasName("IX_ServerFacet_ServerId");

                entity.HasOne(d => d.Facet)
                    .WithMany(p => p.ServerFacets)
                    .HasForeignKey(d => d.FacetId)
                    .HasConstraintName("FK_dbo.ServerFacet_dbo.Facet_FacetId");

                entity.HasOne(d => d.Server)
                    .WithMany(p => p.ServerFacets)
                    .HasForeignKey(d => d.ServerId)
                    .HasConstraintName("FK_dbo.ServerFacet_dbo.Server_ServerId");
            });

            modelBuilder.Entity<ServerSeasons>(entity =>
            {
                entity.HasKey(e => e.SeasonId);

                entity.HasIndex(e => e.GameId);

                entity.HasIndex(e => e.ServerId);

                entity.Property(e => e.SeasonId).ValueGeneratedNever();

                entity.Property(e => e.GameId).HasColumnName("Game_Id");

                entity.Property(e => e.ServerId).HasColumnName("Server_Id");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.ServerSeasons)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK_dbo.Game_dbo.ServerSeasons_SeasonId");

                entity.HasOne(d => d.Server)
                    .WithMany(p => p.ServerSeasons)
                    .HasForeignKey(d => d.ServerId)
                    .HasConstraintName("FK_dbo.Server_dbo.ServerSeasons_Server_Id");
            });

            modelBuilder.Entity<ServerSessions>(entity =>
            {
                entity.HasKey(e => e.SessionId)
                    .HasName("SessionId");

                entity.HasIndex(e => e.GameId);

                entity.HasIndex(e => e.GameId1)
                    .HasName("IX_ServerSession_Game_Game_Id");

                entity.HasIndex(e => e.SeasonId)
                    .HasName("IX_ServerSession_Season_Season _Id");

                entity.Property(e => e.SessionId).ValueGeneratedNever();

                entity.Property(e => e.GameId1).HasColumnName("Game_Id");

                entity.Property(e => e.SeasonId).HasColumnName("Season_Id");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.ServerSessions)
                    .HasForeignKey(d => d.GameId);

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.ServerSessions)
                    .HasForeignKey(d => d.SeasonId)
                    .HasConstraintName("FK_dbo.ServerSeason_dbo.ServerSessions_SeasonId");
            });

            modelBuilder.Entity<ServerSettings>(entity =>
            {
                entity.HasKey(e => e.SettingsId);

                entity.Property(e => e.SettingsId).ValueGeneratedNever();

                entity.Property(e => e.IntervalType).IsRequired();
            });

            modelBuilder.Entity<ServerToLanguages>(entity =>
            {
                entity.HasIndex(e => e.LanguageId)
                    .HasName("IX_Language_Id");

                entity.HasIndex(e => e.ServerServerId)
                    .HasName("IX_ServerToLang_Server_ServerId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LanguageId).HasColumnName("Language_Id");

                entity.Property(e => e.ServerServerId).HasColumnName("Server_ServerId");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.ServerToLanguages)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_dbo.ServerToLanguages_dbo.Languages_LanguageId");

                entity.HasOne(d => d.ServerServer)
                    .WithMany(p => p.ServerToLanguages)
                    .HasForeignKey(d => d.ServerServerId)
                    .HasConstraintName("FK_dbo.ServerToLanguages_dbo.Server_ServerId");
            });

            modelBuilder.Entity<Servers>(entity =>
            {
                entity.HasKey(e => e.ServerId);

                entity.HasIndex(e => e.SettingsId)
                    .IsUnique();

                entity.Property(e => e.ServerId).ValueGeneratedNever();

                entity.Property(e => e.ServerStatementDescriptor)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasDefaultValueSql("(N'rust')");

                entity.Property(e => e.SettingsId).HasColumnName("Settings_Id");

                entity.HasOne(d => d.Settings)
                    .WithOne(p => p.Servers)
                    .HasForeignKey<Servers>(d => d.SettingsId)
                    .HasConstraintName("FK_dbo.Server_dbo.ServerSettings_Settings_Id");
            });

            modelBuilder.Entity<SubscriptionData>(entity =>
            {
                entity.HasIndex(e => e.PlayerSteamId);

                entity.HasIndex(e => e.PlayerSubscriptionId)
                    .IsUnique();

                entity.HasIndex(e => e.VipLevelLevelId)
                    .HasName("IX_SubscriptionData_Level_LevelId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.PlayerSteamId)
                    .HasColumnName("Player_SteamId")
                    .HasColumnType("decimal(20, 0)");

                entity.Property(e => e.VipLevelLevelId).HasColumnName("VipLevel_LevelId");

                entity.HasOne(d => d.PlayerSteam)
                    .WithMany(p => p.SubscriptionData)
                    .HasForeignKey(d => d.PlayerSteamId)
                    .HasConstraintName("FK_dbo.SubscriptionData_dbo.SteamId");

                entity.HasOne(d => d.PlayerSubscription)
                    .WithOne(p => p.SubscriptionData)
                    .HasForeignKey<SubscriptionData>(d => d.PlayerSubscriptionId)
                    .HasConstraintName("FK_dbo.SubscriptionData_dbo.PlayerSubscription_Id");

                entity.HasOne(d => d.VipLevelLevel)
                    .WithMany(p => p.SubscriptionData)
                    .HasForeignKey(d => d.VipLevelLevelId)
                    .HasConstraintName("FK_dbo.SubscriptionData_dbo.LevelId");
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.UserId });

                entity.HasIndex(e => e.IdentityRoleId)
                    .HasName("IX_IdentityRole_Id");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_IdentityUser_Id");

                entity.Property(e => e.IdentityRoleId).HasColumnName("IdentityRole_Id");

                entity.HasOne(d => d.IdentityRole)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.IdentityRoleId)
                    .HasConstraintName("FK_dbo.UserRoles_dbo.Roles_IdentityRole_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.UserRoles_dbo.Users_User_Id");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountClosedTime).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastLoginDateUtc).HasColumnType("datetime");

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");
            });

            modelBuilder.Entity<UsersClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.UsersClaims_dbo.Users_User_Id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
