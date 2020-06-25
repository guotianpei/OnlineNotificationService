﻿using System;
using MediatR;
using OPM.Domain.Aggregates.ProfileAggregate;
namespace OPM.Domain.Events
{
    public class ComChannelAddedDomainEvent : INotification
    {

        public string EntityId { get; }
        public string EntityName { get; }
        public string FName { get; }
        public string LName { get; }
        public ProfileComChannel ComChannel { get; }

        public ComChannelAddedDomainEvent(string entityId, string entityName, string fName, string lName, ProfileComChannel channel)
        {
            EntityId = entityId;
            EntityName = entityName;
            FName = fName;
            LName = lName;
            ComChannel = channel;

        }
    }
}
