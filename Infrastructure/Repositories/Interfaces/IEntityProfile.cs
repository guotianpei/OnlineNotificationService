using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Infrastructure.Responsitories;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    interface IEntityProfile : IGenericRepository<EntityProfile>, IRepository<EntityProfile>
    {
        Task<EntityProfile> AddAsync(EntityProfile request);
        Task<EntityProfile> UpdateAsync(EntityProfile request);
        Task<EntityProfile> GetAsync(int profileID);

    }
}
