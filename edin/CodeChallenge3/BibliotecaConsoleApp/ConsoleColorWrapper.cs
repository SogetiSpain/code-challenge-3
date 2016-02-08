using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaConsoleApp
{
    public class ConsoleColorWrapper : IDisposable
    {
        private readonly System.ConsoleColor _previousColor;
        public ConsoleColorWrapper(System.ConsoleColor color)
        {
            _previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }
        public void Dispose()
        {
            Console.ForegroundColor = _previousColor;
        }
    }
}
