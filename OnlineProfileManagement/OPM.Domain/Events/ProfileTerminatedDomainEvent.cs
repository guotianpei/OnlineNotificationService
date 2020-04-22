using System;
using MediatR;
using OPM.Domain.Aggregates.ProfileAggregate;
namespace OPM.Domain.Events
{
    public class ProfileTerminatedDomainEvent : INotification
    {
        public EntityProfile Profile { get; }

        public ProfileTerminatedDomainEvent(EntityProfile profile)
        {
            Profile = profile;
        }
    }
}
