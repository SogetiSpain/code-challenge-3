namespace CrossCutting.Constants
{
    public static class Constants
    {
        public static class BooksPropertyForLoan
        {
            public const int MaxBookAllowed = 3;
            public const int LoanDays = 30;
            public const int FineValue = 10;
        }

        public static class DocumentType
        {
            public const string Magazine = "MGZN";
            public const string Book = "BOOK";
        }

        public static class Language
        {
            public const string ES = "es";
            public const string EN = "en";
        }

        public const string DefaultUser = "System";
    }
}
