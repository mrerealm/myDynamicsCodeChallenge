using myDynamicsCodeChallenge.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myDynamicsCodeChallenge.Server.Persistence;
using myDynamicsCodeChallenge.Server.Services.Interfaces;
using myDynamicsCodeChallenge.Shared.Enumerations;
using myDynamicsCodeChallenge.Shared.Entities;

namespace myDynamicsCodeChallenge.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClauseController : ControllerBase
    {
        private readonly IClauseService _clauseService;

        public ClauseController(IClauseService clauseService)
        {
            _clauseService = clauseService;
        }

        [HttpGet]
        [Route("reset")]
        public IActionResult Reset()
        {
            _clauseService.Reset();
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var results = _clauseService.GetClauses();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("{id}/{position}")]
        public IActionResult MoveClauseToPosition(int id, int position)
        {
            try
            {
                _clauseService.MoveClauseToPosition(id, (Position)position);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

    }
}
