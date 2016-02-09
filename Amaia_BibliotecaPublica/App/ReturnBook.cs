namespace App
{
    using IDataServiceLayer.Models;
    using IServiceLayer.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using CrossCutting.Resources;
    using CrossCutting.Enum;
    public class ReturnBook
    {
        #region Properties

        private ILoanService loanService;

        private IDocumentService documentService;

        private IUserService userService;

        private User user;

        private FineManager fineManager;

        #endregion

        public ReturnBook(ILoanService loanService, IDocumentService documentService, IUserService userService, User user)
        {
            this.loanService = loanService;
            this.documentService = documentService;
            this.userService = userService;
            this.user = user;
            this.fineManager = new FineManager(this.userService, this.user);
        }

        public async Task StartReturningBook()
        {
            await GetNotReturningBook();
        }

        private async Task GetNotReturningBook()
        {
            var books = await this.loanService.NotReturnDocument(user.Id);
            await HasBookToReturn(books);
        }

        private async Task HasBookToReturn(ICollection<Loan> books)
        {
            if (books.Count() == 0)
            {
                Console.WriteLine(Display.NotBooksToReturn);
                return;
            }

            await ShowNotReturningBook(books);
        }

        private async Task ShowNotReturningBook(ICollection<Loan> books)
        {
            Console.WriteLine(Display.BooksLent, string.Join(", ", books.Select(x => x.Documents.Title)));
            await GetBookTitle();
        }

        private async Task GetBookTitle()
        {
            Console.WriteLine(Display.IntroduceBookName);
            var value = Console.ReadLine();
            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine(Exceptions.EmptyTitleException);
                await GetBookTitle();
            }
            await SearchBook(value);
        }

        private async Task SearchBook(string title)
        {
            var loan = await this.loanService.GetLoanBookListByTitle(title, user.Id);
            if (loan == null)
            {
                Console.WriteLine(Exceptions.BookNotFoundException);
                await GetBookTitle();
            }
            await HasBookLent(loan);
        }

        private async Task HasBookLent(Loan loan)
        {
            var hasLent = await this.loanService.HasBookLent(user.Id, loan.DocumentId);
            if (!hasLent)
            {
                Console.WriteLine(Exceptions.NotLentBookException);
                await GetBookTitle();
            }

            await GetReturnedDate(loan);
        }

        private async Task GetReturnedDate(Loan loan)
        {
            Console.WriteLine(Display.IntroduzceReturnDate);
            var value = Console.ReadLine();
            DateTime returnDate;
            if(DateTime.TryParse(value, out returnDate))
            {
                await CheckReturnedWithLoan(loan, returnDate);
            }
            else
            {
                Console.WriteLine(Exceptions.DateException);
                await GetReturnedDate(loan);
            }
        }

        private async Task CheckReturnedWithLoan(Loan loan, DateTime returnDate)
        {
            if (loan.LoanDate > returnDate)
            {
                Console.WriteLine(Exceptions.ReturnLoanDateException);
                await GetReturnedDate(loan);
            }

            await ReturnBookToLibrary(loan, returnDate);
        }

        private async Task ReturnBookToLibrary(Loan loan, DateTime returnDate)
        {
            await this.loanService.ReturnBook(loan.DocumentId, returnDate);
            Console.WriteLine(Display.BookIsAvailable, loan.Documents.Title);
            await HasFineForReturningLate(loan, returnDate);
        }

        private async Task HasFineForReturningLate(Loan loan, DateTime returnDate)
        {
            if (loan.DueData < returnDate)
            {
                await this.userService.SetFine(user.Id);
                Console.WriteLine(Display.NotReturnOnTime);
                await this.fineManager.AskPaidFine();
            }
        }
    }
}