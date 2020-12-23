using ONP.Domain;
using ONP.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
 

namespace ONP.Infrastructure.Responsitories
{
    class NotificationRequestRepository : GenericRepository<NotificationRequest>, INotificationRequest
    {
        //https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design
        //Define one repository per aggregate
        private readonly NotificationContext _context;

        public NotificationRequestRepository(NotificationContext context) : base(context)
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
        public Task<IEnumerable<NotificationRequest>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<NotificationRequest> GetAsync(NotificationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<NotificationRequest> GetById(int id)
        {
            throw new NotImplementedException();
        }

       

        public Task<NotificationRequest> UpdateAsync(NotificationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
