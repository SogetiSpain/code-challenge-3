using DataLibrary;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallengeIII
{
    class Program
    {
        public static void Main(string[] args)
        {
            List<Book> bookList = new BookService().GetAllBooks();
            List<Loan> loanList = new LoanService().GetAllLoans();
            ShowOptins();
            char op = Char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();
            while (op != ' ')
            {
                if (DoOperation(op, bookList, loanList))
                    ShowOptins();
                op = Char.ToLower(Console.ReadKey().KeyChar);
                Console.WriteLine();
            }

            WriteReport(bookList, loanList);
            new BookService().SaveAllBooks(bookList);
            new LoanService().SaveAllLoans(loanList);
            Console.Read();
        }

        private static bool DoOperation(char op, List<Book> bookList, List<Loan> loanList)
        {
            bool res = true;
            switch (op)
            {
                case 'r':
                    RegisterBook(bookList);
                    break;
                case 'p':
                    LoanBook(bookList, loanList);
                    break;
                case 'd':
                    ReturnBook(bookList, loanList);
                    break;
                default:
                    Console.WriteLine("\nWrong operation.");
                    res = false;
                    break;
            }
            return res;
        }

        private static void ShowOptins()
        {
            Console.WriteLine("¿Quiere (R)egistrar un libro, hacer un (P)réstamo o una (D)evolución? ");
            Console.WriteLine("Pulse espacio en blanco y Enter para salir.");
            Console.Write("Introduzca una opción: ");
        }

        private static void RegisterBook(List<Book> bookList)
        {
            Console.Write("Entre el identificador del libro: ");
            string code = Console.ReadLine();
            Console.Write("Entre el título del libro: ");
            string title = Console.ReadLine();
            Book book = new Book(title, code);
            if (!bookList.Contains(book))
            {
                bookList.Add(book);
                Console.WriteLine("Libro registrado correctamente.");
            }
            else Console.WriteLine("Libro ya existente!");
        }

        private static void LoanBook(List<Book> bookList, List<Loan> loanList)
        {
            foreach (Book b in bookList.Where(a => a.IsTaken == false))
            {
                Console.WriteLine(b.Code + " - " + b.Title);
            }
            Console.Write("Introduzca el código del libro que desea: ");
            string loanCode = Console.ReadLine();
            Book loanBook = bookList.FirstOrDefault(a => a.Code == loanCode);
            Console.Write("Enter your username: ");
            string user = Console.ReadLine();
            if (loanList.Count(a => a.User == user) >= 3)
            {
                Console.WriteLine("Lo sentimos, el máximo de libros permitidos es 3.");
            }
            else
            {
                if (loanBook != null)
                {
                    Loan loan = new Loan(loanCode, user, DateTime.Now);
                    loanList.Add(loan);
                    loanBook.IsTaken = true;
                    Console.WriteLine("Libro alquilado:"+ loanBook.Title+", a devolver antes del:"+loan.DateReturn);
                }
                else
                {
                    Console.WriteLine("Libro equivocado.");
                }
            }

        }

        private static void ReturnBook(List<Book> bookList, List<Loan> loanList)
        {
            Console.Write("Introduzca el código del libro que desea devolver: ");
            string returnCode = Console.ReadLine();
            Loan returnLoan = loanList.FirstOrDefault(a => a.Book == returnCode);
            if (returnLoan != null)
            {
                if (DateTime.Now > returnLoan.DateReturn)
                {
                    Console.WriteLine("Pagar multa :");
                    Book feeBook = bookList.FirstOrDefault(a => a.Code == returnCode);
                    feeBook.IsTaken = false;
                    Console.ReadLine();
                }
                else
                {

                    Book bookAux = bookList.FirstOrDefault(a => a.Code == returnCode);
                    bookAux.IsTaken = false;
                }
            }

        }

        private static void WriteReport(List<Book> bookList, List<Loan> loanList)
        {
            string path = "C:\\Library.txt";
            using (var streamWriter = new StreamWriter(path, true))
            {
                streamWriter.WriteLine("Library Status: ");
                foreach (Book a in bookList)
                {
                    if (a.IsTaken)
                    {
                        Loan loan = loanList.Where(b => b.Book == a.Code).FirstOrDefault();
                        if (DateTime.Now > loan.DateReturn)
                        {
                            streamWriter.WriteLine(a.Code + ", Loan by: " + loan.User + ", on " + loan.DateLoan + " Must pay fine!!");
                        }
                        else
                        {
                            streamWriter.WriteLine(a.Code + ", Loan by: " + loan.User + ", on " + loan.DateLoan);
                        }

                    }
                    else streamWriter.WriteLine(a.Code + " is available");
                }
                streamWriter.WriteLine("|--------- o ---------|");
            }
        }
    }
}
