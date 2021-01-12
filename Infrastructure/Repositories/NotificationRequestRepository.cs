using ONP.Domain.Models;
using ONP.Domain.Seedwork;
using ONP.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace ONP.Infrastructure.Responsitories
{
    class NotificationRequestRepository : GenericRepository<NotificationRequest>, INotificationRequestRepository
    {
        //https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design
        //Define one repository per aggregate
        private readonly NotificationProcessorContext _context;

        public NotificationRequestRepository(NotificationProcessorContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

       
       


        //Get all stage=0
        public async Task<List<NotificationRequest>> GetAndUpdateAsyncAllPendingRequests()
        {                     
            //To-DO: Replace with SP update original request status and insert transaction log.
            return await _context.NotificationRequests
                .Where(n => n.NotificationStage == NotificationStageEnum.RequestReceived)
                .OrderBy(o => o.RequestDatetime)
                .ToListAsync();                
        }

       



        public async Task<NotificationRequest> GetAsyncById(Guid trackingId)
        {
            var request = await _context.NotificationRequests
               .Include(n => n.NotificationTransactionLog)
               .Where(n => n.TrackingID == trackingId)
               .SingleOrDefaultAsync();
            return request;
        }
       

       
    }
}
