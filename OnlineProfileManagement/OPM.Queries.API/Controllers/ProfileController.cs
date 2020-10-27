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
using OPM.Queries.API.Validations;
using FluentValidation.AspNetCore; 

namespace OPM.Queries.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ApiControllerBase
    {

        private readonly ILogger<ProfileController> _logger;
        private readonly ILogger<GetProfileQueryValidator> _valLogger;
        private readonly ILogger<GetProfileComChannelQueryValidator> _valComChannelQueryLogger;

        
        public ProfileController(ILogger<ProfileController> logger, ILogger<GetProfileQueryValidator> valLogger,
           ILogger<GetProfileComChannelQueryValidator> valComChannelQueryLogger,
           IMediator mediator) : base(mediator)
        {
            _logger = logger;
            _valLogger = valLogger;
            _valComChannelQueryLogger = valComChannelQueryLogger;
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
            GetProfileQuery query = new GetProfileQuery(entityId);
            var validator = new GetProfileQueryValidator(_valLogger);
            var results = validator.Validate(query);
            results.AddToModelState(ModelState, null);

            if (!results.IsValid)
            {
                return BadRequest(ModelState);
                //use the below code for detailed validation messages
                //return new BadRequestObjectResult(new
                //{
                //    ErrorCode = "Your validation error code",
                //    Message = results
                //});
            }

            var profile = await QueryAsync(query);
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
            //Validate the query request
            //GetProfileQuery query = new GetProfileQuery(entityId);
            var validator = new GetProfileComChannelQueryValidator(this._valComChannelQueryLogger);
            var results = validator.Validate(request);
            results.AddToModelState(ModelState, null);

            if (!results.IsValid)
            {
                return BadRequest(ModelState);
            }

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
