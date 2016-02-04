namespace LazarilloApp.Implementation
{
    using LazarilloApp.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class ConsoleApp
    {
        static void Main(string[] args)
        {
            do
            {
                while (!Console.KeyAvailable)
                {
                    ConsoleKeyInfo name = Console.ReadKey();
                    Console.WriteLine("You pressed {0}", name.KeyChar);
                    //Console.WriteLine(Constants.LazarilloOpsMessage.MenuOptions.Escape);                
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        #region private
        private string Operation(string Op)
        {
            string result = "Opción no válida";
            ConstantsApp.AppMenuMessages.TryGetValue(Op, out result);
            return result;
        }

        #endregion private


    }
}
