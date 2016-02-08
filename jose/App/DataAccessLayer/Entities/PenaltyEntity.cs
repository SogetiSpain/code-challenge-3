namespace Library.App.DataAccessLayer.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PenaltyEntity
    {
        public int Id { get; set; }

        public string LibraryAppUsername { get; set; }

        public int BookId { get; set; }

        public int BookingId { get; set; }
    }
}
