using DataServiceLayer.EF;
using IDataServiceLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLayer.Repositories
{
    public class BooksRepository : GenericRepository<Book>, IBooksRepository
    {
        public BooksRepository(DbContext ctx)
            : base(ctx)
        {

        }

        public async Task<Book> GetBookByCode(string Code)
        {
            return (await FindBy(x => x.Code == Code)).FirstOrDefault();
        }

        public async Task<IEnumerable<Book>> GetBookListByUsername(string username)
        {
            return await Task.Run(() => _dbset.Include(x => x.User).Include(x => x.User).Where(y => y.User.Username == username));
        }

        public override async Task<IEnumerable<Book>> GetAll()
        {
            return await Task.Run(() => _entities.Set<Book>().Include(x => x.User).AsEnumerable());
        }
    }
}
