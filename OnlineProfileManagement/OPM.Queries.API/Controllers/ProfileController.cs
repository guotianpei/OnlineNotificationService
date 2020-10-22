using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using OPM.Queries.API.Queries;
using OPM.Queries.API.Models;
using OPM.Infrastructure.Repositories.QueryRequests;

namespace OPM.Queries.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ApiControllerBase
    {

        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger, IMediator mediator) : base(mediator)
        {
            _logger = logger;
        }


        //TO-DO:
        //1. It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
        //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
        //2. API HealthCheck

        /// <summary>
        /// Get profile by entityId
        /// </summary>
        /// <param name="entityId">entity ID of profile</param>
        /// <returns>profile information</returns>
        //[Route("GetProfile")]
        //[HttpGet]
        //[ProducesResponseType(typeof(ProfileViewModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        //public async Task<ActionResult> GetProfileAsync([FromBody] string entityId)
        //{

        //    try
        //    {
        //        var profile = await QueryAsync(new GetProfileQuery(entityId));
        //        return Ok(profile);
        //    }
        //    catch
        //    {
        //        return NotFound();
        //    }


        //}

        [HttpGet("{entityId}")]
        //[Route("GetEntityProfile")]
        public async Task<ActionResult> GetEntityProfileAsync(string entityId)
        {
            var profile = await QueryAsync(new GetProfileQuery(entityId));

            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        //GetProfileComChannelQuery request

        [HttpPost]
        [Route("GetProfileComChannelByIDs")]
        public async Task<ActionResult> GetProfileComChannelByIDs(GetProfileComChannelQuery request)
        {
            var profile = await QueryAsync(request);

            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
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
