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
   

    public class SecureMessageService : ISecureMessageService
    {

        private readonly SecureMessageServiceConfig _configuration;
        //private readonly INotificationTemplateRepository _templateRepository;
        //private readonly IEntityProfileRepository _entityRepository;

        public SecureMessageService(IOptions<SecureMessageServiceConfig> smConfig)
        //INotificationTemplateRepository templateRepository,
        //IEntityProfileRepository entityRepository)
        {
            _configuration = smConfig.Value;
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

    }
