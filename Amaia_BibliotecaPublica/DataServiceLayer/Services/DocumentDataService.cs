namespace DataServiceLayer.Services
{
    using IDataServiceLayer.Interfaces;
    using IDataServiceLayer.Models;
    using System.Threading.Tasks;
    using System.Linq;
    using System;
    using CrossCutting.Constants;

    public class DocumentDataService : IDocumentDataService
    {
        private readonly PublicLibraryContext ctx = new PublicLibraryContext();

        public async Task<DocumentList> GetDocumentListById(int id, string documentType)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return this.ctx.DocumentList.Where(x => x.DocumentId == id && x.TypeCode == documentType).FirstOrDefault();
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DocumentList> GetDocumentListByTitle(string title, string documentType)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return this.ctx.DocumentList.Where(x => x.Title == title && x.TypeCode == documentType).FirstOrDefault();
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Document> AddDocument(Document document, int documentPropertyId)
        {
            try
            {
                document.DocumentPropertyId = documentPropertyId;
                document.CreateDate = DateTime.Now;
                document.CreateUser = Constants.DefaultUser;
                var entity = this.ctx.Documents.Add(document);
                await this.ctx.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DocumentProperty> AddDocumentProperties(DocumentProperty property)
        {
            try
            {
                var entity = this.ctx.DocumentProperties.Add(property);
                await this.ctx.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> GetLanguageIdByCode(string code)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var entity = this.ctx.Languages.Where(x => x.Code == code).FirstOrDefault();
                    return entity.Id;
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> GetDocumentTypeIdByCode(string code)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var entity = this.ctx.DocumentTypes.Where(x => x.Code == code).FirstOrDefault();
                    return entity.Id;
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsThereAnyDocument()
        {
            try
            {
                return await Task.Run(() =>
                {
                    return this.ctx.Documents.Count() > 0;
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
