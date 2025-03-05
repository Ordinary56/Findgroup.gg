using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace WPF.Helpers
{
    public static class ConsoleHelper
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        private static TextWriter? _originalConsoleOut;

        public static void CreateConsole()
        {
            if (Debugger.IsAttached) // Csak debug módban nyíljon
            {
                if (AllocConsole())
                {
                    _originalConsoleOut = Console.Out;
                    Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
                    Console.WriteLine("Debug console initialized.");
                }
            }
        }

        public static void CloseConsole()
        {
            if (Debugger.IsAttached && _originalConsoleOut != null)
            {
                Console.SetOut(_originalConsoleOut);
                FreeConsole();
            }
        }
    }
}
