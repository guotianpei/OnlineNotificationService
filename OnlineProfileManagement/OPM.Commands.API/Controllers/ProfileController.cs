﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using OPM.Commands.API.Commands;

namespace OPM.Commands.API.Controllers
{
    [ApiController]
    [Route("[WriteController]")]
    public class ProfileController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(
            IMediator mediator,
            ILogger<ProfileController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        //TO-DO:
        //1. Create identified command, then send
        //_mediator.Send(command)
        //2. API HealthCheck
        //3. Logging request
        [Route("CreateProfile")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateEntityProfileAsync([FromBody]CreateEntityProfileCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {

           
            bool commandResult = false;

            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var request = new IdentifiedCommand<CreateEntityProfileCommand, bool>(command, guid);

                commandResult= await _mediator.Send(command);
            }

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();
        }


    }
}