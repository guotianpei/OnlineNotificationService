using System;
using System.Collections.Generic;
using System.Text;
using ONP.Domain;
using ONP.Infrastructure.Responsitories;
using System.Threading.Tasks;

namespace ONP.Infrastructure.Repositories.Interfaces
{
    interface INotificationTemplate : IGenericRepository<NotificationTemplate>, IRepository<NotificationTemplate>
    {
        Task<NotificationTemplate> AddAsync(NotificationTemplate request);
        Task<NotificationTemplate> UpdateAsync(NotificationTemplate request);
        Task<NotificationTemplate> GetAsync(int templateID);


    }
}
