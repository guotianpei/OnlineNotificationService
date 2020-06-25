using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace OnlineNotificationProcessor.Tasks
{
    class SMSNotificationEventConsumerService : BackgroundService
    {
        public SMSNotificationEventConsumerService()
        {
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
