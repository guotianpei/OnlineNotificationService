using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONP.Domain.Seedwork;

namespace ONP.Domain.Models
{
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
