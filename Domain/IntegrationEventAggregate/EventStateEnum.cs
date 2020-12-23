using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONP.Domain
{
    public enum EventStateEnum
    {
        NotPublished = 0,
        InProgress = 1,
        Published = 2,
        PublishedFailed = 3
    }
}
