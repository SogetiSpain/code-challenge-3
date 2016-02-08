using DataServiceLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataServiceLayer.Repositories
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User> GetUserByUsername(string username);
    }
}
