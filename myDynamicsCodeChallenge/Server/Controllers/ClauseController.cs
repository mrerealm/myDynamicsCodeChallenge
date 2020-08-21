using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myDynamicsCodeChallenge.Server.Services.Interfaces;
using myDynamicsCodeChallenge.Shared.Enumerations;
using myDynamicsCodeChallenge.Shared.Models;

namespace myDynamicsCodeChallenge.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClauseController : ControllerBase
    {
        private readonly IClauseService _clauseService;
        private readonly ILogger<ClauseController> _logger;

        public ClauseController(ILogger<ClauseController> logger,
            IClauseService clauseService)
        {
            _logger = logger;
            _clauseService = clauseService;
        }

        [HttpGet]
        [Route("reset")]
        public async Task<IEnumerable<ClauseModel>> Reset()
        {
            try
            {
                return await _clauseService.ResetAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetBaseException().Message, ex);
            }
            return null;
        }

        [HttpGet]
        public async Task<IEnumerable<ClauseModel>> Get()
        {
            try
            {
                return await _clauseService.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetBaseException().Message, ex);
            }
            return null;
        }

        [HttpGet("{id}/{position}")]
        [Route("move")]
        public async Task<IEnumerable<ClauseModel>> MoveClauseToPosition(int id, int position)
        {
            try
            {
                return await _clauseService.MoveClauseToPositionAsync(id, (Position)position);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetBaseException().Message, ex);
            }
            return null;
        }

    }
}
