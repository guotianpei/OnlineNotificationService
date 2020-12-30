using ONP.Domain.Models;
using ONP.BackendProcessor.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;
using ONP.Infrastructure.Repositories.Interfaces;
using System.Threading.Tasks;


namespace ONP.BackendProcessor.Tasks
{
    public class NotificationProcess
    {
        private readonly ILogger<NotificationProcess> _logger;
        private readonly INotificationRequestRepository _requestRepository;
        private readonly INotificationTemplateRepository _templateRepository;

        ISendNotification _sendnotificatoin;
        public NotificationProcess(ISendNotification objSN,
            INotificationRequestRepository notificationRequestRepository,
            ILogger<NotificationProcess> logger)
        {
            _sendnotificatoin = objSN;
        }
        public async void SendNotification(List<NotificationRequest> lstPendingReq)
        {
            NotificationResponse notificationResponse;

            foreach (NotificationRequest notificationReq in lstPendingReq)
            {
                var notificationData = await BuildNotificationData(notificationReq);
                notificationResponse = _sendnotificatoin.SendNotification(notificationData);

                if (notificationResponse != null )
                {
                   SetCompletedStage(notificationResponse.TrackingID, notificationResponse.ResponseCode, notificationResponse.ResponseMessage);
                }
            }            
        }

        public async Task<NotificationData> BuildNotificationData(NotificationRequest req)
        {
            try
            {
                NotificationData _notificationData = new NotificationData();
                //NotificationTemplate _notificationTemplate = GetNotificatoinTemplate(req.TopicID);
                var _notificationTemplate =await _templateRepository.GetAsync(req.TopicID);
                
                EntityProfile _entprof = GetEntityProfile(req.EntityID);
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
                SetToPublishStage(req.TrackingID, _notificationData.To, _notificationData.RequestMessageData);
                return _notificationData;
            }
            catch (Exception ex)
            {
                //need to log exception
                throw ex;
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

        EntityProfile GetEntityProfile(string EntityID)
        {
            EntityProfile _entprof = new EntityProfile();
            _entprof.FirstName = "Scott";
            _entprof.LastName = "George";
            _entprof.Email = "Scott.George@gainwelltechnologies.com";
            return _entprof;
        }

        private async void SetToPublishStage(Guid trackingId, string recipient, string messageBody)
        {
            var requestToUpdate =await _requestRepository.GetById(trackingId);
            if(requestToUpdate!=null)
            {
                requestToUpdate.SetToPublishStage(recipient, messageBody);
                await _requestRepository.UnitOfWork.SaveEntitiesAsync();
            }             
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

    }
}
