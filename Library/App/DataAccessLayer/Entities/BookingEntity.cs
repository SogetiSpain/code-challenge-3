namespace Library.App.DataAccessLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BookingEntity
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string LibraryAppUsername { get; set; }

        public DateTime StartBookingDate { get; set; }

        public DateTime EndBookingDate { get; set; }

        public DateTime UserReturnDate { get; set; }
    }
}
