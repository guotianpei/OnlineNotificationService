using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineNotificationProcessor.Models;

namespace OnlineNotificationProcessor.Tasks
{
    class SendSMSNotification : ISendNotification
    {
        public SendSMSNotification()
        {


        }

        public NotificationResponse SendNotification(NotificationData notdata)
        {
            return null;
        }
        
    }
}
