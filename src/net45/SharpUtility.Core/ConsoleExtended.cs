using System;

namespace SharpUtility
{
    public static class ConsoleExtended
    {
        private static readonly object _lock = new object();
        public static void WritePercent(double value, double total)
        {
            WritePercent(value/total);
        }

        public static void WritePercent(double value)
        {
            lock (_lock)
            {
                var output = string.Format("{0:P}    ", value);
                Console.Write(output);
                Console.SetCursorPosition(Console.CursorLeft - output.Length, Console.CursorTop);
            }
        }

        public static void WritePercentLine(double value)
        {
            Console.WriteLine("{0:P}", value);
        }

        public static void WritePercentLine(double value, double total)
        {
            WritePercentLine(value/total);
        }
    }
}
