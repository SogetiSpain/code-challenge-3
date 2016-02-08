namespace App
{
    using CrossCutting.Enum;
    using CrossCutting.Resources;
    using IDataServiceLayer.Models;
    using IServiceLayer.Interfaces;
    using System;
    using System.Threading.Tasks;

    public class FineManager
    {
        private IUserService userService;

        private User user;

        private bool isPaid = true;

        public FineManager(IUserService userService, User user)
        {
            this.userService = userService;
            this.user = user;
            this.isPaid = true;
        }

        public async Task<bool> SearchFines()
        {
            await HasAnyFindToPaid();
            return this.isPaid;
        }

        private async Task HasAnyFindToPaid()
        {
            var hasFineToPaid = await this.userService.HasAnyFine(user.Id);
            if (hasFineToPaid)
            {
                Console.WriteLine(Exceptions.PaidFineException);
                await AskPaidFine();
                return;
            }
        }

        public async Task AskPaidFine()
        {
            string value = Console.ReadLine();
            try
            {
                YesNoEnum.YesNo option = (YesNoEnum.YesNo)Enum.Parse(typeof(YesNoEnum.YesNo), value.ToUpper());
                await ResultAskPaidFine(option);
            }
            catch (Exception)
            {
                Console.WriteLine(Exceptions.LetterAskYesNoException);
                await AskPaidFine();
            }
        }

        private async Task ResultAskPaidFine(YesNoEnum.YesNo option)
        {
            switch (option)
            {
                case YesNoEnum.YesNo.S:
                    await PayFine();
                    break;
                default:
                    this.isPaid = false;
                    break;
            }
        }

        private async Task PayFine()
        {
            await this.userService.PayFine(user.Id);
        }
    }
}
