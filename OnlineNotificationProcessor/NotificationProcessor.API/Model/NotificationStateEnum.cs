using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationProcessor.API.Model
{
    public enum NotificationStateEnum
    {
        RequestReceived = 0, //Initial notification request received.
        RequestProcessing = 1, //Notification Background processor retrieve initial request, compose message body and retrieve contact details.
        ToPublished = 2, // Notification Composition has been completed, has been sent to corresponding notification processor depends on type of communication channel.
        InProgress = 3, // Before corresponding notification processor sending Email/SMS/SM notification
        Published = 4, // Success
        PublishedFailed = 5, // Failed.

    }
}
