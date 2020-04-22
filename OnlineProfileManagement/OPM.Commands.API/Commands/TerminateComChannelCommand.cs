using System;
using MediatR;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;
using OPM.Domain.Aggregates.ProfileAggregate;

namespace OPM.Commands.API.Commands
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TerminateComChannelCommand :IRequest<bool>
    {
        
        [DataMember]
        public int ComChannelID { get; private set; }

        public TerminateComChannelCommand(int comChannelId)
        {
            ComChannelID = comChannelId;
        }

        
    }
 
}
