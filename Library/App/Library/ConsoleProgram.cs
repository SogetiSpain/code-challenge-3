namespace Library.App.Program
{
    using Library.App.Program.Implementation;
    using Library.App.Program.Interface;
    using Castle.DynamicProxy;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Infrastructure.Log.Installer;
    using Library.App.Program.Code.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class ConsoleProgram
    {  
        static void Main(string[] args)
        {
            LibraryApp libApp = new LibraryApp();
            libApp.Initialization();
            libApp.Run();
        }

    }
}
