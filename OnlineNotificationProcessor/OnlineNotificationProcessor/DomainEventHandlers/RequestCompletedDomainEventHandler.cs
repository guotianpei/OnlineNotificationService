using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using ONP.Domain.Events;
using ONP.Domain.Models;
using System.Threading;
using System.Threading.Tasks;
using ONP.Infrastructure.Repositories.Interfaces;
using ONP.Infrastructure.Repositories;

namespace ONP.BackendProcessor.DomainEventHandlers
{
    public class RequestCompletedDomainEventHandler : IAsyncNotificationHandler<RequestCompletedDomainEvent>
    {
        private readonly INotificationRequestRepository _requestRepository;

        public RequestCompletedDomainEventHandler(INotificationRequestRepository notificationRequestRepository)
        {
            _requestRepository = notificationRequestRepository ?? throw new ArgumentNullException(nameof(notificationRequestRepository));
        }
        public async Task Handle(RequestCompletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var requestToUpdate = await _requestRepository.GetAsyncById(notification.TrackingID);

            if (requestToUpdate != null)
            {
                requestToUpdate.SetCompletedStage(notification.ResponseCode, notification.ResponseDesc.ToString());
            }
            await _requestRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        }
    }
}
