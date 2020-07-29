using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OPM.Domain.SeekWork;

namespace OPM.Domain.Aggregates.ProfileAggregate
{
    public class ProfileComChannel : Entity, IAggregateRoot
    {

        // DDD Patterns comment
        // Using private fields, allowed since EF Core 1.1, is a much better encapsulation
        // aligned with DDD Aggregates and Domain Entities (Instead of properties and property collections)
        //private string _entityID;

        //private Guid _userID;
        //public Guid UserID
        //{
        //    get { return _userID; }
        //    set { _userID = value; }
        //}
        public string EntityID { get; set; }
        public string ComChannel { get; set; }
        public string Value { get; private set; }
        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        private int _preference;
        public int Preference
        {
            get { return _preference; }
            set { _preference = value; }
        }
        private DateTime? _termDate;
        public DateTime? TermDate
        {
            get { return _termDate; }
            set { _termDate = value; }
        }
        public ComChannelStatus Status { get; private set; }
        public ComChannelTypes ChannelType
        {
            get
            {
                return (!string.IsNullOrEmpty(ComChannel)) ? ComChannelTypes.FromDisplayName<ComChannelTypes>(ComChannel.ToLower().Trim()) : null;
            }
            private set { }
        }
        protected ProfileComChannel() { }

        public ProfileComChannel(ComChannelTypes type, string value, bool enabled, int preference, ComChannelStatus status)
        {
            //input Validation
            ChannelType = type;
            Value = value;
            _enabled = enabled;
            _preference = preference;
            Status = status;
            ComChannel = type.Name;
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
