namespace IServiceLayer.Interfaces
{
    using IDataServiceLayer.Models;
    using System.Threading.Tasks;

    public interface IDocumentService
    {
        Task<DocumentList> GetBookById(int id);

        Task<DocumentList> GetBookByTitle(string title);

        Task<Document> AddBook(Document book, string code);

        Task<bool> IsThereAnyDocument();
    }
}
