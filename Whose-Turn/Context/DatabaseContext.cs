using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Whose_Turn.Context.Entities;
using Whose_Turn.Extensions;

namespace Whose_Turn.Context
{
    public class DatabaseContext : DbContext
    {
        private static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings
            {NullValueHandling = NullValueHandling.Include};

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the To-do item
        /// </summary>
        public DbSet<Todo> Todos { get; set; }

        /// <summary>
        /// Gets or sets a list of house holds
        /// </summary>
        public DbSet<HouseHold> HouseHolds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                throw new ApplicationException("Datacontext has not been configured");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(t => t.Id);

                entity.Property(e => e.AssignedTo)
                    .HasJsonConversion();

                entity.HasOne(e => e.Preferences)
                    .WithOne(e => e.Todo)
                    .HasForeignKey<Todo>(e => e.PreferencesId);

                entity.HasOne(e => e.UsersHouseHold)
                    .WithMany(e => e.Todos)
                    .HasForeignKey(e => e.HouseHoldId);
            });

            modelBuilder.Entity<TodoPreferences>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(t => t.Id);

                entity.Property(e => e.Repeat)
                    .HasConversion(s => Enum.GetName(s.GetType(), s), v => Enum.Parse<TodoRepeat>(v));

                entity.Property(e => e.Priority)
                    .HasConversion(s => Enum.GetName(s.GetType(), s), v => Enum.Parse<TodoPriority>(v));

                entity.Property(e => e.Privacy)
                    .HasConversion(s => Enum.GetName(s.GetType(), s), v => Enum.Parse<TodoPrivacy>(v));
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => new {e.Id, e.Email})
                    .IsUnique(true);

                entity.HasOne(e => e.MyHouseHold)
                    .WithMany(e => e.Users)
                    .HasForeignKey(e => e.HouseHoldId);
            });

            modelBuilder.Entity<HouseHold>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => new {e.Id})
                    .IsUnique(true);
            });
        }
    }
}