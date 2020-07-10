using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineNotificationProcessor.Configuration;
using MMS.EventBus;
using MMS.EventBus.Abstractions;

namespace OnlineNotificationProcessor.Tasks
{
    //This background service will operate as following:
    //1. consumer notification event from event bus, the event contains user's contact info; 
    //2. compose email by calling email template management service
    public class NotificationBackgroundProcessor : BackgroundService
    {
        private readonly ILogger<NotificationBackgroundProcessor> _logger;
        private readonly BackgroundTaskSettings _settings;
        private readonly IEventBus _eventBus;

        public NotificationBackgroundProcessor(IOptions<BackgroundTaskSettings> settings,
            IEventBus eventBus,
            ILogger<NotificationBackgroundProcessor> logger)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("NotificationEventConsumerService is starting.");

            stoppingToken.Register(() => _logger.LogDebug("#1 NotificationEventConsumerService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("NotificationEventConsumerService background task is doing background work.");
                //Looking for pending notification "NotPublished"
                //Get email notification from . Call template management service, compose msg body
                //Depends on the type of notification, publish either email or SMS Into notification table.

            }

            _logger.LogDebug("NotificationEventConsumerService background task is stopping.");

            await Task.CompletedTask;
        }
    }
}
