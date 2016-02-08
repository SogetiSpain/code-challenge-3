using IDataServiceLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServiceLayer.EF;
using System.Data.Entity;

namespace DataServiceLayer.Repositories
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(DbContext ctx)
            : base(ctx)
        {

        }

        public async Task<User> GetUserByUsername(string username)
        {
            return (await FindBy(x => x.Username == username)).FirstOrDefault();
        }
    }
}
