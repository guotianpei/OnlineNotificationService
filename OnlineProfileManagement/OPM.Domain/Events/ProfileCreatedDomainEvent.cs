using System;
using MediatR;
using OPM.Domain.Aggregates.ProfileAggregate;
namespace OPM.Domain.Events
{
    public class ProfileCreatedDomainEvent : INotification
    {

        public EntityProfile Profile { get; }




        public ProfileCreatedDomainEvent(EntityProfile profile)
        {
            Profile = profile;
        }
    }
}
