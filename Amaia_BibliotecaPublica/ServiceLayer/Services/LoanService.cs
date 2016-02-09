namespace ServiceLayer.Services
{
    using CrossCutting.Constants;
    using Factories;
    using IDataServiceLayer.Interfaces;
    using IDataServiceLayer.Models;
    using IServiceLayer.Interfaces;
    using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;

    public class LoanService : ILoanService
    {
        #region Constructor

        public LoanService(ILoanDataService loanDataService)
        {
            this._loanDataService = loanDataService;
        }

        #endregion Constructor

        #region Properties

        private ILoanDataService _loanDataService;

        #endregion Properties

        #region Services

        public async Task ChangeBookAvailability(int documentId, bool availability)
        {
            await this._loanDataService.ChangeDocumentAvailability(documentId, availability);
        }

        public async Task<Loan> LoanBook(int documentId, int userId, DateTime lentDate)
        {
            var loanFactory = DocumentLoanFactory.SetLoanProperties(Constants.DocumentType.Book);
            return await loanFactory.SetProperties(this._loanDataService, documentId, userId, lentDate);
        }

        public Task<bool> HasThreeBookLent(int userId)
        {
            return this._loanDataService.HasThreeDocumentLent(userId);
        }

        public async Task<ICollection<Loan>> NotReturnDocument(int userId)
        {
            return await this._loanDataService.NotReturnDocument(userId);
        }

        public async Task ReturnBook(int documentId, DateTime returnData)
        {
            await this._loanDataService.ReturnDocument(documentId, returnData);
            await this.ChangeBookAvailability(documentId, true);
        }

        public async Task<bool> HasBookLent(int userId, int documentId)
        {
            return await this._loanDataService.HasDocumentLent(userId, documentId);
        }

        public async Task<Loan> GetLoanBookListByTitle(string title, int userId)
        {
            return await this._loanDataService.GetLoandByDocumentTitle(title, userId, Constants.DocumentType.Book);
        }

        #endregion Services
    }
}
