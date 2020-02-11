using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Whose_Turn.Context.Entities;

namespace Whose_Turn.Context
{
    public class DatabaseContext : DbContext
    {
        private static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include };

        public DatabaseContext()
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
            optionsBuilder.UseSqlite("Data Source=Database/WhoseTurnDb.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(entity =>
            {
                entity.Property(e => e.AssignedTo)
                    .HasConversion(s => JsonConvert.SerializeObject(s, JsonSerializerSettings),
                        s => JsonConvert.DeserializeObject<List<Guid>>(s));

                entity.HasKey(e => e.Id);
                entity.HasIndex(t => t.Id);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => new { e.Id, e.Email })
                .IsUnique(true);

                entity.HasOne(e => e.MyHouseHold)
                    .WithMany(e => e.Users)
                    .HasForeignKey(e => e.HouseHoldId);
            });

            modelBuilder.Entity<HouseHold>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => new { e.Id })
                .IsUnique(true);
            });
        }
    }
}
