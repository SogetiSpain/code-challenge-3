namespace App
{
    using CrossCutting.Constants;
    using CrossCutting.Resources;
    using CrossCutting.Utils;
    using IDataServiceLayer.Models;
    using IServiceLayer.Interfaces;
    using System;
    using System.Threading.Tasks;

    public class RegisterBook
    {
        private IDocumentService documentService;
        private Document document;
        private ConsoleDatas cd;

        public RegisterBook(IDocumentService documentService)
        {
            this.documentService = documentService;
            this.cd = new ConsoleDatas();
        }

        public async Task RegisterNewBook()
        {
            document = new Document();
            document.Title = GetBookTitle();
            var language = GetBookLanguage();
            var book = await this.documentService.AddBook(document, language);
        }

        private string GetBookTitle()
        {
            return this.cd.GetData(Display.IntroduceBookName, Exceptions.EmptyTitleException);
        }

        private string GetBookLanguage()
        {
            Console.WriteLine(Display.IntroduceBookLanguage);
            string value = Console.ReadLine().ToLower();
            if (string.IsNullOrEmpty(value) || (!value.Equals(Constants.Language.EN) && !value.Equals(Constants.Language.ES)))
            {
                Console.WriteLine(Exceptions.LanguageException);
                return GetBookLanguage();
            }

            return value;
        }
    }
}
