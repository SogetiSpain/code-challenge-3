using Castle.Windsor;
using IServiceLayer.Models;
using IServiceLayer.Services;
using ServiceLayer.IoC;
using ServiceLayer.IoC.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        private static IWindsorContainer container;      
        private static string inputValue = string.Empty;
        private static DateTime inputDateValue = DateTime.MinValue;
        private static IBooksService libraryService;
        private static LibraryBooksOperationResult result;

        static void Main(string[] args)
        {
            Console.WriteLine(Resource.Apptitle);
            container = WindsorCastleContainer.Instance;
            ConfigureIoC.Configure(container);
            libraryService = container.Resolve<IBooksService>();           

            while (true)
            {
                AskForValue(Resource.SelectOperation);

                OperationDispatcher(inputValue);

                AskToContinue();
            }
        }

        private static void OperationDispatcher(string inputValue)
        {
            switch (inputValue)
            {
                case "R":
                    Register();
                    break;
                case "P":
                    Loan();
                    break;
                case "D":
                    Return();
                    break;
            }
        }

        private static void Register()
        {
            var infoReq = new InfoRequest();
            AskForValue(Resource.EnterName);
            infoReq.Username = inputValue;
            AskForValue(Resource.EnterBook);
            infoReq.BookCode = inputValue;            

            result = libraryService.ExecuteBookOperation(infoReq).Result;

            if (result.Success)
            {
                PrintMessage(Resource.RegisterBookSuccess);
            }
            else
            {
                PrintMessage(result.Message.ToString());
            }
        }

        private static void Loan()
        {
            var infoReq = new InfoRequest();
            AskForValue(Resource.EnterName);
            infoReq.Username = inputValue;
            AskForValue(Resource.EnterBook);
            infoReq.BookCode = inputValue;
            AskForDate(Resource.EnterLoanDate);
            infoReq.LoanDate = inputDateValue;

            result = libraryService.ExecuteBookOperation(infoReq).Result;

            if (result.Success)
            {
                PrintMessage(Resource.LoanBookSuccess);
            }
            else
            {
                PrintMessage(result.Message.ToString());
            }
        }

        private static void Return()
        {
            var infoReq = new InfoRequest();
            AskForValue(Resource.EnterName);
            infoReq.Username = inputValue;
            AskForValue(Resource.EnterBook);
            infoReq.BookCode = inputValue;
            AskForDate(Resource.EnterReturnDate);
            infoReq.ReturnDate = inputDateValue;

            result = libraryService.ExecuteBookOperation(infoReq).Result;

            if (result.Success)
            {
                PrintMessage(Resource.ReturnBookSuccess);
            }
            else
            {
                PrintMessage(result.Message.ToString());
            }
        }

        private static void AskForValue(string questionMsg)
        {
            Console.WriteLine(questionMsg);
            inputValue = Console.ReadLine();
        }

        private static void AskForDate(string questionMsg)
        {
            Console.WriteLine(questionMsg);
            inputDateValue = DateTime.Parse(Console.ReadLine());
        }

        private static void AskToContinue()
        {
            Console.WriteLine(Resource.EnterKeyToContinue);
            Console.ReadKey();
            Console.Clear();
        }

        private static void PrintMessage(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
