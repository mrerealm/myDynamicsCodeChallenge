using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using myDynamicsCodeChallenge.Server.Persistence.Interfaces;
using myDynamicsCodeChallenge.Shared.Aggregates;
using myDynamicsCodeChallenge.Shared.Entities;

namespace myDynamicsCodeChallenge.Server.Persistence
{
    public class ApplicationDBContext: DbContext, IApplicationDBContext
    {
        private IConfiguration Configuration { get; }
        private static string ConnectionString { get; set; }

        public ApplicationDBContext(){}

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options,
            IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        public DbSet<Clause> Clauses { get; set; }
        public DbSet<ListPosition> ListPositions { get; set; }
        public DbSet<ClausePosition> ClausePositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clause>().HasData(
                new Clause { Id = 1, Text = "This is Clause A" },
                new Clause { Id = 2, Text = "This is Clause B" },
                new Clause { Id = 3, Text = "This is Clause C" },
                new Clause { Id = 4, Text = "This is Clause D" }
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

        public void SaveSaveChanges() => base.SaveChanges();

        public DbContext GetContext()
        {
            return (DbContext)Activator.CreateInstance(typeof(ApplicationDBContext));
        }

        public async Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            using (var context = GetContext())
            {
                return await context.Database.ExecuteSqlCommandAsync(sql, parameters);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(ConnectionString);
            optionsBuilder.EnableSensitiveDataLogging().EnableDetailedErrors();
        }
    }
}
