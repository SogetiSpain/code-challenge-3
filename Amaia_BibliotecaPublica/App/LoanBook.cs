namespace App
{
    using CrossCutting.Enum;
    using CrossCutting.Resources;
    using CrossCutting.Utils;
    using IDataServiceLayer.Models;
    using IServiceLayer.Interfaces;
    using System;
    using System.Threading.Tasks;

    public class LoanBook
    {
        #region properties

        private ILoanService loanService;

        private IDocumentService documentService;

        private IUserService userService;

        private User user;

        private FineManager fineManager;

        private ConsoleDatas cd;

        #endregion properties

        public LoanBook(ILoanService loanService, IDocumentService documentService, IUserService userService, User user)
        {
            this.loanService = loanService;
            this.documentService = documentService;
            this.userService = userService;
            this.user = user;
            this.fineManager = new FineManager(this.userService, user);
            this.cd = new ConsoleDatas();
        }

        public async Task StartLoanBook()
        {
            var isAnyBook = await IsAnyDocument();
            if (!isAnyBook) { return; }
            var hasPaid = await this.fineManager.SearchFines();
            var canLent = await HasThreeBookLent(hasPaid);
            await GetBookTitle(canLent);
        }

        private async Task<bool> IsAnyDocument()
        {
            var isSth = await this.documentService.IsThereAnyDocument();
            if (!isSth)
            {
                Console.WriteLine(Exceptions.NotBookRegistered);
                return false;
            }
            return true;
        }

        private async Task<bool> HasThreeBookLent(bool isPaid)
        {
            if (!isPaid) { return false; }
            var hasThreeBookLent = await this.loanService.HasThreeBookLent(user.Id);
            if (hasThreeBookLent)
            {
                Console.WriteLine(Exceptions.MaxBookLentException);
                return false;
            }
            return true;
        }

        private async Task GetBookTitle(bool canLent)
        {
            if (!canLent) { return; }
            var title =  this.cd.GetData(Display.IntroduceBookName, Exceptions.EmptyTitleException);
            await SearchBook(title);
        }

        private async Task SearchBook(string title)
        {
            var documentList = await this.documentService.GetBookByTitle(title);
            if (documentList == null)
            {
                Console.WriteLine(Exceptions.BookNotFoundException);
               await GetBookTitle(true);
               return;
            }
            await CanBeLent(documentList, title);
        }

        private async Task CanBeLent(DocumentList documentList, string title)
        {
            if (!documentList.CanBeLent)
            {
                Console.WriteLine(Exceptions.BookNotAvailable, title);
                Console.WriteLine(Display.AskForAnotherBook);
                await AskForAnotherBook();
                return;
            }

            await GetLentDate(documentList.DocumentId);
        }

        private async Task AskForAnotherBook()
        {
            string value = Console.ReadLine();
            try
            {
                YesNoEnum.YesNo option = (YesNoEnum.YesNo)Enum.Parse(typeof(YesNoEnum.YesNo), value.ToUpper());
                await ResultAskForAnotherBook(option);
            }
            catch (Exception)
            {
                Console.WriteLine(Exceptions.LetterAskYesNoException);
                await AskForAnotherBook();
            }
        }

        private async Task ResultAskForAnotherBook(YesNoEnum.YesNo option)
        {
            switch (option)
            {
                case YesNoEnum.YesNo.S:
                    await GetBookTitle(true);
                    break;
                default:
                    break;
            }
        }

        private async Task GetLentDate(int documentId)
        {
            Console.WriteLine(Display.IntroduceDataLoan);
            var value = Console.ReadLine();
            DateTime lentDate;
            if(DateTime.TryParse(value, out lentDate))
            {
                await LoanUserBook(documentId, lentDate);
            }
            else
            {
                Console.WriteLine(Exceptions.DateException);
                await GetLentDate(documentId);
            }
        }

        private async Task LoanUserBook(int documentId, DateTime lentDate)
        {
            var book = await this.loanService.LoanBook(documentId, user.Id, lentDate);
            await this.loanService.ChangeBookAvailability(documentId, false);
            Console.WriteLine(Display.LoanSuccessful, book.DueData.ToShortDateString());
        }
    }
}
