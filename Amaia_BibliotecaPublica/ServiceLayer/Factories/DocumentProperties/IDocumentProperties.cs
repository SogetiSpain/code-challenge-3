namespace ServiceLayer.Factories.DocumentProperties
{
    using IDataServiceLayer.Interfaces;
    using IDataServiceLayer.Models;
    using System.Threading.Tasks;

    public interface IDocumentProperties
    {
        Task<DocumentProperty> SetProperties(IDocumentDataService documentDataService, string languageCode);
    }
}
