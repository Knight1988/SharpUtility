using System;

namespace SharpUtility
{
    public static class ConsoleExtended
    {
        private static readonly object Lock = new object();

        /// <summary>
        /// Write percent number
        /// </summary>
        /// <param name="value">current value</param>
        /// <param name="total">total</param>
        public static void WritePercent(double value, double total)
        {
            WritePercent(value/total);
        }

        /// <summary>
        /// Write percent number
        /// </summary>
        /// <param name="value"></param>
        public static void WritePercent(double value)
        {
            lock (Lock)
            {
                var output = $"{value:P}\t";
                Console.Write(output);
                Console.SetCursorPosition(Console.CursorLeft - output.Length, Console.CursorTop);
            }
        }

        /// <summary>
        /// Write percent number line
        /// </summary>
        /// <param name="value"></param>
        public static void WritePercentLine(double value)
        {
            Console.WriteLine("{0:P}", value);
        }

        /// <summary>
        /// Write percent number line
        /// </summary>
        /// <param name="value"></param>
        /// <param name="total"></param>
        public static void WritePercentLine(double value, double total)
        {
            WritePercentLine(value/total);
        }

        /// <summary>
        /// Write a text with x position and max length
        /// </summary>
        /// <param name="format"></param>
        /// <param name="left"></param>
        /// <param name="maxLength"></param>
        /// <param name="arg"></param>
        public static void Write(string format, int left, int maxLength, params object[] arg)
        {
            var top = Console.CursorTop;
            Console.SetCursorPosition(left, top);

            if (format.Length > maxLength)
            {
                format = format.Substring(maxLength - format.Length);
            }

            Console.Write(format, arg);
        }
    }
}
