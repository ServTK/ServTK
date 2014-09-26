using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServTK.Diagnostics
{
    public static class Logger
    {
        private const ConsoleColor _logColor = ConsoleColor.White;
        private const ConsoleColor _infoColor = ConsoleColor.Green;
        private const ConsoleColor _warningColor = ConsoleColor.Yellow;
        private const ConsoleColor _errorColor = ConsoleColor.Red;

        public static void Initialize()
        {
            Debug.Listeners.Add(new ConsoleTraceListener());
        }

        public static void Log(string message, params object[] args)
        {
            if (Core.Debug)
            {
                Console.ForegroundColor = _logColor;
                Console.WriteLine(message, args);
                Console.ForegroundColor = ConsoleColor.White;    
            }
        }

        public static void Info(string message, params object[] args)
        {
            Console.ForegroundColor = _infoColor;
            Console.WriteLine(message, args);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warning(string message, params object[] args)
        {
            Console.ForegroundColor = _warningColor;
            Console.WriteLine(message, args);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string message, params object[] args)
        {
            Console.ForegroundColor = _errorColor;
            Console.WriteLine(message, args);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Seperator()
        {
            Console.WriteLine(new string('-', Console.BufferWidth - 1));
        }
    }
}
