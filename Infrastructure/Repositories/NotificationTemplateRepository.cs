using ONP.Domain;
using ONP.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
 

namespace ONP.Infrastructure.Responsitories
{
    class NotificationTemplateRepository : GenericRepository<NotificationTemplate>, INotificationTemplate
    {
        //https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design
        //Define one repository per aggregate
        private readonly NotificationProcessorContext _context;

        public NotificationTemplateRepository(NotificationProcessorContext context) : base(context)
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

        public Task<NotificationTemplate> AddAsync(NotificationTemplate request)
        {
            throw new NotImplementedException();
        }

        public Task<NotificationTemplate> GetAsync(int templateID)
        {
            throw new NotImplementedException();
        }

        public Task<NotificationTemplate> UpdateAsync(NotificationTemplate request)
        {
            throw new NotImplementedException();
        }
    }
}
