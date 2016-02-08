namespace IServiceLayer.Interfaces
{
    using IDataServiceLayer.Models;
    using System.Threading.Tasks;

    public interface IAccountService
    {
        Task<User> GetUserByUsername(string username);

        Task<User> RegisterUser(User user);
    }
}
