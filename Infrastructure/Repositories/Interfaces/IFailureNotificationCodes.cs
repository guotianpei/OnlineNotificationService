using System;
using System.Collections.Generic;
using System.Text;
using ONP.Domain;
using Infrastructure.Responsitories;
using System.Threading.Tasks;

namespace ONP.Infrastructure.Repositories
{
    interface IFailureNotiificationCodes : IGenericRepository<FailureNotifyCodes>, IRepository<FailureNotifyCodes>
    {
        Task<FailureNotifyCodes> AddAsync(FailureNotifyCodes request);
        Task<FailureNotifyCodes> UpdateAsync(FailureNotifyCodes request);
        Task<FailureNotifyCodes> GetAsync(int id);
        Task<FailureNotifyCodes> GetByCode(string code);
    }
}
