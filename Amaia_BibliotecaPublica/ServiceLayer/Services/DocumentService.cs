namespace ServiceLayer.Services
{
    using CrossCutting.Constants;
    using Factories;
    using IDataServiceLayer.Interfaces;
    using IDataServiceLayer.Models;
    using IServiceLayer.Interfaces;
    using System.Threading.Tasks;
    using System;

    public class DocumentService : IDocumentService
    {
        #region Constructor

        public DocumentService(IDocumentDataService documentDataService)
        {
            this._documentDataService = documentDataService;
        }

        #endregion Constructor

        #region Properties

        private IDocumentDataService _documentDataService;

        #endregion Properties

        #region Services

        public async Task<DocumentList> GetBookById(int id)
        {
            return await this._documentDataService.GetDocumentListById(id, Constants.DocumentType.Book);
        }

        public async Task<DocumentList> GetBookByTitle(string title)
        {
            return await this._documentDataService.GetDocumentListByTitle(title, Constants.DocumentType.Book);
        }

        public async Task<Document> AddBook(Document book, string languageCode)
        {
            var propertyFactory = DocumentPropertiesFactory.SetDocumentProperties(Constants.DocumentType.Book);
            var property = await propertyFactory.SetProperties(this._documentDataService, languageCode);
            return await this._documentDataService.AddDocument(book, property.Id);
        }

        public async Task<bool> IsThereAnyDocument()
        {
            return await this._documentDataService.IsThereAnyDocument();
        }

        #endregion Services
    }
}
