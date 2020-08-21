using System;
using System.Collections.Generic;
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
        public IEnumerable<ClauseModel> Reset()
        {
            try
            {
                return _clauseService.Reset();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetBaseException().Message, ex);
            }
            return null;
        }

        [HttpGet]
        public IEnumerable<ClauseModel> Get()
        {
            try
            {
                return _clauseService.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetBaseException().Message, ex);
            }
            return null;
        }

        [HttpGet("{id}/{position}")]
        [Route("move")]
        public IEnumerable<ClauseModel> MoveClauseToPosition(int id, int position)
        {
            try
            {
                return _clauseService.MoveClauseToPosition(id, (Position)position);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetBaseException().Message, ex);
            }
            return null;
        }

    }
}
