using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;

namespace OPM.Queries.API.Controllers
{
    [ApiController]
    [Route("[ReadController]")]
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
        //1. It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
        //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
        //2. API HealthCheck

        /// <summary>
        /// Get profile by entityId
        /// </summary>
        /// <param name="entityId">entity ID of profile</param>
        /// <returns>profile information</returns>
        [Route("GetProfile")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProfile(string entityId)
        {
            await _mediator.Publish(new EmptyCommand());
            var queryResult = await _mediator.Send(new EmptyQuery());
            return View();
        }
    }
}
