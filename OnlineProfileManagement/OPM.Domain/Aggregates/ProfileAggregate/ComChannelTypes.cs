using System;
using System.Linq;
using System.Collections.Generic;
using OPM.Domain.SeekWork;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPM.Domain.Aggregates.ProfileAggregate
{
    [NotMapped]
    public class ComChannelTypes : Enumeration
    {
        public static ComChannelTypes Email = new ComChannelTypes(1, nameof(Email).ToLowerInvariant());
        public static ComChannelTypes SecureMessage = new ComChannelTypes(2, nameof(SecureMessage).ToLowerInvariant());
        public static ComChannelTypes TEXT = new ComChannelTypes(3, nameof(TEXT).ToLowerInvariant());

        public ComChannelTypes(int id, string name) : base(id, name)
        {

        }

        public static IEnumerable<ComChannelTypes> List() =>
               new[] { Email, SecureMessage, TEXT };


    }
}
