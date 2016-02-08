using Biblioteca;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaConsoleApp
{

    class Program
    {
       
        private static BibliotecaService bibliotecaService; 

        static void Main(string[] args)
        {
            Console.WriteLine("Sistema de Biblioteca.");

            InitializeDependencies();

            do
            {
                OperationType operacion = GetOperationFromUser();
                try
                {
                    switch (operacion)
                    {
                        case OperationType.Registro:
                            RegisterItem();
                            break;
                        case OperationType.Prestamo:
                            LendItem();
                            break;
                        case OperationType.Devolucion:
                            ReturnItem();
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    using (new ConsoleColorWrapper(ConsoleColor.Red))
                    {
                        Console.WriteLine(String.Format("ERROR: {0}", ex.Message));
                    }
                }
                
            } while (true);
        }

        private static void RegisterItem()
        {
            var item = GetBookDetails();
            bibliotecaService.Register(item);
        }

        private static void LendItem()
        {
            var identifier = GetItemReference();
            var userId = GetUserReference();
            var lendingDate = GetOperationDate();

            bibliotecaService.Lend(identifier, userId, lendingDate);
        }

        private static void ReturnItem()
        {
            var identifier = GetItemReference();
            var userId = GetUserReference();
            var returnDate = GetOperationDate();

            var result = bibliotecaService.Return(identifier, userId, returnDate);
            if (result == UserStatus.Blocked)
            {
                UnblockUser(userId);
            }
        }

        private static void UnblockUser(string userId)
        {
            var isUnblocked = false;
            using (new ConsoleColorWrapper(ConsoleColor.Red))
            {
                Console.Write("El usuario está bloqueado. ¿Quiere pagar la multa para desbloquearlo? (S/N)");
                var input = Console.ReadLine();

                if (input.ToLowerInvariant() == "s")
                {
                    isUnblocked = true;
                    bibliotecaService.Unblock(userId);                    
                }
            }
            if (isUnblocked)
            {
                Console.WriteLine("El usuario está desbloqueado.");
            }
        }

        private static DateTime GetOperationDate()
        {
            bool isValidDate = true;
            DateTime operationDate = DateTime.Now;

            do
            {
                Console.Write("Introduzca la fecha (o INTRO para usar la fecha de hoy): ");
                var operationDateText = Console.ReadLine();

                if (!String.IsNullOrEmpty(operationDateText))
                {
                    isValidDate = DateTime.TryParse(operationDateText, out operationDate);
                }
            } while (!isValidDate);
            return operationDate;
        }

        private static string GetUserReference()
        {
            Console.Write("Introduzca el usuario: ");
            var userId = Console.ReadLine();
            return userId;
        }

        private static Book GetBookDetails()
        {
            Console.Write("Introduzca el identificador del libro: ");
            var identifier = Console.ReadLine();
            Console.Write("Introduzca el nombre del libro:");
            var nombreLibro = Console.ReadLine();

            var item = new Book() { ID = identifier, Name = nombreLibro };
            return item;
        }

        private static string GetItemReference()         {
            Console.Write("Introduzca el identificador del libro: ");
            var identifier = Console.ReadLine();
            return identifier;
        }


        private static void InitializeDependencies()
        {
            var users = new List<User>();
            users.Add(new User() { Username = "edin" });
            users.Add(new User() { Username = "roberto" });

            var bibliotecaStore = new InMemoryBibliotecaStore(users);
            bibliotecaService = new BibliotecaService(bibliotecaStore);
        }

        private static OperationType GetOperationFromUser()
        {
            var operationMapping = CreateOperationTypeMapping();

            string userInput = String.Empty;
            do
            {
                Console.Write("¿Quiere (R)egistrar un libro, hacer un (P)réstamo o una (D)evolución:");
                userInput = Console.ReadLine();
            } while (!IsValidInput(userInput));

            return operationMapping[userInput.ToLower()];
        }

        private static IDictionary<string, OperationType> CreateOperationTypeMapping()
        {
            Dictionary<string, OperationType> operationMapping = new Dictionary<string, OperationType>();
            operationMapping.Add("r", OperationType.Registro);
            operationMapping.Add("p", OperationType.Prestamo);
            operationMapping.Add("d", OperationType.Devolucion);
            return operationMapping;
        }

        private static bool IsValidInput(string userInput)
        {
            bool isValid = false;
            if (userInput.ToLower() == "r" || userInput.ToLower() == "p" || userInput.ToLower() == "d")
            {
                isValid = true;
            }
            return isValid;
        }
    }
}
