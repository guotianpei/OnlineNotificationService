using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OPM.Queries.API.Extension;

namespace OPM.Queries.API.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("----- Handling query {QueryName} ({@Query})", request.GetGenericTypeName(), request);
            var response = await next();
            _logger.LogInformation("----- Query {QueryName} handled - response: {@Response}", request.GetGenericTypeName(), response);

            return response;
        }
    }
}
