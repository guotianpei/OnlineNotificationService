using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMS.EventBus.Abstractions;
using OPM.Commands.API.IntegrationEvents.Events;
using OPM.ONP.Infrastructure.Repositories.Interfaces;
using OPM.Infrastructure.Idempotency;
using Microsoft.Extensions.Logging;
using MediatR;
using Serilog.Context;
using OPM.Commands.API.Commands;
using OPM.Commands.API.Extensions;

namespace OPM.Commands.API.IntegrationEvents.EventHandling
{
    //This event handler handles integration event from event bus. 
    //After TradingPartner registered in TPA or  other entity enrolled/registered,  needs to create new entity profile for communication.
    public class EntityRegisteredIntegrationEventHandler : IIntegrationEventHandler<EntityRegisteredIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EntityRegisteredIntegrationEventHandler> _logger;

        public EntityRegisteredIntegrationEventHandler(IMediator mediator,
            ILogger<EntityRegisteredIntegrationEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //Integration event handler which starts the create entity process/command.
        public async Task Handle(EntityRegisteredIntegrationEvent @event)
        {
            bool commandResult = false;
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                if (@event.RequestID != Guid.Empty)
                {
                    using (LogContext.PushProperty("IdentifiedCommandId", @event.RequestID))
                    {
                        var createEntityProfileCommand = new CreateEntityProfileCommand(
                            @event.EntityID, @event.EntityName, @event.EntityName,
                            @event.FirstName, @event.LastName, @event.RequestorApp,
                            @event.ProfileComChannels);
                        var requestCommand = new IdentifiedCommand<CreateEntityProfileCommand, bool>(createEntityProfileCommand, @event.RequestID);

                        _logger.LogInformation(
                       "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                       requestCommand.GetGenericTypeName(),
                       nameof(requestCommand.Id),
                       requestCommand.Id,
                       requestCommand);

                        commandResult= await _mediator.Send(requestCommand);

                        if (commandResult)
                        {
                            _logger.LogInformation("----- CreateEntityProfileCommand suceeded - RequestId: {RequestId}", @event.RequestID);
                        }
                        else
                        {
                            _logger.LogWarning("CreateEntityProfileCommand failed - RequestId: {RequestId}", @event.RequestID);
                        }

                    }
                }

               
            }
        }
    }
}
