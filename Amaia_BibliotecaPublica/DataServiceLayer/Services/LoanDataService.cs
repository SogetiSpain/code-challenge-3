namespace DataServiceLayer.Services
{
    using IDataServiceLayer.Interfaces;
    using IDataServiceLayer.Models;
    using System.Threading.Tasks;
    using System.Linq;
    using System;
    using CrossCutting.Constants;
    using System.Collections.Generic;

    public class LoanDataService : ILoanDataService
    {
        private readonly PublicLibraryContext ctx = new PublicLibraryContext();

        public async Task<Loan> LoanDocument(Loan loan)
        {
            try
            {
                var entity = this.ctx.Loans.Add(loan);
                await this.ctx.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ChangeDocumentAvailability(int documentId, bool availability)
        {
            try
            {
                var document = this.ctx.Documents.Include("DocumentProperties").Where(x => x.Id == documentId).FirstOrDefault();
                document.DocumentProperties.CanBeLent = availability;
                await this.ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> HasThreeDocumentLent(int userId)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var documentsCount = this.ctx.Loans.Where(x => x.UserId == userId && x.ReturnedData == null).Count();
                    return documentsCount >= 3;
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<Loan>> NotReturnDocument(int userId)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return this.ctx.Loans.Include("Documents").Where(x => x.UserId == userId && x.ReturnedData == null).ToList();
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ReturnDocument(int documentId, DateTime returnData)
        {
            try
            {
                var loan = this.ctx.Loans.Where(x => x.DocumentId == documentId && x.ReturnedData == null).FirstOrDefault();
                loan.ReturnedData = returnData;
                loan.UpdateUser = Constants.DefaultUser;
                loan.UpdateDate = DateTime.Now;

                await this.ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> HasDocumentLent(int userId, int documentId)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return this.ctx.Loans.Any(x => x.UserId == userId && x.DocumentId == documentId && x.ReturnedData == null);
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Loan> GetLoandByDocumentTitle(string title, int userId, string documentType)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return this.ctx.Loans.Include("Documents").Where(x => x.UserId == userId && x.Documents.Title == title && x.ReturnedData == null).FirstOrDefault();
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
