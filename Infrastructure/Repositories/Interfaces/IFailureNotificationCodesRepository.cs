using System;
using System.Collections.Generic;
using System.Text;
using ONP.Domain.Models;
using ONP.Infrastructure.Responsitories;
using System.Threading.Tasks;
using ONP.Domain.Seedwork;

namespace ONP.Infrastructure.Repositories.Interfaces
{
    public interface IFailureNotiificationCodesRepository : IGenericRepository<FailureNotifyCodes>, IRepository<FailureNotifyCodes>
    {
        Task<FailureNotifyCodes> AddAsync(FailureNotifyCodes request);
        Task<FailureNotifyCodes> UpdateAsync(FailureNotifyCodes request);
        Task<FailureNotifyCodes> GetAsync(int id);
        Task<FailureNotifyCodes> GetByCode(string code);
    }
}
