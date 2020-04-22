using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using OPM.Domain.Aggregates.ProfileAggregate;
using OPM.Domain.SeekWork;
using OPM.Infrastructure.Repositories.Interfaces;

namespace OPM.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {

        //https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design
        //Define one repository per aggregate
        private readonly ProfileContext _context;

        public ProfileRepository(ProfileContext context)
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

        public EntityProfile Add(EntityProfile profile)
        {
            return _context.EntityProfile.Add(profile).Entity;
        }

        public Task<EntityProfile> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EntityProfile> GetAsync(string profileId)
        {
            throw new NotImplementedException();
        }

        public void Update(EntityProfile profile)
        {
            _context.Entry(profile).State = EntityState.Modified;
        }
    }
}
