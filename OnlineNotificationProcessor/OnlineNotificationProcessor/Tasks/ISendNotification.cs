using ONP.BackendProcessor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ONP.BackendProcessor.Tasks
{
    public interface ISendNotification
    {
        NotificationResponse SendNotification(NotificationData notlog);
        //void UpdateNotificationStatus();

    }
}
