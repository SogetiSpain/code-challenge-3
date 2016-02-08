namespace Library.App.ServiceLayer.DTO
{
    using System;

    [Serializable]
    public class BookDto
    {
        public int Id { get; set; }

        public string BookTitle { get; set; }

        public string AuthorName { get; set; }

    }
}
