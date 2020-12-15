using OnlineNotificationProcessor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineNotificationProcessor.Tasks
{
    public interface ISendNotification
    {
        NotificationResponse SendNotification(NotificationData notlog);
        //void UpdateNotificationStatus();

    }
}
