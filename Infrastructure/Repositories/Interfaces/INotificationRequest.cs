using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Infrastructure.Responsitories;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    interface INotificationRequest : IGenericRepository<NotificationRequest>, IRepository<NotificationRequest>
    {
        Task<NotificationRequest> AddAsync(NotificationRequest request);
        Task<NotificationRequest> UpdateAsync(NotificationRequest request);
        Task<NotificationRequest> GetAsync(NotificationRequest request);
    }
}
