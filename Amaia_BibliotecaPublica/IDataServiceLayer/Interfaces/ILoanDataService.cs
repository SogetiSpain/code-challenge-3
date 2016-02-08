namespace IDataServiceLayer.Interfaces
{
    using IDataServiceLayer.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILoanDataService
    {
        Task<Loan> LoanDocument(Loan loan);

        Task ChangeDocumentAvailability(int documentId, bool availability);

        Task<bool> HasThreeDocumentLent(int userId);

        Task<ICollection<Loan>> NotReturnDocument(int userId);

        Task ReturnDocument(int documentId, DateTime returnData);

        Task<bool> HasDocumentLent(int userId, int documentId);

        Task<Loan> GetLoandByDocumentTitle(string title, int userId, string documentType);
    }
}
