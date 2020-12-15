using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Infrastructure.Responsitories;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    
    interface INotificationTransactionLog : IGenericRepository<NotificationTransactionLog>, IRepository<NotificationTransactionLog>
    {
        Task<NotificationTransactionLog> AddAsync(NotificationTransactionLog request);
        Task<NotificationTransactionLog> UpdateAsync(NotificationTransactionLog request);
        Task<NotificationTransactionLog> GetAsync(int transactionID);


    }
}
