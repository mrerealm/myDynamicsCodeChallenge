using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using myDynamicsCodeChallenge.Server.Persistence;
using myDynamicsCodeChallenge.Server.Persistence.Interfaces;
using myDynamicsCodeChallenge.Server.Services.Interfaces;
using myDynamicsCodeChallenge.Shared.Entities;
using myDynamicsCodeChallenge.Shared.Enumerations;
using myDynamicsCodeChallenge.Shared.Models;

namespace myDynamicsCodeChallenge.Server.Services
{
    public class ClauseService: IClauseService
    {
        private readonly IApplicationDBContext _context;
        private readonly string MoveClauseToPositionSpCall = "EXEC MoveClauseToPosition @Id, @Position";
        private readonly string ResetClausesSpCall = "EXEC ResetClauses";

        public ClauseService(IApplicationDBContext clauseDbContext)
        {
            _context = clauseDbContext;
        }

        public async Task<IEnumerable<ClauseModel>> GetAllAsync()
        {
            var results = await _context.Clauses
                .AsNoTracking()
                .Join(_context.ClausePositions,
                    c => c.Id,
                    cp => cp.ClauseId,
                    (c, cp) => new ClauseModel
                    {
                        Id = c.Id,
                        Text = c.Text,
                        Position = (Position)cp.PositionId,
                        Order = cp.Order.HasValue ? cp.Order.Value : 1
                    }).ToListAsync();

            return results;
        }

        public async Task<IEnumerable<ClauseModel>> ResetAsync()
        {
             await _context.ExecuteSqlCommandAsync(ResetClausesSpCall);

            // this is the best option - however it is not working on my environment
            _context.Clauses
                .FromSqlRaw(ResetClausesSpCall)
                .AsNoTracking();
            return await GetAllAsync();
        }

        public async Task<IEnumerable<ClauseModel>> MoveClauseToPositionAsync(int id, Position position)
        {
            await _context.ExecuteSqlCommandAsync(MoveClauseToPositionSpCall,
                    new SqlParameter("Id", id),
                    new SqlParameter("Position", (int)position));

            // this is the best option - however it is not working on my environment
            _context.Clauses
                .FromSqlRaw(MoveClauseToPositionSpCall,
                    new SqlParameter("Id", id),
                    new SqlParameter("Position", (int)position))
                .AsNoTracking();
            return await GetAllAsync();
        }
    }
}
