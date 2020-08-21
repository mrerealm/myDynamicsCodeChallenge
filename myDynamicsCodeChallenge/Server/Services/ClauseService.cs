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

        public IEnumerable<ClauseModel> GetAllClauses()
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
            _context.Clauses.FromSqlRaw("EXECUTE dbo.ResetClauses");
            _context.Save();
            return GetAllClauses();
        }

        public IEnumerable<ClauseModel> MoveClauseToPosition(int id, Position position)
        {
            _context.Clauses.FromSqlRaw(MoveClauseToPositionSpCall, id, (int)position);
            _context.Save();
            return GetAllClauses();
        }
    }
}
