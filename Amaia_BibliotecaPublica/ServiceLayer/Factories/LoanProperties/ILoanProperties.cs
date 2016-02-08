namespace ServiceLayer.Factories.LoanProperties
{
    using IDataServiceLayer.Interfaces;
    using IDataServiceLayer.Models;
    using System;
    using System.Threading.Tasks;

    public interface ILoanProperties
    {
        Task<Loan> SetProperties(ILoanDataService loanDataService, int documentId, int userId, DateTime lentDate);
    }
}
