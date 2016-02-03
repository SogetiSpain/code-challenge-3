using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

                if (option == null) continue;
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
            Console.WriteLine("Introduzca el usuario:");
            var usuarioName = Console.ReadLine();
            var usuario = BibliotecaService.FindUsuario(usuarioName);
            var borrowedBooks = GetOwnBorrowedBooks(usuario.Id).ToList();

            Console.WriteLine("Estos son los libros que tiene en su poder:");
            PrintPrestamos(borrowedBooks);

            Console.WriteLine("Introduzca el título del libro a devolver:");
            var titulo = Console.ReadLine();

            if (IhaveThisBook(titulo, borrowedBooks))
            {
                var libro = BibliotecaService.FindBook(titulo);
                BibliotecaService.ReturnBook(libro.Id);

                Console.WriteLine("La fecha de devolución es: " + DateTime.Now.Date.ToShortDateString());
                var fechaLimite = borrowedBooks.First(x => x.LibroId == libro.Id).FechaPrestamo.AddMonths(1);
                if (fechaLimite < DateTime.Now.Date)
                {
                    Console.WriteLine("La devolución se ha realizado tarde, debe pagar una multa antes de volver a alquilar mas libros");
                    BibliotecaService.AddFine(usuario.Id);
                }
                Console.WriteLine("El libro " + titulo + " está disponible para ser prestado");
                return;
            }
            Console.WriteLine("Usted no tiene el libro " + titulo);
        }

        private static bool IhaveThisBook(string titulo, IEnumerable<Prestamos> borrowedBooks)
        {
            return borrowedBooks.Any(prestamo => String.CompareOrdinal(titulo, BibliotecaService.FindBook(prestamo.LibroId).Titulo) != 0);

            //return borrowedBooks.Any(x => x.Libros.Titulo == titulo);
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
            return  BibliotecaService.GetOwnBorrowedBooks(usuarioId);
        }

        private static void PrestarLibro()
        {
            if (!ShowAllBooks()){ return; }

            Console.WriteLine("Introduzca el usuario:");
            var nombreUsuario = Console.ReadLine();
            var usuario = SearchOrCreateUsuarioIfNotExist(nombreUsuario);

            if (HasFines(usuario.Id))
            {
                Console.WriteLine("Usted tiene multas pendientes. No puede alquilar mas libros");
                return;
            }

            Console.WriteLine("Introduzca el título del libro:");
            var titulo = Console.ReadLine();

            var libro = BibliotecaService.FindBook(titulo);

            if (!BibliotecaService.IsBookBorrowed(libro.Id))
            {
                if (CanBorrowBooks(usuario.Id))
                {
                    BibliotecaService.LoanBook(libro.Id, usuario.Id);

                    Console.WriteLine("Fecha del prestamo: " + DateTime.Now.Date.ToShortDateString());
                    Console.WriteLine("Préstamo realizado. Fecha de devolución: " + DateTime.Now.Date.AddMonths(1).ToShortDateString());
                    return;
                }

                Console.WriteLine("Usted tiene 3 o más libros en su poder, no puede realizar más préstamos");
                return;
            }
            Console.WriteLine("El libro " + titulo + " no está disponible ahora mismo.");
        }

        private static bool HasFines(int usuarioId)
        {
            return BibliotecaService.HasFines(usuarioId);
        }

        private static bool CanBorrowBooks(int usuarioId)
        {
           return BibliotecaService.CanBorrowBooks(usuarioId);
        }

        private static Usuarios SearchOrCreateUsuarioIfNotExist(string nombreUsuario)
        {
            if (!BibliotecaService.ExistUser(nombreUsuario))
            {
                BibliotecaService.CrearUsuario(nombreUsuario);
            }
            var usuario = BibliotecaService.FindUsuario(nombreUsuario);
            return usuario;
        }

        private static bool ShowAllBooks()
        {
            var books =  BibliotecaService.GetAllBooks();
            if (books.Count == 0)
            {
                Console.WriteLine("No tenemos libros disponibles, por favor registre uno.");
                return false;
            }
            Console.WriteLine("Estos son los libros que tenemos:");
            for (var i = 0; i < books.Count; i++)
            {
                Console.WriteLine(i +" - " +books[i].Titulo);
            }
            return true;
        }   

        private static void RegistrarLibro()
        {
            Console.WriteLine("Introduzca el título del libro:");
            var titulo = Console.ReadLine();

            BibliotecaService.RegistreBook(titulo);
        }
    }
}