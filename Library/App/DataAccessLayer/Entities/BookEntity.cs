namespace Library.App.DataAccessLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BookEntity
    {
        public int Id { get; set; }

        public string BookTitle { get; set; }

        public string AuthorName { get; set; }

    }
}
