using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;

namespace OPM.Queries.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        public async Task<IActionResult> Index()
        {
            await _mediator.Publish(new EmptyCommand());
            var queryResult = await _mediator.Send(new EmptyQuery());
            return View();
        }
    }
}
