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
    public class RegisterBook : ILibraryOperations
    {
        private IBooksRepository _bookRepository;
        private IUsersRepository _usersRepository;

        public RegisterBook(IBooksRepository bookRepository, IUsersRepository usersRepository)
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
                await Register(infoRequest);
                await this._bookRepository.Save();
            }

            return result;
        }

        private LibraryBooksOperationResult validate(InfoRequest infoReq)
        {
            var result = new LibraryBooksOperationResult();
            var book = this._bookRepository.FindBy(x => x.Code == infoReq.BookCode).Result.FirstOrDefault();

            result.Success = !LibraryValidator.isBookAlreadyRegistered(book, ref result);

            return result;
        }

        private async Task Register(InfoRequest infoRequest)
        {
            var user = await this._usersRepository.GetUserByUsername(infoRequest.Username);

            await this._bookRepository.Add(new Book()
            {
                Code = infoRequest.BookCode,
                LoanDate = infoRequest.LoanDate,
                IsLoaned = true,
                ReturnDate = infoRequest.ReturnDate,
                User = user == null ? new User() { Username = infoRequest.Username } : user
        });
        }
    }
}
