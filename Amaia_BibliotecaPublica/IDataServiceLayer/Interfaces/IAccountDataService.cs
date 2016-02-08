namespace IDataServiceLayer.Interfaces
{
    using IDataServiceLayer.Models;
    using System.Threading.Tasks;

    public interface IAccountDataService
    {
        Task<User> GetUserByUsername(string username);

        Task<User> RegisterUser(User user);
    }
}