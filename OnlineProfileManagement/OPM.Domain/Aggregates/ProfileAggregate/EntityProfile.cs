﻿using System;
using System.Collections.Generic;
using System.Linq;
using OPM.Domain.SeekWork;
using OPM.Domain.Events;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string EntityID
        {
            get { return _entityID; }
            set { _entityID = value; }
        }

        private string _entityName;
        public string EntityName
        {
            get { return _entityName; }
            set { _entityName = value; }
        }

        private string _entityType;
        public string EntityType
        {
            get { return _entityType; }
            set { _entityType = value; }
        }

        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private DateTime _effDate;
        public DateTime EffDate
        {
            get { return _effDate; }
            set { _effDate = value; }
        }
        private DateTime? _termDate;
        public DateTime? TermDate
        {
            get { return _termDate; }
            set { _termDate = value; }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            private set { _status = value; }
        }
        public ProfileResource ProfileResource { get; set; }
        private int _resourceID;
        [ForeignKey("ProfileResource")]
        public int ResourceID
        {
            get { return _resourceID; }
            set { _resourceID = value; }
        }

        private readonly List<ProfileComChannel> _profileComChannels;

        public IReadOnlyCollection<ProfileComChannel> ProfileComChannels => _profileComChannels;

        protected EntityProfile()
        {
            _profileComChannels = new List<ProfileComChannel>();
        }

        public EntityProfile(string entityId, string entityName, string entityType, string fName, string lName, string status, int resourceId) : this()
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


        public void AddOrUpdateProfileComChannel(string entityID, ComChannelTypes type, string value, bool enabled, int preference)
        {
            //Validation logic all business rules, one value per channel type.
            //Same type/value-> do nothing;
            //Existing type/different value -> term old & add new  ;
            //if type doesn't exist
            //if is same.

            if (string.IsNullOrEmpty(entityID))
            {
                AddProfileComChannel(type, value);
            }
            else
            {
                //var existingChannel = _profileComChannels.SingleOrDefault(c => c.IsEqualTo(type, value));
                //see if the channel exist.
                var existingType = _profileComChannels.SingleOrDefault(c => c.IsTypeExist(type));
                //if the channel type (email/text) exists, but different value, we terminate existing one.
                if (existingType != null)
                {
                    TerminateProfileComChannel(existingType.Id);
                    AddProfileComChannel(type, value);
                }

                //if type doesn't exist
                if (existingType == null)
                {
                    AddProfileComChannel(type, value);
                }
            }
        }


        private void ComChannelAddedDomainEvent(ProfileComChannel channel)
        {
            var comChanneAddedDomainEvent = new ComChannelAddedDomainEvent(_entityID, _entityName, _firstName, _lastName, channel);
            AddDomainEvent(comChanneAddedDomainEvent);
        }

        public void UpdateProfileStatus(string status)
        {
            _status = status;
        }

        public void TerminateProfileComChannel(int id)
        {
            var existingChannel = _profileComChannels.Where(c => c.Id == id).SingleOrDefault();
            if (existingChannel != null)
            {
                existingChannel.Terminate(existingChannel.Id);
            }
        }

        private void AddProfileComChannel(ComChannelTypes type, string value)
        {
            //Add new channel.
            ComChannelStatus status = ComChannelStatus.VALIDATING;
            if (type.Name == ComChannelTypes.SecureMessage.ToString().Trim())
            {
                status = ComChannelStatus.VALIDATED;
            }
            var comChannel = new ProfileComChannel(type, value, true, 1, status);

            _profileComChannels.Add(comChannel);

            //Raise side effect-- for to be validated channel, to send out validation text/email.
            ComChannelAddedDomainEvent(comChannel);

        }



        private void ProfileCreatedDomainEvent()
        {
            var profileCreatedDomainEvent = new ProfileCreatedDomainEvent(this);
            AddDomainEvent(profileCreatedDomainEvent);
        }


    }
}
