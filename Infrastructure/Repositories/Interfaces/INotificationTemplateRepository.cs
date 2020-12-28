using System;
using System.Collections.Generic;
using System.Text;
using ONP.Domain.Models;
using ONP.Infrastructure.Responsitories;
using System.Threading.Tasks;
using ONP.Domain.Seedwork;

namespace ONP.Infrastructure.Repositories.Interfaces
{
    public interface INotificationTemplateRepository : IGenericRepository<NotificationTemplate>, IRepository<NotificationTemplate>
    {
        Task<NotificationTemplate> AddAsync(NotificationTemplate request);
        Task<NotificationTemplate> UpdateAsync(NotificationTemplate request);
        Task<NotificationTemplate> GetAsync(int templateID);


    }
}
