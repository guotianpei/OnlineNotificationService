using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using ONP.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace ONP.BackendProcessor.DomainEventHandlers
{
    public class PublishFailedDomainEventHandler : IAsyncNotificationHandler<PublishFailedDomainEvent>
    {
        public PublishFailedDomainEventHandler()
        { }
        public async Task Handle(PublishFailedDomainEvent notification, CancellationToken cancellationToken)
        {
            //TBD-- Send email notification to admin.
        }
    }
}
