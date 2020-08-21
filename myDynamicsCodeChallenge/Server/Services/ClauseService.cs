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
        private readonly string MoveClauseToPositionSpCall = "[dbo].[MoveClauseToPosition] @Id, @Position";
        private readonly string ResetClausesSpCall = "[dbo].[ResetClauses]";

        public ClauseService(IApplicationDBContext clauseDbContext)
        {
            _context = clauseDbContext;
        }

        public IEnumerable<ClauseModel> GetAll()
        {
            var results = _context.Clauses
                .Join(_context.ClausePositions,
                    c => c.Id,
                    cp => cp.ClauseId,
                    (c, cp) => new ClauseModel
                    {
                        Id = c.Id,
                        Text = c.Text,
                        Position = (Position)cp.PositionId
                    });

            return results;
        }

        public IEnumerable<ClauseModel> Reset()
        {
            _context.Clauses.FromSqlRaw(ResetClausesSpCall);
            return GetAll();
        }

        public IEnumerable<ClauseModel> MoveClauseToPosition(int id, Position position)
        {
            var result = _context.Clauses.FromSqlRaw(MoveClauseToPositionSpCall,
                new SqlParameter("Id", id),
                new SqlParameter("Position", (int)position));
            return GetAll();
        }
    }
}
