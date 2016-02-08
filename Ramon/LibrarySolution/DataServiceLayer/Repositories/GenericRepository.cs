using IDataServiceLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
   {
       protected DbContext _entities;
       protected readonly IDbSet<T> _dbset;
 
       public GenericRepository(DbContext context)
       {
           _entities = context;
           _dbset = context.Set<T>();
       }
 
       public virtual async Task<IEnumerable<T>> GetAll()
       {
           return await Task.Run(() => _dbset.AsEnumerable<T>());
       }
 
       public async Task<IEnumerable<T>> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
       {
            return await Task.Run(() => _dbset.Where(predicate).AsEnumerable()); 
       }
 
       public virtual async Task<T> Add(T entity)
       {
            return await Task.Run(() => _dbset.Add(entity));
       }
 
       public virtual async Task<T> Delete(T entity)
       {
            return await Task.Run(() => _dbset.Remove(entity));
       }
 
       public virtual void Edit(T entity)
       {
           _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
       }
 
       public virtual async Task Save()
       {
            await _entities.SaveChangesAsync();
       }
   }
}
