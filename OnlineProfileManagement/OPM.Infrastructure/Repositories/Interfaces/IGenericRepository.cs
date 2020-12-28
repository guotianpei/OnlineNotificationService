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
        T Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
