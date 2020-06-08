using System;
using System.Threading.Tasks;
using OPM.Domain.Aggregates.DistributionGroupsAggregate;
using OPM.Domain.Aggregates.ProfileAggregate;
using OPM.Domain.SeekWork;
using OPM.Infrastructure.Repositories.Interfaces;

namespace OPM.Infrastructure.Repositories
{
    //test commit
    public class GroupRepository :IGroupRepository
    {
        public GroupRepository()
        {
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public DistributionGroup Add(DistributionGroup group)
        {
            throw new NotImplementedException();
        }

        public Task<DistributionGroup> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DistributionGroup> GetAsync(int groupId)
        {
            throw new NotImplementedException();
        }

        public void Update(DistributionGroup group)
        {
            throw new NotImplementedException();
        }
    }
}
