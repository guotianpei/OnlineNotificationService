using ONP.Domain;
using ONP.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ONP.Domain.Seedwork;
using ONP.Domain.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;



namespace ONP.Infrastructure.Responsitories
{
    class NotificationTemplateRepository : GenericRepository<NotificationTemplate>, INotificationTemplateRepository
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

        public async Task<NotificationTemplate> GetAsync(int topicId)
        {
            
            return await _context.NotificationTemplates
                .Include(n => n.TopicName)
                .Include(n => n.Subject)
                .Include(n => n.TemplateFile)
                .Include(n => n.From)
                .Where(n => n.ID == topicId && n.TerminateDate> DateTime.Today)
                .FirstOrDefaultAsync();                  
              
        }

        public Task<NotificationTemplate> UpdateAsync(NotificationTemplate request)
        {
            throw new NotImplementedException();
        }
    }
}
