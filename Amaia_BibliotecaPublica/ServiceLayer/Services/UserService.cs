namespace ServiceLayer.Services
{
    using IDataServiceLayer.Interfaces;
    using IDataServiceLayer.Models;
    using IServiceLayer.Interfaces;
    using System.Threading.Tasks;
    using System;

    public class UserService : IUserService
    {
        #region Constructor

        public UserService(IUserDataService userDataService)
        {
            this._userDataService = userDataService;
        }

        #endregion Constructor

        #region Properties

        private IUserDataService _userDataService;

        #endregion Properties

        #region Services

        public async Task<User> GetUserById(int id)
        {
            return await this._userDataService.GetUserById(id);
        }

        public async Task<bool> HasAnyFine(int userId)
        {
            return await this._userDataService.HasAnyFine(userId);
        }

        public async Task PayFine(int userId)
        {
            await this._userDataService.PayFine(userId);
        }

        public async Task<Fine> SetFine(int userId)
        {
            return await this._userDataService.SetFine(userId);
        }

        #endregion Services
    }
}
