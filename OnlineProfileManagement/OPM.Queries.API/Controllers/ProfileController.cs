﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using OPM.Queries.API.Queries;
using OPM.Queries.API.Models;

namespace OPM.Queries.API.Controllers
{
    [ApiController]
   
    public class ProfileController : ApiControllerBase
    {



        //private readonly IMediator _mediator;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(
            
            ILogger<ProfileController> logger, IMediator mediator): base(mediator)
        {
            //_mediator = mediator;
            _logger = logger;
        }


        /// <summary>
        /// Get profile by entityId
        /// </summary>
        /// <param name="entityId">entity ID of profile</param>
        /// <returns>profile information</returns>
        [Route("GetProfile")]
        [HttpGet]
        [ProducesResponseType(typeof(ProfileViewModel),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void),(int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetProfileAsync([FromBody]string entityId)
        {

            try
            {
                var profile= await QueryAsync(new GetProfileQuery(entityId));
                return Ok(profile);
            }
            catch
            {
                return NotFound();
            }
           

        }
    }


    public class ApiControllerBase : ControllerBase
    {
        private readonly IMediator _mediator;
        

        public ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException();
        }

       
        protected async Task<TResult> QueryAsync<TResult>(IRequest<TResult> query)
        {
          
                return await _mediator.Send(query);
            
        }

       



    }
}
