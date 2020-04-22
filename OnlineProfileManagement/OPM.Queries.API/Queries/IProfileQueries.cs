using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPM.Queries.API.Queries
{
    public interface IProfileQueries
    {
        Task<Profile> GetProfileByID(int id);


        Task<IEnumerable<Profile>> GetMultipleProfiles(List<int> Ids, ProfileComChannel channel);



    }
}
