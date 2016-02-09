namespace ServiceLayer.Factories
{
    using CrossCutting.Constants;
    using DocumentProperties;

    public static class DocumentPropertiesFactory
    {
        public static IDocumentProperties SetDocumentProperties(string documentType)
        {
            IDocumentProperties result = null;
            switch (documentType)
            {
                case Constants.DocumentType.Book:
                    result = new BookProperties();
                    break;
            }

            return result;
        }
    }
}
