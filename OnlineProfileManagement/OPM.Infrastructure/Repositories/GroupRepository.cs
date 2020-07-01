using System;
using System.Threading.Tasks;
using OPM.Domain.Aggregates.DistributionGroupsAggregate;
using OPM.Domain.Aggregates.ProfileAggregate;
using OPM.Domain.SeekWork;
using OPM.Infrastructure.Repositories.Interfaces;

namespace OPM.Infrastructure.Repositories
{
    //test commit
    public class GroupRepository : GenericRepository<DistributionGroup>,IGroupRepository
    {
        public GroupRepository(ProfileContext context) : base(context)
        { 
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();
    }
}
