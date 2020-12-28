using Microsoft.EntityFrameworkCore;
using OPM.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OPM.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        
        DbContext _context;
        //Rachel: SaveChangesAsync happens in DBContext.SaveEntitiesAsync() to commit all DB CRUD commands.
        public GenericRepository(DbContext context)
        {
            _context = context;
        }
        public async virtual Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async virtual Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public virtual T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            //await _context.SaveChangesAsync();
            return entity;
        }
        public virtual void Update(T entity)
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
            
            //await _context.SaveChangesAsync();
        }
        public virtual void Delete(int id)
        {
            T entity = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entity);
            //await _context.SaveChangesAsync();
        }
        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }
}
