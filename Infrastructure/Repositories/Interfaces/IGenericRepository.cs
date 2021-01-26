using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace
    ONP.Infrastructure.Repositories.Interfaces

{
    public interface IGenericRepository<T>:IDisposable where T:class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
