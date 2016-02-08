using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using DataLayer;

namespace Biblioteca
{
    internal class Program
    {
        private static readonly BibliotecaService BibliotecaService = new BibliotecaService();

        private static void Main(string[] args)
        {
            var salir = false;

            do
            {
                Console.WriteLine("¿Quiere (R)egistrar un libro, hacer un (P)réstamo, una (D)evolución o (S)alir?:");

                var option = Console.ReadLine();

                if (option == null)
                {
                    continue;
                }
                switch (option.ToUpper())
                {
                    case "R":
                        RegistrarLibro();
                        break;
                    case "P":
                        PrestarLibro();
                        break;

                    case "D":
                        DevolverLibro();
                        break;

                    case "S":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción incorrecta");
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                }
            } while (salir == false);
        }

        private static void DevolverLibro()
        {
            try
            {
                Console.WriteLine("Introduzca el usuario:");
                var usuarioName = Console.ReadLine();

                if (!ExisteUsuario(usuarioName))
                {
                    Console.WriteLine("El usuario no existe");
                    return;
                }
                var usuario = BibliotecaService.FindUsuario(usuarioName);
                var borrowedBooks = GetOwnBorrowedBooks(usuario.Id).ToList();

                if (borrowedBooks.Count < 1)
                {
                    Console.WriteLine("Usted No tiene libros a devolver");
                    return;
                }

                Console.WriteLine("Estos son los libros que tiene en su poder:");
                PrintPrestamos(borrowedBooks);

                Console.WriteLine("Introduzca el título del libro a devolver:");
                var titulo = Console.ReadLine();

                if (IhaveThisBook(titulo, borrowedBooks))
                {
                    ReturnBook(titulo, borrowedBooks, usuario);
                    return;
                }
                Console.WriteLine("Usted no tiene el libro " + titulo);
            }
            catch (Exception)
            {
                Console.WriteLine("Ha habido algún problema al devolver el libro, intentelo mas tarde");
            }
        }

        private static void PrestarLibro()
        {
            try
            {
                Console.WriteLine("Introduzca el usuario:");
                var nombreUsuario = Console.ReadLine();
                if (!ExisteUsuario(nombreUsuario))
                {
                    CrearUsuario(nombreUsuario);
                }
                var usuario = BibliotecaService.FindUsuario(nombreUsuario);

                if (CheckFines(usuario))
                {
                    return;
                }

                if (!CanBorrowBooks(usuario.Id))
                {
                    Console.WriteLine("Usted tiene 3 o más libros en su poder, no puede realizar más préstamos");
                    return;
                }

                if (!ShowAllBooks())
                {
                    return;
                }

                Console.WriteLine("Introduzca el título del libro:");
                var titulo = Console.ReadLine();

                if (!ExistBook(titulo))
                {
                    Console.WriteLine("El libro no existe");
                    return;
                }

                var libro = BibliotecaService.FindBook(titulo);

                if (BibliotecaService.IsBookBorrowed(libro.Id))
                {
                    Console.WriteLine("El libro " + titulo + " no está disponible ahora mismo.");
                    return;
                }

                BibliotecaService.LoanBook(libro.Id, usuario.Id);

                Console.WriteLine("Fecha del prestamo: " + DateTime.Now.Date.ToShortDateString());
                Console.WriteLine("Préstamo realizado. Fecha de devolución: " +
                                  DateTime.Now.Date.AddMonths(1).ToShortDateString());
            }
            catch (Exception)
            {
                Console.WriteLine("Ha habido un problema al prestar el libro.");
            }
        }

        private static void RegistrarLibro()
        {
            try
            {
                Console.WriteLine("Introduzca el título del libro:");
                var titulo = Console.ReadLine();

                if (ExistBook(titulo))
                {
                    Console.WriteLine("El libro ya esta registrado");
                    return;
                }
                BibliotecaService.RegistreBook(titulo);
            }
            catch (Exception)
            {
                Console.WriteLine("Ha habido un problema al registrar el libro");
            }
        }

        private static bool IhaveThisBook(string titulo, IEnumerable<Prestamos> borrowedBooks)
        {
            return
                borrowedBooks.Any(
                    prestamo => string.CompareOrdinal(titulo, BibliotecaService.FindBook(prestamo.LibroId).Titulo) != 0);
        }

        private static void PrintPrestamos(IEnumerable<Prestamos> borrowedBooks)
        {
            foreach (var prestamo in borrowedBooks)
            {
                Console.WriteLine(BibliotecaService.FindBook(prestamo.LibroId).Titulo);
            }
        }

        private static IEnumerable<Prestamos> GetOwnBorrowedBooks(int usuarioId)
        {
            return BibliotecaService.GetOwnBorrowedBooks(usuarioId);
        }

        private static void PagarMulta(int usuarioId)
        {
            BibliotecaService.PayFine(usuarioId);
        }

        private static bool HasFines(int usuarioId)
        {
            return BibliotecaService.HasFines(usuarioId);
        }

        private static bool CanBorrowBooks(int usuarioId)
        {
            return BibliotecaService.CanBorrowBooks(usuarioId);
        }

        private static void CrearUsuario(string nombreUsuario)
        {
            BibliotecaService.CrearUsuario(nombreUsuario);
        }

        private static bool ExisteUsuario(string nombreUsuario)
        {
            return BibliotecaService.ExistUser(nombreUsuario);
        }

        private static bool ShowAllBooks()
        {
            var books = BibliotecaService.GetAllBooks();
            if (books.Count == 0)
            {
                Console.WriteLine("No tenemos libros disponibles, por favor registre uno.");
                return false;
            }
            Console.WriteLine("Estos son los libros que tenemos:");
            for (var i = 0; i < books.Count; i++)
            {
                Console.WriteLine(i + " - " + books[i].Titulo);
            }
            return true;
        }

        private static void ReturnBook(string titulo, List<Prestamos> borrowedBooks, Usuarios usuario)
        {
            var libro = BibliotecaService.FindBook(titulo);
            BibliotecaService.ReturnBook(libro.Id);

            Console.WriteLine("La fecha de devolución es: " + DateTime.Now.Date.ToShortDateString());
            var fechaLimite = borrowedBooks.First(x => x.LibroId == libro.Id).FechaPrestamo.AddMonths(1);
            if (fechaLimite < DateTime.Now.Date)
            {
                Console.WriteLine(
                    "La devolución se ha realizado tarde, debe pagar una multa antes de volver a alquilar mas libros");
                BibliotecaService.AddFine(usuario.Id);
            }
            Console.WriteLine("El libro " + titulo + " está disponible para ser prestado");
        }

        public static bool ExistBook(string titulo)
        {
            return BibliotecaService.ExistBook(titulo);
        }

        private static bool CheckFines(Usuarios usuario)
        {
            if (!HasFines(usuario.Id)) return false;
            do
            {
                Console.WriteLine("Usted tiene multas pendientes. No puede alquilar mas libros");
                Console.WriteLine("Quiere pagar las multas? (S)i // (N)o");
                var option = Console.ReadLine();

                if (option == null) continue;
                switch (option.ToUpper())
                {
                    case "S":
                        PagarMulta(usuario.Id);
                        Console.WriteLine("Las multas han sido pagadas. Ya puedo alquilar más libros");
                        return false;
                    case "N":
                        return true;
                    default:
                        return true;
                }
            } while (true);
        }
    }
}