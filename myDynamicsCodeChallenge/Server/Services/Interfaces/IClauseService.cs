using System;
using System.Collections.Generic;
using myDynamicsCodeChallenge.Shared.Enumerations;
using myDynamicsCodeChallenge.Shared.Models;

namespace myDynamicsCodeChallenge.Server.Services.Interfaces
{
    public interface IClauseService
    {
        public IEnumerable<ClauseModel> Reset();
        public IEnumerable<ClauseModel> GetAll();
        public IEnumerable<ClauseModel> MoveClauseToPosition(int id, Position position);
    }
}
