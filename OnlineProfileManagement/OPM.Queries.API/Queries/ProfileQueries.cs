using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPM.Queries.API.Queries
{
    public class ProfileQueries : IProfileQueries
    {
        //Define query objects.

        public ProfileQueries()
        {
        }

        Task<IEnumerable<Profile>> IProfileQueries.GetMultipleProfiles(List<int> Ids, ProfileComChannel channel)
        {
            throw new NotImplementedException();
        }

        Task<Profile> IProfileQueries.GetProfileByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
