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

        public void SendMailAsync(List<NotificationData> lstEmailRequest)
        {
            throw new NotImplementedException();
        }
    }
}