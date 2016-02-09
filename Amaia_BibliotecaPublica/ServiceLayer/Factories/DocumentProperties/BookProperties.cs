namespace ServiceLayer.Factories.DocumentProperties
{
    using CrossCutting.Constants;
    using IDataServiceLayer.Interfaces;
    using IDataServiceLayer.Models;
    using System;
    using System.Threading.Tasks;

    public class BookProperties : IDocumentProperties
    {
        public async Task<DocumentProperty> SetProperties(IDocumentDataService documentDataService, string languageCode)
        {
            var documentTypeId = await documentDataService.GetDocumentTypeIdByCode(Constants.DocumentType.Book);
            var languageId = await documentDataService.GetLanguageIdByCode(languageCode);
            var property = new DocumentProperty()
            {
                CanBeLent = true,
                MaxDaysRent = Constants.BooksPropertyForLoan.LoanDays,
                TypeId = documentTypeId,
                FinePrice = Constants.BooksPropertyForLoan.FineValue,
                LanguageId = languageId,
                CreateUser = Constants.DefaultUser,
                CreateDate = DateTime.Now
            };

            return await documentDataService.AddDocumentProperties(property);
        }
    }
}
