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
using System.Reflection;
using MMS.EventBus.Abstractions;
using ONP.Domain.Models;
using ONP.BackendProcessor.Services;
using ONP.Infrastructure.Repositories.Interfaces;
using ONP.BackendProcessor.Models;




namespace ONP.BackendProcessor.Tasks
{
    //This background service will operate as following:
    //1. consumer notification event from event bus, the event contains user's contact info; 
    //2. compose email by calling email template management service
    public class NotificationBackgroundProcessor : BackgroundService
    {
        private readonly ILogger<NotificationBackgroundProcessor> _logger;
        private readonly BackgroundTaskSettings _settings;
        private readonly IEventBus _eventBus;
        private readonly INotificationRequestRepository _requestRepository;
        private readonly INotificationTemplateRepository _templateRepository;
        private readonly IEntityProfileRepository _entityRepository;
        private readonly IEmailService _emailService;
        private readonly ISecureMessageService _secureMessageService;
        private readonly ISMSService _sMSService;

        public NotificationBackgroundProcessor(IOptions<BackgroundTaskSettings> settings,
            IEventBus eventBus,
            INotificationRequestRepository notificationRequestRepository,
            INotificationTemplateRepository templateRepository,
            IEntityProfileRepository entityRepository,
            IEmailService emailService,
            ISecureMessageService secureMessageService,
            ISMSService sMSService,
            ILogger<NotificationBackgroundProcessor> logger)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _requestRepository = notificationRequestRepository ?? throw new ArgumentNullException(nameof(notificationRequestRepository));
            _templateRepository = templateRepository ?? throw new ArgumentNullException(nameof(templateRepository));
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _secureMessageService = secureMessageService ?? throw new ArgumentNullException(nameof(secureMessageService));
            _sMSService = sMSService ?? throw new ArgumentNullException(nameof(sMSService));

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
                SendPendingNotification();

            }

            _logger.LogDebug("NotificationEventConsumerService background task is stopping.");

            await Task.CompletedTask;
        }

        protected async void SendPendingNotification()
        {

            List<NotificationRequest> lstNotificationRequests = await _requestRepository.GetAndUpdateAsyncAllPendingRequests();

            List<NotificationRequest> listEmailRequest = new List<NotificationRequest>();
            List<NotificationRequest> lstSecureMessageNotReqs = new List<NotificationRequest>();
            List<NotificationRequest> lstSMSNotReqs = new List<NotificationRequest>();

            List<NotificationData> lstEmailData = new List<NotificationData>();
            List<NotificationData> lstSecureMessageData = new List<NotificationData>();
            List<NotificationData> lstSMSData = new List<NotificationData>();


            if (lstNotificationRequests != null && lstNotificationRequests.Any())
            {
                //Split pending request based on channel;
                listEmailRequest = lstNotificationRequests.Where(not => not.ComChannel == ComChannelTypes.Email.ToString()).ToList();
                lstEmailData = await BuildEmailTemplateData(listEmailRequest);
                _emailService.SendMailAsync(lstEmailData);
                //To update Responsitory with Notification Response



                lstSecureMessageNotReqs = lstNotificationRequests.Where(not => not.ComChannel == ComChannelTypes.SecureMessage.ToString()).ToList();
                lstSecureMessageData = await BuildEmailTemplateData(lstSecureMessageNotReqs);
                _secureMessageService.SendMailAsync(lstSecureMessageData);

                lstSMSNotReqs = lstNotificationRequests.Where(not => not.ComChannel == ComChannelTypes.TEXT.ToString()).ToList();             
                lstSMSData = await BuildSMSTemplateData(lstSMSNotReqs);                
                _sMSService.SendSMSAsync(lstSMSData);
            }
            





        }

        //Build template and update stage ToPublish;
        private async Task<List<NotificationData>> BuildEmailTemplateData(List<NotificationRequest> reqList)
        {
            List<NotificationData> data = new List<NotificationData>();
            foreach (var req in reqList)
            {
                try
                {
                    NotificationData _notificationData = new NotificationData();
                    //NotificationTemplate _notificationTemplate = GetNotificatoinTemplate(req.TopicID);
                    var _notificationTemplate = await _templateRepository.GetAsync(req.TopicID);
                    EntityProfile _entprof = await _entityRepository.GetAsync(req.EntityID);

                   
                    BuildTemplate _bt = new BuildTemplate();

                    if (_notificationTemplate != null)
                    {
                        _notificationData.From = _notificationTemplate.From;
                        _notificationData.Subject = _notificationTemplate.Subject;
                        _notificationData.To = _entprof.Email;//chnage based on comchannel
                        EmailTemplateBuild _etb = _bt.BuildEmailTemplate(_notificationTemplate.TemplateFile);
                        PlaceHoldReplacer _phr = new PlaceHoldReplacer();
                        _notificationData.RequestMessageData = _phr.Replace(_etb.MailBody.Trim(), AddPlaceHolder(_entprof), false).Trim(); //construct message with template
                                                                                                                                           //Replace custom tag
                        _notificationData.RequestMessageData = _notificationData.RequestMessageData.Replace("$CustomTag$", req.RequestMessageData).Trim();
                        //Change Notification Request stage;
                        SetToPublishStage(req.TrackingID, _entprof.Email, _notificationData.RequestMessageData);
                    }
                    // Notification Composition has been completed, to send to corresponding notification processor depends on type of communication channel. Update stage=ToPublish
                    //SetToPublishStage(req.TrackingID, _notificationData.To, _notificationData.RequestMessageData);
                    data.Add(_notificationData);
                }
                catch (Exception ex)
                {
                    //need to log exception
                    throw ex;
                }
            }
            return data;
        }

        private async Task<List<NotificationData>> BuildSMSTemplateData(List<NotificationRequest> reqList)
        {
            List<NotificationData> data = new List<NotificationData>();
            foreach (var req in reqList)
            {
                try
                {
                    NotificationData _notificationData = new NotificationData();
                    //NotificationTemplate _notificationTemplate = GetNotificatoinTemplate(req.TopicID);
                    var _notificationTemplate = await _templateRepository.GetAsync(req.TopicID);
                    EntityProfile _entprof = await _entityRepository.GetAsync(req.EntityID);
                    BuildTemplate _bt = new BuildTemplate();

                    if (_notificationTemplate != null)
                    {
                        _notificationData.From = _notificationTemplate.From;
                        _notificationData.Subject = _notificationTemplate.Subject;
                        _notificationData.To = _entprof.Email;//chnage based on comchannel

                        EmailTemplateBuild _etb = _bt.BuildEmailTemplate(_notificationTemplate.TemplateFile);
                        PlaceHoldReplacer _phr = new PlaceHoldReplacer();
                        _notificationData.RequestMessageData = _phr.Replace(_etb.MailBody.Trim(), AddPlaceHolder(_entprof), false).Trim(); //construct message with template
                                                                                                                                           //Replace custom tag
                        _notificationData.RequestMessageData = _notificationData.RequestMessageData.Replace("$CustomTag$", req.RequestMessageData).Trim();
                    }
                    // Notification Composition has been completed, to send to corresponding notification processor depends on type of communication channel. Update stage=ToPublish
                    //SetToPublishStage(req.TrackingID, _notificationData.To, _notificationData.RequestMessageData);
                    data.Add(_notificationData);
                }
                catch (Exception ex)
                {
                    //need to log exception
                    throw ex;
                }
            }
            return data;
        }

        private async void SetToPublishStage(Guid trackingId, string recipient, string messageBody)
        {
            var requestToUpdate = await _requestRepository.GetById(trackingId);
            if (requestToUpdate != null)
            {
                requestToUpdate.SetToPublishStage(recipient, messageBody);
                await _requestRepository.UnitOfWork.SaveEntitiesAsync();
            }
        }

        private async void RequestCompleted(List<NotificationResponse> lstResponseData)
        {

        }

        private async void SetCompletedStage(Guid trackingId, string responseCode, string responseDescription)
        {
            var requestToUpdate = await _requestRepository.GetById(trackingId);
            if (requestToUpdate != null)
            {
                requestToUpdate.SetCompletedStage(responseCode, responseDescription);
                await _requestRepository.UnitOfWork.SaveEntitiesAsync();
            }
        }

        List<PlaceHolder> AddPlaceHolder(EntityProfile entProf)
        {
            List<PlaceHolder> lstPH = new List<PlaceHolder>();
            PlaceHolder _pholder;
            foreach (PropertyInfo prop in entProf.GetType().GetProperties())
            {
                _pholder = new PlaceHolder(string.Format("${0}$", prop.Name), prop.GetValue(entProf).ToString());
                lstPH.Add(_pholder);
            }
            return lstPH;
        }



    }
}
