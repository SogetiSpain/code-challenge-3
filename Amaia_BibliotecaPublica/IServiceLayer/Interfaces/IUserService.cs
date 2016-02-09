namespace IServiceLayer.Interfaces
{
    using IDataServiceLayer.Models;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<User> GetUserById(int id);

        Task<bool> HasAnyFine(int userId);

        Task PayFine(int userId);

        Task<Fine> SetFine(int userId);
    }
}
