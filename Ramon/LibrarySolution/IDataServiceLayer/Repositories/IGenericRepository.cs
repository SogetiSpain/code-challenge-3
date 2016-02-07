using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDataServiceLayer.Repositories
{
    public interface IGenericRepository<T> 
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        Task<T> Delete(T entity);
        void Edit(T entity);
        Task Save();
    }
}
