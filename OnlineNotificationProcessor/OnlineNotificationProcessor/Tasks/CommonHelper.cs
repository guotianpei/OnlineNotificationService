using System;
using System.Collections.Generic;
using System.Text;
using OnlineNotificationProcessor.Models;

namespace OnlineNotificationProcessor.Tasks
{
    public class CommonHelper
    {
        internal static List<List<NotificationData>> SplitMany(List<NotificationData> source, int size)
        {
            var sourceChunks = new List<List<NotificationData>>();

            for (int i = 0; i < source.Count; i += size)
                sourceChunks.Add(source.GetRange(i, Math.Min(size, source.Count - i)));

            return sourceChunks;
        }

        
    }
}
