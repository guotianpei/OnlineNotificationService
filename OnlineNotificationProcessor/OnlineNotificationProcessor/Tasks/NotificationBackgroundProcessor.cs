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
using OnlineNotificationProcessor.Models;
using System.Reflection;

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

        //public NotificationBackgroundProcessor(IOptions<BackgroundTaskSettings> settings,
        //    IEventBus eventBus,
        //    ILogger<NotificationBackgroundProcessor> logger)
        //{
        //    _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        //    _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        //    _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        //}

        public NotificationBackgroundProcessor()
        {
           

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SendPendingNotification();

            //_logger.LogDebug("NotificationEventConsumerService is starting.");

            //stoppingToken.Register(() => _logger.LogDebug("#1 NotificationEventConsumerService background task is stopping."));

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogDebug("NotificationEventConsumerService background task is doing background work.");
            //    //Looking for pending notification "NotPublished"
            //    //Get email notification from . Call template management service, compose msg body
            //    //Depends on the type of notification, publish either email or SMS Into notification table.

            //}

            //_logger.LogDebug("NotificationEventConsumerService background task is stopping.");

            await Task.CompletedTask;
        }

        protected void SendPendingNotification()
        {
            List<NotificationRequest> lstNotificationLogs = GetNotificationRequest();

            List<NotificationRequest> lstEmailNotReqs = new List<NotificationRequest>();
            List<NotificationRequest> lstSMSNotReqs = new List<NotificationRequest>();

            if (lstNotificationLogs != null && lstNotificationLogs.Count > 0)
            {
                lstEmailNotReqs=lstNotificationLogs.FindAll(not => not.ComChannel == "email").ToList();
                lstSMSNotReqs= lstNotificationLogs.FindAll(not => not.ComChannel == "sms").ToList();

                SendEmailNotification(lstEmailNotReqs);
                SendSMSNotification(lstSMSNotReqs);               
            }
        }
        protected void SendEmailNotification(List<NotificationRequest> lstnotreq)
        {
            NotificationProcess notprocess = new NotificationProcess(new SendEmailNotification());          

            if (notprocess != null)
            {
                notprocess.SendNotification(lstnotreq);
            }
        }

        protected void SendSMSNotification(List<NotificationRequest> lstnotreq)
        {
            NotificationProcess notprocess = new NotificationProcess(new SendSMSNotification());

            if (notprocess != null)
            {
                notprocess.SendNotification(lstnotreq);
            }
        }

        //protected void SendSMSNotification(NotificationRequest notreq)
        //{
        //    NotificationProcess notprocess = null;
        //    switch (notreq.ComChannel.ToLower().Trim())
        //    {
        //        case "email":
        //            notprocess = new NotificationProcess(new SendEmailNotification());
        //            break;
        //        case "sms":
        //            notprocess = new NotificationProcess(new SendSMSNotification());
        //            break;
        //    };

        //    if (notprocess != null)
        //    {
        //        notprocess.SendNotification(notreq);
        //    }
        //}

        List<NotificationRequest> GetNotificationRequest()
        {
            List<NotificationRequest> lstNotificationRequests = new List<NotificationRequest>();
            NotificationRequest _notificationrequest = null;

            _notificationrequest = new NotificationRequest();
            _notificationrequest.ComChannel = "Email";
            _notificationrequest.NotificationStage = "Pending";
            _notificationrequest.RequestMessageData = "";
            _notificationrequest.TopicID = "123";
            //_notificationrequest.MessageBody = "<h1>Amazon SES Test</h1>" +
            //    "<p>This email was sent through the " +
            //    "<a href='https://aws.amazon.com/ses'>Amazon SES</a> SMTP interface " +
            //    "using the .NET System.Net.Mail library.</p>";

            //_notificationLog.Recipient = "muhilan.murugavel@dxc.com,muhilan.cm@gmail.com";
            //_notificationrequest.Recipient = "muhilan.murugavel@dxc.com,tguo4@dxc.com,srimallika.terupally@dxc.com,vrampalli2@dxc.com,cyang48@dxc.com";
            //_notificationrequest.NotificationStatge = 0;
            lstNotificationRequests.Add(_notificationrequest);

            _notificationrequest = new NotificationRequest();
            _notificationrequest.ComChannel = "SMS";
            _notificationrequest.NotificationStage = "Pending";
            _notificationrequest.RequestMessageData = "";
            _notificationrequest.TopicID = "321";
            lstNotificationRequests.Add(_notificationrequest);

            return lstNotificationRequests;
        }

    }
}
