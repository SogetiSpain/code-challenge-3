using System.Collections.Generic;
using DataLayer;

namespace DataAccessLayer
{
    public interface IBibliotecaService
    {
        /// <summary>
        ///     Registres the book.
        /// </summary>
        /// <param name="name">The name.</param>
        bool RegistreBook(string name);

        /// <summary>
        ///     Loans the book.
        /// </summary>
        /// <param name="libroId">The libro identifier.</param>
        /// <param name="usuarioId">The usuario identifier.</param>
        bool LoanBook(int libroId, int usuarioId);

        /// <summary>
        ///     Returns the book.
        /// </summary>
        /// <param name="libroId">The libro identifier.</param>
        bool ReturnBook(int libroId);

        /// <summary>
        ///     Finds the book.
        /// </summary>
        /// <param name="titulo">The titulo.</param>
        Libros FindBook(string titulo);

        /// <summary>
        ///     Finds the book.
        /// </summary>
        /// <param name="libroId">The libro identifier.</param>
        Libros FindBook(int libroId);

        /// <summary>
        ///     Gets all books.
        /// </summary>
        List<Libros> GetAllBooks();

        /// <summary>
        ///     Finds the usuario.
        /// </summary>
        /// <param name="nombre">The nombre.</param>
        Usuarios FindUsuario(string nombre);

        /// <summary>
        ///     Crears the usuario.
        /// </summary>
        /// <param name="nombreUsuario">The nombre usuario.</param>
        bool CrearUsuario(string nombreUsuario);

        /// <summary>
        ///     Exists the user.
        /// </summary>
        /// <param name="nombreUsuario">The nombre usuario.</param>
        bool ExistUser(string nombreUsuario);

        /// <summary>
        ///     Determines whether [is book borrowed] [the specified libro identifier].
        /// </summary>
        /// <param name="libroId">The libro identifier.</param>
        bool IsBookBorrowed(int libroId);

        /// <summary>
        ///     Determines whether this instance [can borrow books] the specified usuario identifier.
        /// </summary>
        /// <param name="usuarioId">The usuario identifier.</param>
        bool CanBorrowBooks(int usuarioId);

        /// <summary>
        ///     Gets the own borrowed books.
        /// </summary>
        /// <param name="usuarioId">The usuario identifier.</param>
        List<Prestamos> GetOwnBorrowedBooks(int usuarioId);

        /// <summary>
        ///     Adds the fine.
        /// </summary>
        /// <param name="usuarioId">The usuario identifier.</param>
        bool AddFine(int usuarioId);

        /// <summary>
        ///     Determines whether the specified usuario identifier has fines.
        /// </summary>
        /// <param name="usuarioId">The usuario identifier.</param>
        bool HasFines(int usuarioId);

        /// <summary>
        ///     Pays the fine.
        /// </summary>
        /// <param name="usuarioId">The usuario identifier.</param>
        void PayFine(int usuarioId);
    }
}