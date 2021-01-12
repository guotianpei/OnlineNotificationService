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
    class EntityProfileRepository : GenericRepository<EntityProfile>, IEntityProfileRepository
    {
        //https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design
        //Define one repository per aggregate
        private readonly NotificationProcessorContext _context;

        public EntityProfileRepository(NotificationProcessorContext context) : base(context)
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

        public Task<EntityProfile> AddAsync(EntityProfile request)
        {
            throw new NotImplementedException();
        }

        public Task<EntityProfile> GetAsync(string EntityID)
        {
            throw new NotImplementedException();
        }

        public Task<EntityProfile> UpdateAsync(EntityProfile request)
        {
            throw new NotImplementedException();
        }
    }
}
