using System;
using System.Threading.Tasks;
using OPM.Domain;
using OPM.Domain.Aggregates;
using OPM.Domain.Aggregates.ProfileAggregate;
using OPM.Domain.SeekWork;

namespace OPM.Infrastructure.Repositories.Interfaces
{
    public interface IProfileRepository : IRepository<EntityProfile>
    {

        EntityProfile Add(EntityProfile profile);

        void Update(EntityProfile profile);
        
        Task<EntityProfile> FindByIdAsync(int id);

        Task<EntityProfile> GetAsync(string profileId);
    }
}
