﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ONP.BackendProcessor.Models;
using ONP.Domain.Models;

namespace ONP.BackendProcessor.Services
{
    public interface ISMSService
    {
        public Task<List<NotificationResponse>> SendSMSAsync(List<NotificationData> lstSMSRequest);
    }
}
