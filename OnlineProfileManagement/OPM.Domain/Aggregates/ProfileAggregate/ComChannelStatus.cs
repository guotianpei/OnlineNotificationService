using System;
using System.Linq;
using System.Collections.Generic;
using OPM.Domain.SeekWork;

namespace OPM.Domain.Aggregates.ProfileAggregate
{

    public class ComChannelStatus : Enumeration
    {
        public static ComChannelStatus VALIDATING = new ComChannelStatus(1, nameof(VALIDATING).ToLowerInvariant());
        public static ComChannelStatus VALIDATED = new ComChannelStatus(2, nameof(VALIDATED).ToLowerInvariant());
        public static ComChannelStatus TERMINATED = new ComChannelStatus(3, nameof(TERMINATED).ToLowerInvariant());

        public ComChannelStatus(int id, string name) : base(id, name)
        {

        }

        public static IEnumerable<ComChannelStatus> List() =>
               new[] { VALIDATING, VALIDATED, TERMINATED };


    }
}
