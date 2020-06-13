using System;
using System.Collections.Generic;
using System.Linq;
using OPM.Domain.SeekWork;
using OPM.Domain.Events;
  

namespace OPM.Domain.Aggregates.ProfileAggregate
{
    public class EntityProfile : Entity, IAggregateRoot
    {


        // DDD Patterns comment
        // Using private fields, allowed since EF Core 1.1, is a much better encapsulation
        // aligned with DDD Aggregates and Domain Entities (Instead of properties and property collections)
        //Per DDD patterns, you should encapsulate domain behavior and rules within the entity class itself,
        //so it can control invariants, validations, and rules when accessing any collection. Therefore, it is
        //not a good practice in DDD to allow public access to collections of child entities or value objects.
        //Instead, you want to expose methods that control how and when your fields and property collections can
        //be updated, and what behavior and actions should occur when that happens.

        //Any change toward to child item(profile communication channel), is the change
        //to the Profile, must go thru ProfileRepository.Update(Profile)
        private string _entityID;

        private string _entityName;

        private string _entityType;

        private string _firstName;

        private string _lastName;

         
        //private DateTime _termDate;

        private string _status;

        private int _resourceID;

       

        private readonly List<ProfileComChannel> _profileComChannels;

        public IReadOnlyCollection<ProfileComChannel> ProfileComChannels => _profileComChannels;

        protected EntityProfile()
        { }

        public EntityProfile(string entityId, string entityName, string entityType, string fName, string lName,  string status, int resourceId ) :this()
        {
            // Initializations ...

            _entityID = entityId;
            _entityName = entityName;
            _entityType = entityType;
            _firstName = fName;
            _lastName = lName;
            _status = status;
            _resourceID = resourceId;

            //Add ProfileCreatedDomainEvent for any side effect to be raised by the event.
            ProfileCreatedDomainEvent();
        }



        // DDD Patterns comment
        // This Profile AggregateRoot's method "AddProfileComChannel()" should be the only way to add profile
        //communication channel to the profile, from command handlers or WebAPI controllers,
        // so any behavior and validations are controlled by the AggregateRoot 
        // in order to maintain consistency between the whole Aggregate.
        //https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/net-core-microservice-domain-model

        public void AddProfileComChannel(ComChannelTypes type, string value, bool enabled, int preference)
        {

            //TO-DO:
            //Validation logic all business rules, one value per channel type.
            //Same type/value-> do nothing;
            //Existing type/different value -> add new & term old;
            //if type doesn't exist

            ComChannelStatus status = ComChannelStatus.VALIDATING;
            //if is same.
            var existingChannel = _profileComChannels.SingleOrDefault(c => c.IsEqualTo(type, value));
            //see if the channel exist.
            var existingType = _profileComChannels.SingleOrDefault(c => c.IsTypeExist(type));

            if (type==ComChannelTypes.SecureMessage)
            {
                    status = ComChannelStatus.VALIDATED;
            }

            //if the channel type (email/text) exists, but different value, we terminate existing one.
            if (existingChannel == null && existingType != null)
            {
                TerminateProfileComChannel(existingChannel.Id);
            }

            //if type doesn't exist
            if (existingType == null)
            {
                //Add new channel.
                var comChannel = new ProfileComChannel(type, value, enabled, preference, status);

                _profileComChannels.Add(comChannel);
                //Raise side effect-- for to be validated channel, to send out validation text/email.
                if (type == ComChannelTypes.Email || type == ComChannelTypes.TEXT)
                {
                    ValidatingComChannelDomainEvent(comChannel);
                }

            }

        }
        private void ValidatingComChannelDomainEvent(ProfileComChannel channel)
        {
            var validdatingcomChannelDomainEvent = new ValidatingComChannelDomainEvent(_entityID, _entityName, _firstName, _lastName, channel);
            AddDomainEvent(validdatingcomChannelDomainEvent);
        }


        public void TerminateProfileComChannel(int id)
        {
            var existingChannel = _profileComChannels.Where(c => c.Id == id).SingleOrDefault();
             if(existingChannel!=null)
            {
                existingChannel.Terminate(existingChannel.Id);
            }
         

        }

       

        private void ProfileCreatedDomainEvent()
        {
            var profileCreatedDomainEvent = new ProfileCreatedDomainEvent(this);
            AddDomainEvent(profileCreatedDomainEvent);
        }


    }
}
