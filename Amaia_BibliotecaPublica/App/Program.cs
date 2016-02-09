namespace App
{
    using CrossCutting.Enum;
    using CrossCutting.Resources;
    using IDataServiceLayer.Models;
    using IServiceLayer.Interfaces;
    using System;
    using System.Threading.Tasks;

    class Program
    {
        private static Global global = new Global();
        private static IAccountService accountService;
        private static IUserService userService;
        private static IDocumentService documentService;
        private static ILoanService loanService;

        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                global.Application_Start(out accountService, out documentService, out userService, out loanService);

                var exit = false;
                do
                {
                    var m = new ChoiceInputMenu();
                    var option = m.choiceMenu();
                    exit = await MenuAction(option, exit);

                } while (!exit);

            }).Wait();
        }

        public static async Task<bool> MenuAction(ChoiceStartEnum.ChoiceEnum option, bool exit)
        {
            switch (option)
            {
                case ChoiceStartEnum.ChoiceEnum.R:
                    await RegisterBook();
                    break;
                case ChoiceStartEnum.ChoiceEnum.P:
                    var userP = await ManageUser();
                    await LoanBook(userP);
                    break;
                case ChoiceStartEnum.ChoiceEnum.D:
                    var userD = await ManageUser();
                    await ReturnBook(userD);
                    break;
                case ChoiceStartEnum.ChoiceEnum.S:
                    exit = true;
                    break;
                default:
                    Console.Write(Exceptions.LetterAskException);
                    break;
            }

            return exit;
        }

        public static async Task RegisterBook()
        {
            var register = new RegisterBook(documentService);
            await register.RegisterNewBook();
        }

        public static async Task<User> ManageUser()
        {
            var register = new ManageUser(accountService);
            return await register.GetUser();
        }

        public static async Task LoanBook(User user)
        {
            var loan = new LoanBook(loanService, documentService, userService, user);
            await loan.StartLoanBook();
        }

        public static async Task ReturnBook(User user)
        {
            var returnBook = new ReturnBook(loanService, documentService, userService, user);
            await returnBook.StartReturningBook();
        }
    }
}
