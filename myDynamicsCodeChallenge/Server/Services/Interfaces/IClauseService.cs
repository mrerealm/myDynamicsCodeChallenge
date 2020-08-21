using System;
using System.Collections.Generic;
using myDynamicsCodeChallenge.Shared.Enumerations;
using myDynamicsCodeChallenge.Shared.Models;

namespace myDynamicsCodeChallenge.Server.Services.Interfaces
{
    public interface IClauseService
    {
        public void Reset();
        public List<ClauseModel> GetClauses();
        public void MoveClauseToPosition(int id, Position position);
    }
}
