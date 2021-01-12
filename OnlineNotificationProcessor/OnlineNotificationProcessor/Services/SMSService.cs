using ONP.BackendProcessor.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ONP.Domain.Models;

namespace ONP.BackendProcessor.Services
{
    class SMSService : ISMSService
    {
        
        public Task<NotificationResponse> SendSMSAsync(EmailRequest mailrequest)
        {
            throw new NotImplementedException();
        }

        public Task<List<NotificationResponse>> SendSMSAsync(List<NotificationData> lstSMSRequest)
        {
            throw new NotImplementedException();
        }
    }
}
