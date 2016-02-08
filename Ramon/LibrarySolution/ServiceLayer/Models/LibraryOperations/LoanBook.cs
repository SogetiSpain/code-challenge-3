using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDataServiceLayer.Repositories;
using IServiceLayer.Models;
using DataServiceLayer.EF;

namespace ServiceLayer.Models.LibraryOperations
{
    public class LoanBook : ILibraryOperations
    {

        private IBooksRepository _bookRepository;
        private IUsersRepository _usersRepository;

        public LoanBook(IBooksRepository bookRepository, IUsersRepository usersRepository)
        {
            this._usersRepository = usersRepository;
            this._bookRepository = bookRepository;
        }
        public async Task<LibraryBooksOperationResult> ExecuteOperation(InfoRequest infoRequest)
        {

            LibraryBooksOperationResult result;
            result = validate(infoRequest);
            if (result.Success)
            {
                await Loan(infoRequest);
                await this._bookRepository.Save();
            }
            
            return result;
        }

        private LibraryBooksOperationResult validate(InfoRequest infoReq)
        {
            var result = new LibraryBooksOperationResult();

            var user = this._usersRepository.GetUserByUsername(infoReq.Username).Result;
            var book = this._bookRepository.FindBy(x => x.Code == infoReq.BookCode).Result.FirstOrDefault();
            var userBookList = this._bookRepository.GetBookListByUsername(infoReq.Username).Result;

            result.Success = !LibraryValidator.isBookNull(book, ref result)
                && !LibraryValidator.isBookLoaned(book, ref result)
                && !LibraryValidator.isMaxLoanReached(userBookList,ref result)
                && !LibraryValidator.isUserFined(user, ref result);

            return result;
        }

        private async Task Loan(InfoRequest infoReq)
        {
            var bookList = await this._bookRepository.GetBookListByUsername(infoReq.Username);
            var book = bookList.Where(x => x.Code == infoReq.BookCode).FirstOrDefault();
            var user = await this._usersRepository.GetUserByUsername(infoReq.Username);
            book.IsLoaned = true;
            book.User = user == null ? new User() { Username = infoReq.Username } : user;
            this._bookRepository.Edit(book);
        }       
    }
}
