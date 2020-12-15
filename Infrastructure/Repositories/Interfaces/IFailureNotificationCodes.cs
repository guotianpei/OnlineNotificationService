using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Infrastructure.Responsitories;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    interface IFailureNotiificationCodes : IGenericRepository<FailureNotifyCodes>, IRepository<FailureNotifyCodes>
    {
        Task<FailureNotifyCodes> AddAsync(FailureNotifyCodes request);
        Task<FailureNotifyCodes> UpdateAsync(FailureNotifyCodes request);
        Task<FailureNotifyCodes> GetAsync(int id);
        Task<FailureNotifyCodes> GetByCode(string code);
    }
}
