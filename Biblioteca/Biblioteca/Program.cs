using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    class Program
    {

        private static readonly BibliotecaService BibliotecaService = new BibliotecaService();

        static void Main(string[] args)
        {
            var salir = false;

            do
            {
                Console.WriteLine("¿Quiere (R)egistrar un libro, hacer un (P)réstamo o una (D)evolución:");

                var option = Console.ReadLine();

                if (option == null) continue;
                switch (option.ToUpper())
                {
                    case "R":
                        RegistrarLibro();
                        salir = true;
                        break;
                    case "P":
                        PrestarLibro();
                        salir = true;
                        break;

                    case "D":
                        DevolverLibro();
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
            Console.WriteLine("Introduzca el título del libro:");
            var titulo = Console.ReadLine();
            var libro = BibliotecaService.Find(titulo);

            BibliotecaService.ReturnBook(libro.Id);

            Console.WriteLine("La fecha de devolución es: " + DateTime.Now.Date.ToShortDateString());
            Console.WriteLine("El libro " + titulo+ " está disponible para ser prestado");

        }

        private static void PrestarLibro()
        {
            Console.WriteLine("Introduzca el título del libro:");
            var titulo = Console.ReadLine();
            var libro = BibliotecaService.Find(titulo);
            if (libro.Disponible != null && libro.Disponible.Value)
            {
                BibliotecaService.LoanBook(libro.Id);

                Console.WriteLine("Fecha del prestamo: "+ DateTime.Now.Date.ToShortDateString());
                Console.WriteLine("Préstamo realizado. Fecha de devolución: " + (DateTime.Now.Date.AddMonths(1).ToShortDateString()));
            }
            else
            {
                Console.WriteLine("El libro " + titulo + " no está disponible ahora mismo.");
            }
        }

        private static void RegistrarLibro()
        {
            Console.WriteLine("Introduzca el título del libro:");
            var titulo = Console.ReadLine();

            BibliotecaService.RegistreBook(titulo);

        }
    }
}
