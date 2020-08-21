using System;
using Microsoft.EntityFrameworkCore;
using myDynamicsCodeChallenge.Shared.Aggregates;
using myDynamicsCodeChallenge.Shared.Entities;

namespace myDynamicsCodeChallenge.Server.Persistence.Interfaces
{
    public interface IApplicationDBContext
    {
        public DbSet<Clause> Clauses { get; set; }
        public DbSet<ListPosition> ListPositions { get; set; }
        public DbSet<ClausePosition> ClausePositions { get; set; }
    }
}
