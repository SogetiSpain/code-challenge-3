
namespace Biblioteca
{
    using System.Collections.Generic;

    public interface IBibliotecaService
    {
        bool RegistreBook(string name);

        bool LoanBook(int libroId, int usuarioId);

        bool ReturnBook(int libroId);

        Libros FindBook(string titulo);

        Libros FindBook(int libroId);

        List<Libros> GetAllBooks();

        Usuarios FindUsuario(string nombre);

        bool CrearUsuario(string nombreUsuario);

        bool ExistUser(string nombreUsuario);

        bool IsBookBorrowed(int libroId);

        bool CanBorrowBooks(int usuarioId);

        List<Prestamos> GetOwnBorrowedBooks(int usuarioId);

        bool AddFine(int usuarioId);


    }
}