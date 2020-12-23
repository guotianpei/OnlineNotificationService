using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ONP.BackendProcessor.Configuration;
using MMS.EventBus;
using MMS.EventBus.Abstractions;
using ONP.BackendProcessor.Models;
using ONP.BackendProcessor.Services;

namespace ONP.BackendProcessor.Tasks
{
    class SendEmailNotification : ISendNotification
    {
        private readonly ILogger<SendEmailNotification> _logger;
        private readonly BackgroundTaskSettings _settings;
        private readonly IEventBus _eventBus;
        private readonly EmailService _ems;

        public SendEmailNotification(IOptions<BackgroundTaskSettings> settings,
            IEventBus eventBus,
            ILogger<SendEmailNotification> logger)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        public SendEmailNotification()
        {
            _ems = new EmailService();
        }

        public NotificationResponse SendNotification(NotificationData notdata)
        {
            NotificationResponse _emailResponse = new NotificationResponse();
            try
            {
                EmailService ems = new EmailService();
                EmailRequest emr = null;

                emr = new EmailRequest();
                emr.To = notdata.To;
                emr.subject = notdata.Subject;
                emr.Message = notdata.RequestMessageData;
                emr.From = notdata.From;
                emr.IsBodyHtml = true;
                emr.Priority = System.Net.Mail.MailPriority.Normal;
                _ems.SendMail(emr);
                _emailResponse.NotificationStage = NotificationStage.Success;
                _emailResponse.ID = notdata.ID;
            }
            catch (Exception ex)
            {
                _emailResponse.NotificationStage = NotificationStage.Failed;
                _emailResponse.Error = ex;                
            }

            return _emailResponse;
        }


        public List<NotificationResponse> SendBulkNotification(List<NotificationData> lstNotificationData)
        {
            int chunkSize = 80;//TO-DO: from appsetting configuration.
            
            // Split "mailingList" to multiple lists of "chunkSize" size.
            var mailingChunks = CommonHelper.SplitMany(mailingList, chunkSize);

        }
        void UpdatNotification()
        {

        }

    }
}
