using DataServiceLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataServiceLayer.Repositories
{
    public interface IBooksRepository : IGenericRepository<Book>
    {
        Task<Book> GetBookByCode(string Code);
        Task<IEnumerable<Book>> GetBookListByUsername(string username);
    }
}
