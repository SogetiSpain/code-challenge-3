namespace IDataServiceLayer.Interfaces
{
    using IDataServiceLayer.Models;
    using System.Threading.Tasks;

    public interface IDocumentDataService
    {
        Task<DocumentList> GetDocumentListById(int id, string documentType);

        Task<DocumentList> GetDocumentListByTitle(string title, string documentType);

        Task<Document> AddDocument(Document document, int documentPropertyId);

        Task<DocumentProperty> AddDocumentProperties(DocumentProperty property);

        Task<int> GetLanguageIdByCode(string code);

        Task<int> GetDocumentTypeIdByCode(string code);

        Task<bool> IsThereAnyDocument();
    }
}
