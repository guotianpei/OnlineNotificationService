using System;
using System.Collections.Generic;
using System.Text;
using ONP.Domain.Models;
using ONP.Infrastructure.Responsitories;
using System.Threading.Tasks;
using ONP.Domain.Seedwork;

namespace ONP.Infrastructure.Repositories.Interfaces
{
    public interface INotificationRequestRepository : IGenericRepository<NotificationRequest>, IRepository<NotificationRequest>
    {
        //Task<NotificationRequest> AddAsync(NotificationRequest request);
        //void Update(NotificationRequest request);
        //public  Task<NotificationRequest> GetAsyncById(Guid trackingId);
        Task<List<NotificationRequest>> GetAndUpdateAsyncAllPendingRequests();
      


    }
}
