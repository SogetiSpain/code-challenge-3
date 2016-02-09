namespace ServiceLayer.Services
{
    using IDataServiceLayer.Interfaces;
    using IDataServiceLayer.Models;
    using IServiceLayer.Interfaces;
    using System.Threading.Tasks;

    public class AccountService : IAccountService
    {
        #region Constructor

        public AccountService(IAccountDataService accountDataService)
        {
            this._accountDataService = accountDataService;
        }

        #endregion Constructor

        #region Properties

        private IAccountDataService _accountDataService;

        #endregion Properties

        #region Services

        public async Task<User> GetUserByUsername(string username)
        {
            return await this._accountDataService.GetUserByUsername(username);
        }

        public async Task<User> RegisterUser(User user)
        {
            return await this._accountDataService.RegisterUser(user);
        }

        #endregion Services
    }
}
