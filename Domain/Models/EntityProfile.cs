﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONP.Domain.Seedwork;

namespace ONP.Domain.Models
{
    public class EntityProfile : Entity, IAggregateRoot
    {
        public int ID { get; set; }
        public string EntityID { get; set; }
        public string EntityName { get; set; }
        public string EntityType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? Active { get; set; }
        public string Email { get; set; }
        public string SMS { get; set; }
        public string SecureMassage { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
    }
}
