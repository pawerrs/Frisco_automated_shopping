using System;
using System.Collections.Generic;
using System.Text;

namespace Frisco_automated_shopping.Extensions
{
    public static class ConsoleUtilities
    {
        public static void SetConsoleColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public static void DisplayError<T>(T value)
        {
            SetConsoleColor(ConsoleColor.Red);
            Console.WriteLine($"Wystąpił błąd. Dokładny opis: {value}");
            SetConsoleColor(ConsoleColor.White);
        }
    }
}
