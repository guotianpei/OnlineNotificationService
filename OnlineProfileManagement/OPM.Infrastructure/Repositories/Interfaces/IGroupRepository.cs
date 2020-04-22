using System;
using System.Threading.Tasks;
using OPM.Domain;
using OPM.Domain.Aggregates;
using OPM.Domain.Aggregates.DistributionGroupsAggregate;
using OPM.Domain.SeekWork;
namespace OPM.Infrastructure.Repositories.Interfaces
{
    public interface IGroupRepository : IRepository<DistributionGroup>
    {

        DistributionGroup Add(DistributionGroup group);

        void Update(DistributionGroup group);

        Task<DistributionGroup> GetAsync(int groupId);

        Task<DistributionGroup> FindByIdAsync(int id);
    }
}
