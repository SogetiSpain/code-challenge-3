namespace App
{
    using CrossCutting.Resources;
    using CrossCutting.Utils;
    using IDataServiceLayer.Models;
    using IServiceLayer.Interfaces;
    using System.Threading.Tasks;

    public class ManageUser
    {
        private IAccountService accountService;
        private User user;
        private ConsoleDatas cd;

        public ManageUser(IAccountService accountService)
        {
            this.accountService = accountService;
            this.cd = new ConsoleDatas();
        }

        public async Task<User> GetUser()
        {
            var username = GetUsername();
            user = await this.accountService.GetUserByUsername(username);
            if (user == null)
            {
                user = await this.accountService.RegisterUser(new User() { Username = username });
            }

            return user;
        }

        private string GetUsername()
        {
            return this.cd.GetData(Display.IntroduceUser, Exceptions.EmptyUsernameException);
        }
    }
}
