namespace Biblioteca
{
    public interface IBibliotecaService
    {
        bool RegistreBook(string name);

        bool LoanBook(int id);

        bool ReturnBook(int id);

        Libros Find(string titulo);
    }
}