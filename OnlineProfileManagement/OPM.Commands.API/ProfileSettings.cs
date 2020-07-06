using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPM.Commands.API
{
    //Setup project level configuration.
    //Add strongly typed configuration using build-in configuration provider
    public class ProfileSettings
    {
        public string ProfileDBConnectionString { get; set; }
        public string EventBusConnection { get; set; }
    }
}
