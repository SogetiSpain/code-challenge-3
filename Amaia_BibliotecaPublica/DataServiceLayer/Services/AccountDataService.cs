namespace DataServiceLayer.Services
{
    using IDataServiceLayer.Interfaces;
    using IDataServiceLayer.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class AccountDataService : IAccountDataService
    {
        private readonly PublicLibraryContext ctx = new PublicLibraryContext();

        public async Task<User> GetUserByUsername(string username)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return this.ctx.Users.Where(x => x.Username == username).FirstOrDefault();
                });
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> RegisterUser(User user)
        {
            try
            {
                var entity = this.ctx.Users.Add(user);
                await this.ctx.SaveChangesAsync();
                return entity;
            }
            catch (System.Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
