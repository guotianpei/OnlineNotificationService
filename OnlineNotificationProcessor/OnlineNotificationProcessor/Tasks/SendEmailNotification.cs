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
using ONP.Domain.Models;
using ONP.Infrastructure.Repositories.Interfaces;

namespace ONP.BackendProcessor.Tasks
{
    class SendEmailNotification : ISendNotification
    {
        private readonly ILogger<SendEmailNotification> _logger;
        private readonly BackgroundTaskSettings _settings;
        private readonly IEventBus _eventBus;
        private readonly IEmailService _emailService;

        private readonly EmailServiceConfig _configuration;
        


        private readonly INotificationRequestRepository _requestRepository;

        public SendEmailNotification(IOptions<BackgroundTaskSettings> settings,
            IEventBus eventBus,
            ILogger<SendEmailNotification> logger,
            IEmailService emailService)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _emailService= emailService ?? throw new ArgumentNullException(nameof(emailService));

        }

        

        public async Task<NotificationResponse> SendNotification(NotificationData notdata)
        {
            NotificationResponse _emailResponse = new NotificationResponse();
            try
            {
                //Update stage
                SetPublishingStage(notdata.TrackingID);
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
                _emailResponse.NotificationStage = NotificationStageEnum.Published;//Need to capture actual response.
                _emailResponse.TrackingID = notdata.TrackingID;
            }
            catch (Exception ex)
            {
                _emailResponse.NotificationStage = NotificationStageEnum.PublishFailed;
                _emailResponse.Error = ex;                
            }

            return _emailResponse;
        }



        private async void SetPublishingStage(Guid trackingId)
        {
            var requestToUpdate = await _requestRepository.GetById(trackingId);
            if (requestToUpdate != null)
            {
                requestToUpdate.SetPublishingStage();
                await _requestRepository.UnitOfWork.SaveEntitiesAsync();
            }
        }

    }
}
