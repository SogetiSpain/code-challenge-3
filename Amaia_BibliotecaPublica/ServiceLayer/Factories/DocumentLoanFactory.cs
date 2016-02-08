namespace ServiceLayer.Factories
{
    using CrossCutting.Constants;
    using LoanProperties;

    public static class DocumentLoanFactory
    {
        public static ILoanProperties SetLoanProperties(string documentType)
        {
            ILoanProperties result = null;
            switch (documentType)
            {
                case Constants.DocumentType.Book:
                    result = new BookLoanProperties();
                    break;
            }

            return result;
        }
    }
}
