using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using OPM.Domain.Aggregates.ProfileAggregate;
using OPM.Domain.SeekWork;
using OPM.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace OPM.Infrastructure.Repositories
{
    public class ProfileRepository : GenericRepository<EntityProfile>, IProfileRepository
    {

        //https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design
        //Define one repository per aggregate
        private readonly ProfileContext _context;

        public ProfileRepository(ProfileContext context):base(context)
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
        public Task<EntityProfile> GetAsync(string EntityID)
        {
            return Task.FromResult(_context.EntityProfiles.Where(obj => obj.EntityID == EntityID)
                                                           .Include(obj => obj.ProfileResource)
                                                           .Include(obj => obj.ProfileComChannels)
                                                           .FirstOrDefault<EntityProfile>());
        }

    }
}
