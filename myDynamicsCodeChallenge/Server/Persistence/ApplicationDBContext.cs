using System;
using Microsoft.EntityFrameworkCore;
using myDynamicsCodeChallenge.Server.Persistence.Interfaces;
using myDynamicsCodeChallenge.Shared.Aggregates;
using myDynamicsCodeChallenge.Shared.Entities;

namespace myDynamicsCodeChallenge.Server.Persistence
{
    public class ApplicationDBContext: DbContext, IApplicationDBContext
    {
        public ApplicationDBContext() { }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Clause> Clauses { get; set; }
        public DbSet<ListPosition> ListPositions { get; set; }
        public DbSet<ClausePosition> ClausePositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clause>().HasData(
                new Clause { Id = 1, Text = "A"},
                new Clause { Id = 2, Text = "B"},
                new Clause { Id = 3, Text = "C"},
                new Clause { Id = 4, Text = "D"}
                );

            modelBuilder.Entity<ListPosition>()
                .HasData(
                    new Clause { Id = 1, Text = "Left"},
                    new Clause { Id = 2, Text = "Right"}
                );

            modelBuilder.Entity<ClausePosition>(cp => cp.HasNoKey());

            modelBuilder.Entity<ClausePosition>()
                .HasData(
                    new ClausePosition { ClauseId = 1,  PositionId = 1 },
                    new ClausePosition { ClauseId = 1,  PositionId = 1 },
                    new ClausePosition { ClauseId = 1,  PositionId = 1 },
                    new ClausePosition { ClauseId = 1,  PositionId = 1 }
                );
        }

        public void Save() => base.SaveChanges();

        public DbContext GetContext()
        {
            return (DbContext)Activator.CreateInstance(typeof(ApplicationDBContext));
        }
    }
}
