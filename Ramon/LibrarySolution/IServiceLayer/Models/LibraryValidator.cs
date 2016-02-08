using DataServiceLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServiceLayer.Models
{
    public static class LibraryValidator
    {

        private const int maxNumberBooks = 3;

        public static bool isBookNull(Book book, ref LibraryBooksOperationResult result)
        {
            if (book == null)
            {
                result.Message = ValidationMessages.BookNotExist;
                return true;
            }

            return false;
        }

        public static bool isBookLoaned(Book book, ref LibraryBooksOperationResult result)
        {
            if (book != null && book.IsLoaned.HasValue && book.IsLoaned.Value)
            {
                result.Message = ValidationMessages.BookAlreadyLoaned;
                return true;
            }

            return false;
        }

        public static bool isMaxLoanReached(IEnumerable<Book> userBookList, ref LibraryBooksOperationResult result)
        {
            if (userBookList.Count() > maxNumberBooks)
            {
                result.Message = ValidationMessages.MaxBookLoanReached;
                return true;
            }

            return false;
        }

        public static bool isUserFined(User user, ref LibraryBooksOperationResult result)
        {
            if (user.isFined.Value)
            {
                result.Message = ValidationMessages.UserFined;
                return true;
            }

            return false;
        }

        public static bool isBookAlreadyRegistered(Book book, ref LibraryBooksOperationResult result)
        {
            if (book != null)
            {
                result.Message = ValidationMessages.BookAlreadyRegistered;
                return true;
            }

            return false;
        }

        public static bool isBookLoanExceeds30Days(Book book, ref LibraryBooksOperationResult result, InfoRequest infoReq)
        {
            if (book.LoanDate.Value.AddDays(30) < infoReq.ReturnDate)
            {
                result.Message = ValidationMessages.BookLoanExceeds30Days;
                return true;
            }

            return false;
        }

        public enum ValidationMessages
        {
            BookNotAvailable,
            BookAlreadyRegistered,
            BookAlreadyLoaned,
            BookNotExist,
            BookLoanExceeds30Days,
            MaxBookLoanReached,
            UserFined
        }
    }
}
