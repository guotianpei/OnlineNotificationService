using System;
using System.Collections.Generic;
using System.Text;

namespace ONP.BackendProcessor.Configuration
{
    public class EmailServiceConfig
    {
        public string MailServerHost { get; set; }
        public string Port { get; set; }
        public string ACCESS_KEY { get; set; }
        public string SECRET_KEY { get; set; }
        public string RegionEndpoint { get; set; }
        public bool EnableSsl { get; set; }
    }
}
