using System;
using System.Collections.Generic;
using System.Linq;
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

        public ClauseService(IApplicationDBContext clauseDbContext)
        {
            _context = clauseDbContext;
        }

        public void Reset()
        {
            _context.Clauses.FromSqlRaw("EXECUTE dbo.ResetClauses");
        }

        public List<ClauseModel> GetClauses(ClausePositions position)
        {
            var results = _context.Clauses
                .Join(_context.ClausePositions,
                    c => c.Id,
                    cp => cp.ClauseId,
                    (c,cp) => new ClauseModel
                    {
                        Id = c.Id,
                        Text = c.Text,
                        Position = (ClausePositions)cp.PositionId
                    }).ToList();

            return results;
        }

        public void MoveClauseToPosition(int id, ClausePositions position)
        {
            _context.Clauses.FromSqlRaw("EXECUTE dbo.MoveClauseToPosition @Id, @Position", id, (int)position);
        }
    }
}
