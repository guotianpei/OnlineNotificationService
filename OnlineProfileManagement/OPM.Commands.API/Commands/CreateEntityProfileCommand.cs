using System;
using MediatR;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;
using OPM.Domain.Aggregates.ProfileAggregate;
using OPM.Commands.API.Models;

namespace OPM.Commands.API.Commands
{


    // DDD and CQRS patterns comment: Note that it is recommended to implement immutable Commands
    // In this case, its immutability is achieved by having all the setters as private
    // plus only being able to update the data just once, when creating the object through its constructor.
    // References on Immutable Commands:  
    // http://cqrs.nu/Faq
    // https://docs.spine3.org/motivation/immutability.html 
    // http://blog.gauffin.org/2012/06/griffin-container-introducing-command-support/
    // https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/how-to-implement-a-lightweight-class-with-auto-implemented-properties

    [DataContract]
    public class CreateEntityProfileCommand :IRequest<bool>
    {
        [DataMember]
        public string EntityId { get; private set; }


        [DataMember]
        public string EntityName { get; private set; }

        [DataMember]
        public string EntityType { get; private set; }

        [DataMember]
        public string FirstName { get; private set; }

        [DataMember]
        public string LastName { get; private set; }

        [DataMember]
        public string Status { get; private set; }

        [DataMember]
        public int ResourceID { get; private set; }

        [DataMember]
        public string ResourceName { get; private set; }


        [DataMember]
        private readonly List<ComChannel> _comChannels;


        [DataMember]
        public IEnumerable<ComChannel> ComChannels => _comChannels;

        public CreateEntityProfileCommand()
        { }


        public CreateEntityProfileCommand(string entityId, string entityName,
            string entityType, string firstName, string lastName, string resourceName,
            List<ComChannel> comChannels) : this()
        {
            EntityId = entityId;
            EntityName = entityName;
            EntityType = entityType;
            FirstName = firstName;
            LastName = lastName;
            ResourceName = resourceName;
            _comChannels = comChannels;
        }

    }
}
