
namespace Biblioteca
{
    using System.Collections.Generic;

    public interface IBibliotecaService
    {
        bool RegistreBook(string name);

        bool LoanBook(int id);

        bool ReturnBook(int id);

        Libros Find(string titulo);

        List<Libros> GetAllBooks();
    }
}