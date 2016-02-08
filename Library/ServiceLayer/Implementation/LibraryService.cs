namespace Library.App.ServiceLayer.Implementation
{
    using Library.App.DataAccessLayer.Entities;
    using Library.App.DataAccessLayer.Fake;
    using Library.App.ServiceLayer.DTO;
    using Library.App.ServiceLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LibraryService : ILibraryService
    {
        private static LibraryDbFake _dblibrary = new LibraryDbFake();

        public LibraryService() 
        { 
        
        }

        public PenaltyDto ReturnBook(string Username, string BookName, DateTime returnDate) 
        {
            BookingDto NewBookingDto = this.GetBooking(Username, BookName);
            BookingEntity bookingEntity = new BookingEntity();

            bookingEntity.BookId = NewBookingDto.BookId;
            bookingEntity.EndBookingDate = NewBookingDto.EndBookingDate;
            bookingEntity.LibraryAppUsername = NewBookingDto.LibraryAppUsername;
            bookingEntity.StartBookingDate = NewBookingDto.StartBookingDate;
            _dblibrary.ReturnBook(bookingEntity);

            if (NewBookingDto.EndBookingDate < returnDate)
            {
                PenaltyEntity penalty = new PenaltyEntity();
                penalty.BookingId = bookingEntity.Id;
                penalty.BookId = bookingEntity.BookId;
                penalty.LibraryAppUsername = bookingEntity.LibraryAppUsername;
                _dblibrary.CreatePenalty(penalty);

                PenaltyDto penaltyDto = new PenaltyDto();
                penaltyDto.Id = penalty.Id;
                penaltyDto.BookId = penalty.BookId;
                penaltyDto.BookingId = penalty.BookingId;
                penaltyDto.LibraryAppUsername = penalty.LibraryAppUsername;
                return(penaltyDto);
            }
            else 
            {
                _dblibrary.ReturnBook(bookingEntity);             
                return null;
            }
        }

        public bool RegisterBook(BookDto NewBook) 
        {
            BookEntity bookEntity = new BookEntity();
            bookEntity.BookTitle = NewBook.BookTitle;
            bookEntity.AuthorName = NewBook.AuthorName;
            if (_dblibrary.RegisterBook(bookEntity))
            {
                //Console.ForegroundColor = ConsoleColor.DarkYellow;
                //Console.WriteLine("LibraryService - Libro creado correctamente\n");
                //Console.ForegroundColor = ConsoleColor.White;
                return true;
            }
            else 
            {
                //Console.ForegroundColor = ConsoleColor.DarkYellow;
                //Console.WriteLine("LibraryService - El libro no se ha creado en el sistema\n");
                //Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
        }

        public void RegisterBooking(BookingDto NewBookingDto)
        {
            //Console.ForegroundColor = ConsoleColor.DarkYellow;
            //Console.WriteLine("LibraryService - Reservando libro para su préstamo \n");
            //Console.ForegroundColor = ConsoleColor.White;
            
            BookingEntity bookingEntity = new BookingEntity();

            bookingEntity.BookId = NewBookingDto.BookId;
            bookingEntity.EndBookingDate = NewBookingDto.EndBookingDate;
            bookingEntity.LibraryAppUsername = NewBookingDto.LibraryAppUsername;
            bookingEntity.StartBookingDate = NewBookingDto.StartBookingDate;
            _dblibrary.RegisterBooking(bookingEntity);
        }

        public bool IsUserAllowed(string Username)
        {
            //Console.ForegroundColor = ConsoleColor.DarkYellow;
            //Console.WriteLine("LibraryService - Comprobando sí el usuario puede reservar libro... \n");
            //Console.ForegroundColor = ConsoleColor.White;

            bool hasPenalty = _dblibrary.HasPenalty(Username);
            bool limitBook = _dblibrary.GetTotalBooking(Username)>3;

            return (!hasPenalty && !limitBook);
        }

        public BookingDto GetBooking(string Username, string bookTitle) 
        {
            //Console.ForegroundColor = ConsoleColor.DarkYellow;
            //Console.WriteLine("LibraryService - Obteniendo reserva del usuario {0} del libro {1}... \n", Username, bookTitle);
            //Console.ForegroundColor = ConsoleColor.White;
            BookingEntity bookingEntity = _dblibrary.GetBooking(Username, bookTitle);
            BookingDto bookingDto = new BookingDto();

            bookingDto.Id = bookingEntity.Id;
            bookingDto.BookId = bookingEntity.BookId;
            bookingDto.EndBookingDate = bookingEntity.EndBookingDate;
            bookingDto.StartBookingDate = bookingEntity.StartBookingDate;
            bookingDto.LibraryAppUsername = bookingEntity.LibraryAppUsername;
            bookingDto.UserReturnDate = bookingEntity.UserReturnDate;            

            return bookingDto;
        }

        public BookDto GetBook(string bookTitle) 
        {
            BookEntity bookEntity = _dblibrary.GetBook(bookTitle);
            BookDto bookDto = new BookDto();
            if (bookEntity!= null)
            {
                bookDto.AuthorName = bookEntity.AuthorName;
                bookDto.BookTitle = bookEntity.BookTitle;
                bookDto.Id = bookEntity.Id;
                return (bookDto);
            }
            else 
            {
                return null;
            }
        }

        public bool IsBooked(string bookTitle) 
        {
            return(_dblibrary.IsBooked(bookTitle));
        }

        public PenaltyDto GetPenalty(string username, string bookTitle) 
        {
            PenaltyEntity penaltyEntity = _dblibrary.GetPenalty(username, bookTitle);
            PenaltyDto PenaltyDto = new PenaltyDto();
            PenaltyDto.BookId = penaltyEntity.BookId;
            PenaltyDto.BookingId = penaltyEntity.BookingId;
            PenaltyDto.Id = penaltyEntity.Id;
            PenaltyDto.LibraryAppUsername = penaltyEntity.LibraryAppUsername;
            return (PenaltyDto);
        }

        public bool PayPenalty(PenaltyDto Penalty) 
        {
            _dblibrary.PayPenalty(Penalty.Id);
            return (true);
        }
    }
}
