namespace ServiceLayer.Factories.LoanProperties
{
    using CrossCutting.Constants;
    using IDataServiceLayer.Interfaces;
    using IDataServiceLayer.Models;
    using System;
    using System.Threading.Tasks;

    public class BookLoanProperties : ILoanProperties
    {
        public async Task<Loan> SetProperties(ILoanDataService loanDataService, int documentId, int userId, DateTime lentDate)
        {
            var loan = new Loan()
            {
                CreateDate = DateTime.Now,
                CreateUser = Constants.DefaultUser,
                LoanDate = lentDate,
                DueData = lentDate.AddDays(Constants.BooksPropertyForLoan.LoanDays),
                DocumentId = documentId,
                UserId = userId
            };

            return await loanDataService.LoanDocument(loan);
        }
    }
}
