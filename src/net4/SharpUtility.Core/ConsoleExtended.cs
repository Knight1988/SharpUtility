using System;

namespace SharpUtility.Core
{
    public static class ConsoleExtended
    {
        public static void WritePercent(double value, double total)
        {
            WritePercent(value/total);
        }

        public static void WritePercent(double value)
        {
            var output = string.Format("{0:P}    ", value);
            Console.Write(output);
            Console.SetCursorPosition(Console.CursorLeft - output.Length, Console.CursorTop);
        }
    }
}
