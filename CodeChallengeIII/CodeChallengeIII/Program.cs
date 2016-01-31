using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallengeIII
{
    class Program
    {
        public static void Main(string[] args)
        {
            char option = Console.ReadKey().KeyChar;
            ReadOption(option);
            Console.ReadLine();            
        }

        private static void ReadOption(char option)
        {
            switch (option)
            {
                case 'R':
                    break;
                case 'T':
                    break;
                case 'B':
                    break;
                case 'F':
                    break;
                default:
                    Console.WriteLine("Wrong Operation, try again.");
                    break;
            }
        }
    }
}
