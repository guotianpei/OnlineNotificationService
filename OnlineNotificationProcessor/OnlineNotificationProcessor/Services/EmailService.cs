using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Reflection;
using Microsoft.Extensions.Options;
using ONP.BackendProcessor.Models;
using System.Threading.Tasks;
using ONP.BackendProcessor.Configuration;
using ONP.Infrastructure.Repositories.Interfaces;
using ONP.Domain.Models;
using ONP.BackendProcessor.Tasks;


namespace ONP.BackendProcessor.Services
{
    public class EmailService : IEmailService
    {

        private readonly EmailServiceConfig _configuration;
        //private readonly INotificationTemplateRepository _templateRepository;
        //private readonly IEntityProfileRepository _entityRepository;

        public EmailService(IOptions<EmailServiceConfig> emailConfig) 
            //INotificationTemplateRepository templateRepository,
            //IEntityProfileRepository entityRepository)
        {
            _configuration = emailConfig.Value;
            //_templateRepository = templateRepository;
            //_entityRepository = entityRepository;
        }

        public async Task<List<NotificationResponse>> SendMailAsync(List<NotificationData> lstPendingReq)
        {
            SmtpClient client = new SmtpClient();
            client.Host = _configuration.MailServerHost;
            client.Credentials = new System.Net.NetworkCredential(_configuration.ACCESS_KEY, _configuration.SECRET_KEY);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            try
            {
                foreach (var mailrequest in lstPendingReq)
                {
                    var data = await BuildEmailTemplateData(mailrequest);                    
                    message.From = new System.Net.Mail.MailAddress(data.From);
                    message.Subject = data.Subject;
                    message.Body = data.RequestMessageData;
                    //message.Priority = mailrequest.Priority;
                   // message.IsBodyHtml = mailrequest.IsBodyHtml;
                    message.To.Add(data.To);
                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                        //Capture response and update to NotificationResponse 
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }



        private async Task<NotificationData> BuildEmailTemplateData(NotificationRequest req)
        {
            try
            {
                NotificationData _notificationData = new NotificationData();
                //NotificationTemplate _notificationTemplate = GetNotificatoinTemplate(req.TopicID);
                var _notificationTemplate = await _templateRepository.GetAsync(req.TopicID);

                EntityProfile _entprof =await _entityRepository.GetAsync(req.EntityID);
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

    }
}
