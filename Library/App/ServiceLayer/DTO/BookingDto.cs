namespace Library.App.ServiceLayer.DTO
{
    using System;

    [Serializable]
    public class BookingDto
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string LibraryAppUsername { get; set; }

        public DateTime StartBookingDate { get; set; }

        public DateTime EndBookingDate { get; set; }

        public DateTime UserReturnDate { get; set; }

    }
}
