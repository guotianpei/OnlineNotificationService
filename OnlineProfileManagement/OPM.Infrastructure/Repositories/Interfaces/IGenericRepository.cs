using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OPM.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<T>:IDisposable where T:class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
