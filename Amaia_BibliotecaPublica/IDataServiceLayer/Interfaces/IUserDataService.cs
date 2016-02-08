namespace IDataServiceLayer.Interfaces
{
    using IDataServiceLayer.Models;
    using System.Threading.Tasks;

    public interface IUserDataService
    {
        Task<User> GetUserById(int id);

        Task<bool> HasAnyFine(int userId);

        Task PayFine(int userId);

        Task<Fine> SetFine(int userId);
    }
}
