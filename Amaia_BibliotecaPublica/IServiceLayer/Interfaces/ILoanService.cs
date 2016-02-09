namespace IServiceLayer.Interfaces
{
    using IDataServiceLayer.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILoanService
    {
        Task<Loan> LoanBook(int documentId, int userId, DateTime lentDate);

        Task ChangeBookAvailability(int documentId, bool availability);

        Task<bool> HasThreeBookLent(int userId);

        Task<ICollection<Loan>> NotReturnDocument(int userId);

        Task ReturnBook(int documentId, DateTime returnData);

        Task<bool> HasBookLent(int userId, int documentId);

        Task<Loan> GetLoanBookListByTitle(string title, int userId);
    }
}
