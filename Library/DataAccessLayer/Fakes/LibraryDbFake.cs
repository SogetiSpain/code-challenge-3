namespace Library.App.DataAccessLayer.Fake
{
    using Library.App.DataAccessLayer.Code;
using Library.App.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class LibraryDbFake
    {
        private List<BookEntity> _bookTable = new List<BookEntity>(){
            new BookEntity() {Id = 1, BookTitle = "El médico", AuthorName = "Noah Gordon"},
            new BookEntity() {Id = 2, BookTitle = "Cabo de trafalgar", AuthorName = "Arturo Perez Reverte"},
            new BookEntity() {Id = 3, BookTitle = "Ángeles y demonios", AuthorName = "Dan Brown"},
            new BookEntity() {Id = 4, BookTitle = "1984", AuthorName = "George Orwell"},
            new BookEntity() {Id = 5, BookTitle = "Polvo", AuthorName = "Patricia Cornwell"}
        };

        private List<BookingEntity> _bookingTable = new List<BookingEntity>(){
            new BookingEntity() {Id = 1, BookId=4, LibraryAppUsername="jgonzalez", 
                                StartBookingDate = DateTime.ParseExact("01/01/2016", "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo),
                                EndBookingDate= DateTime.ParseExact("01/02/2016", "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo) }
        };

        private List<PenaltyEntity> _penaltyTable = new List<PenaltyEntity>(){
            new PenaltyEntity() {Id=1, BookId=1, LibraryAppUsername="jgonzalez"}
        };

        public bool RegisterBooking(BookingEntity newBookingEntity) 
        {
            int idCounter = 1;
            foreach (BookingEntity booking in _bookingTable)
            {
                idCounter++;
            }
            newBookingEntity.Id = idCounter;
            _bookingTable.Add(newBookingEntity);
            return true;
        }

        public bool RegisterBook(BookEntity newBookEntity) 
        {
            int idCounter = 1;
            foreach (BookEntity book in _bookTable)
            {
                if ((book.BookTitle == newBookEntity.BookTitle)&&(book.AuthorName == newBookEntity.AuthorName))
                {
                    return false;
                }
                idCounter++;
            }
            newBookEntity.Id = idCounter;
            _bookTable.Add(newBookEntity);
            return true;
        }

        public BookEntity GetBook(string bookTitle) 
        {
            BookEntity book = new BookEntity();
            foreach (BookEntity bookItem in _bookTable)
            {
                if (bookItem.BookTitle == bookTitle) 
                {
                    return bookItem;
                }            
            }

            return null;        
        }

        public bool IsBooked(string bookTitle) 
        {
            try
            {
                BookEntity book = _bookTable.Where(x => x.BookTitle == bookTitle).First();
                BookingEntity booking = _bookingTable.Where(x => x.BookId == book.Id).First();
                if (booking.UserReturnDate < booking.StartBookingDate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        
        }

        public bool HasPenalty(string username) 
        {
            foreach (PenaltyEntity penalty in _penaltyTable) 
            {
                if (penalty.LibraryAppUsername == username) 
                {
                    return true;
                }                
            }
            return false;
        
        }

        public BookingEntity GetBooking(string username, string bookTitle) 
        {
            try
            {
                BookEntity book = _bookTable.Where(x => x.BookTitle == bookTitle).First();
                return (_bookingTable.Where(x => (x.LibraryAppUsername == username) && (x.BookId == book.Id)).First());
            }
            catch 
            {
                return null;
            }
        }

        public bool CreatePenalty(PenaltyEntity penalty) 
        {
            int idCounter = 1;
            foreach (PenaltyEntity penaltyItem in _penaltyTable)
            {
                idCounter++;
            }
            penalty.Id = idCounter;
            _penaltyTable.Add(penalty);
            return true;
        }

        public void ReturnBook(BookingEntity booking) 
        {
            booking.UserReturnDate = DateTime.Today;
        }

        public int GetTotalBooking(string username) 
        {
            return (_bookingTable.Where(x => x.LibraryAppUsername == username).Count());
        }
        
        public PenaltyEntity GetPenalty(string username, string bookTitle)
        {
            int bookId = _bookTable.Where(x => x.BookTitle == bookTitle).First().Id;
            return (_penaltyTable.Where(x => (x.LibraryAppUsername == username)&&(x.BookId==bookId)).First());
        }

        public bool PayPenalty(int id) 
        {
            PenaltyEntity penalty = _penaltyTable.Where(x => x.Id == id).First();
            _penaltyTable.Remove(penalty);
            return true;
        }
    }
}








