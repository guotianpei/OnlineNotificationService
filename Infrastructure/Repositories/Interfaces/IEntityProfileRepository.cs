using System;
using System.Collections.Generic;
using System.Text;
using ONP.Domain.Models;
using ONP.Infrastructure.Responsitories;
using System.Threading.Tasks;
using ONP.Domain.Seedwork;

namespace ONP.Infrastructure.Repositories.Interfaces
{
    public interface IEntityProfileRepository : IGenericRepository<EntityProfile>, IRepository<EntityProfile>
    {
        Task<EntityProfile> AddAsync(EntityProfile request);
        Task<EntityProfile> UpdateAsync(EntityProfile request);
        Task<EntityProfile> GetAsync(string EntityID);

    }
}
