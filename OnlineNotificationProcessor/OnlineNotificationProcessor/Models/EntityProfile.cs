using System;
using System.Collections.Generic;
using System.Text;

namespace ONP.BackendProcessor.Models
{
    public class EntityProfile
    {
        public string ID { get; set; }
        public string EntityID { get; set; }
        public string EntityName { get; set; }
        public string EntityType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public string SMS { get; set; }
        public string SecureMessage { get; set; }
        public DateTime LasteUpdateDateTime { get; set; }
    }
}
