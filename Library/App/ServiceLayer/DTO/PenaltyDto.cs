namespace Library.App.ServiceLayer.DTO
{
    using System;

    [Serializable]
    public class PenaltyDto
    {
        public int Id { get; set; }

        public string LibraryAppUsername { get; set; }

        public int BookId { get; set; }

        public int BookingId { get; set; }

    }
}

