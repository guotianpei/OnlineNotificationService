using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class ComChannel
    {
        public string ComChannelType { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public ComChannelTypes ComChannelTypes => (!string.IsNullOrEmpty(ComChannelType)) ? ComChannelTypes.FromDisplayName<ComChannelTypes>(ComChannelType.ToLower().Trim()) : null;

        public string Value { get; set; }

        public bool Enabled { get; set; }

        public int Preference { get; set; }
    }
}
