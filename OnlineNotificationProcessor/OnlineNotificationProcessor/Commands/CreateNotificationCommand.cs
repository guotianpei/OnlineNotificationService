using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;
using ONP.Domain.Models;
using MediatR;

namespace ONP.BackendProcessor.Commands
{
   
    
    
    public class CreateNotificationCommand : IRequest<bool>
    {
        [DataMember]
        public Guid TrackingID { get; private set; }
        [DataMember]
        public string EntityID { get; private set; }
        [DataMember]
        public ComChannelTypes ComChannel { get; private set; }
        [DataMember]
        public string RequestMessageData { get; private set; }

        public int TopicID { get; private set; }

        public CreateNotificationCommand(Guid trackingID, string entityID, ComChannelTypes comChannel, string requestMessageData, int topicID)
        {


        }
    }
}
