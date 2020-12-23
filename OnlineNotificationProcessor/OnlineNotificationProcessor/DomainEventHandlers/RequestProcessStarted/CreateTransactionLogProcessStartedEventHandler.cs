using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ONP.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using ONP.Infrastructure;

namespace ONP.BackendProcessor
{
    public class CreateTransactionLogProcessStartedEventHandler: IAsyncNotificationHandler<RequestProcessStartedDomainEvent>
    {

    }
}
