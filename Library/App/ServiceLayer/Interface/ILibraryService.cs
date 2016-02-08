namespace Library.App.ServiceLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Library.App.ServiceLayer.DTO;

    public interface ILibraryService
    {
        /// <summary>
        /// Register the return book with the current date and if the returnDate > finishBookingDAte
        /// The method will return the right Penalty for the delay.
        /// </summary>
        /// <param name="NewBook"></param>
        PenaltyDto ReturnBook(string Username, string BookName, DateTime returnDate);

        /// <summary>
        /// Creates a NewBook into the system
        /// </summary>
        /// <param name="NewBook"></param>
        bool RegisterBook(BookDto NewBook);

        /// <summary>
        /// Creates the NewBooking into the system
        /// </summary>
        /// <param name="NewBooking"></param>
        void RegisterBooking(BookingDto NewBooking);

        /// <summary>
        /// Check if the user are or not allowed to make a Booking
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        bool IsUserAllowed(string Username);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookTitle"></param>
        /// <returns></returns>
        BookDto GetBook(string bookTitle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookTitle"></param>
        /// <returns></returns>
        bool IsBooked(string bookTitle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="bookTitle"></param>
        /// <returns></returns>
        BookingDto GetBooking(string Username, string bookTitle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        PenaltyDto GetPenalty(string username, string bookTitle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Penalty"></param>
        /// <returns></returns>
        bool PayPenalty(PenaltyDto Penalty);
    }
}
