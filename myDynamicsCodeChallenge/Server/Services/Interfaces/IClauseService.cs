using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using myDynamicsCodeChallenge.Shared.Enumerations;
using myDynamicsCodeChallenge.Shared.Models;

namespace myDynamicsCodeChallenge.Server.Services.Interfaces
{
    public interface IClauseService
    {
        public Task<IEnumerable<ClauseModel>> ResetAsync();
        public Task<IEnumerable<ClauseModel>> GetAllAsync();
        public Task<IEnumerable<ClauseModel>> MoveClauseToPositionAsync(int id, Position position);
    }
}
