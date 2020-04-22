using System;
using OPM.Domain.SeekWork;

namespace OPM.Domain.Aggregates.ProfileAggregate
{
    public class ProfileComChannel :Entity
    {

        // DDD Patterns comment
        // Using private fields, allowed since EF Core 1.1, is a much better encapsulation
        // aligned with DDD Aggregates and Domain Entities (Instead of properties and property collections)
        //private string _entityID;
         
        public string Value { get; private set; }
        private bool _enabled;
        private int _preference;
        private DateTime _termDate;
        public ComChannelStatus Status { get; private set; }

        public ComChannelTypes ChannelType { get; private set; }



        protected ProfileComChannel() { }

        public ProfileComChannel(ComChannelTypes type, string value, bool enabled, int preference, ComChannelStatus status)
        {
            //input Validation
            ChannelType = type;
            Value = value;
            _enabled = enabled;
            _preference = preference;
            Status = status;

        }

        //If the communication channel exists and is active.
        public bool IsEqualTo(ComChannelTypes type, string value)
        {
            return Value == value && ChannelType == type && _termDate == null;
        }

        public bool IsTypeExist(ComChannelTypes type)
        {
            return ChannelType == type && _termDate == null;
        }

        

        public int GetPreference()
        {
            return _preference;
        }

        public void SetDisable(int id)
        {
            _enabled = false;
        }

        public void Terminate(int id)
        {
            _termDate = DateTime.UtcNow.Date;
        }

        public void SetPreference(int id)
        {
            

        }

        

         
    }
}
