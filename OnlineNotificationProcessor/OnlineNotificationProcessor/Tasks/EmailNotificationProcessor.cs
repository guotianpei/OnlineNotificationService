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
    class EmailNotificationProcessor : BackgroundService
    {
        private readonly ILogger<EmailNotificationProcessor> _logger;
        private readonly BackgroundTaskSettings _settings;
        private readonly IEventBus _eventBus;


        public EmailNotificationProcessor(IOptions<BackgroundTaskSettings> settings,
            IEventBus eventBus,
            ILogger<EmailNotificationProcessor> logger)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("EmailNotificationEventConsumerService is starting.");

            stoppingToken.Register(() => _logger.LogDebug("#1 EmailNotificationEventConsumerService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("EmailNotificationEventConsumerService background task is doing background work.");
                //Get email notification event, send out email.
                
            }

            _logger.LogDebug("EmailNotificationEventConsumerService background task is stopping.");

            await Task.CompletedTask;
        }
    }
}
