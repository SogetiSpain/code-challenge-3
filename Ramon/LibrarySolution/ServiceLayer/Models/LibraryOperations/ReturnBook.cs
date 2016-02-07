using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDataServiceLayer.Repositories;
using IServiceLayer.Models;
using static IServiceLayer.Models.LibraryValidator;
using DataServiceLayer.EF;

namespace ServiceLayer.Models.LibraryOperations
{
    public class ReturnBook : ILibraryOperations
    {
        private IBooksRepository _bookRepository;
        private IUsersRepository _usersRepository;

        public ReturnBook(IBooksRepository bookRepository, IUsersRepository usersRepository)
        {
            this._usersRepository = usersRepository;
            this._bookRepository = bookRepository;
        }

        public async Task<LibraryBooksOperationResult> ExecuteOperation(InfoRequest infoRequest)
        {
            LibraryBooksOperationResult result;
            var bookList = await this._bookRepository.GetBookListByUsername(infoRequest.Username);
            var book = bookList.Where(x => x.Code == infoRequest.BookCode).FirstOrDefault();
            var user = await this._usersRepository.GetUserByUsername(infoRequest.Username);
            
            result = validate(infoRequest, book);

            if (result.Success || result.Message.Equals(ValidationMessages.BookLoanExceeds30Days))
            {
                if (result.Message.Equals(ValidationMessages.BookLoanExceeds30Days))
                {
                    user.isFined = true;
                    await this._usersRepository.Save();
                }
                Return(infoRequest, book);
                await this._bookRepository.Save();
            }

            return result;
        }

        private LibraryBooksOperationResult validate(InfoRequest infoReq, Book book)
        {
            var result = new LibraryBooksOperationResult();
            
            result.Success = !LibraryValidator.isBookNull(book, ref result)
                && LibraryValidator.isBookLoaned(book, ref result)
                && LibraryValidator.isBookLoanExceeds30Days(book, ref result, infoReq);

            return result;
        }

        private void Return(InfoRequest infoRequest, Book book)
        {            
            book.IsLoaned = false;
            book.ReturnDate = infoRequest.ReturnDate;
            this._bookRepository.Edit(book);
        }
    }
}
