using System;
using System.Runtime.CompilerServices;

namespace SharpUtility
{
    public class JobCounter
    {
        public string LastOutput { get; private set; } = string.Empty;

        public double MaxValue { get; set; }

        public double Value { get; set; }

        public JobCounter(double value, double maxValue)
        {
            Value = value;
            MaxValue = maxValue;
        }

        public JobCounter(double maxValue) : this(0, maxValue)
        {
        }

        public JobCounter DisplayToConsole()
        {
            var value = Value/MaxValue;
            var output = $"{value:P}";
            Console.SetCursorPosition(Console.CursorLeft - LastOutput.Length, Console.CursorTop);
            Console.Write(output);
            LastOutput = output;
            return this;
        }

        public JobCounter IncreaseValue(long increment = 1)
        {
            Value = Value + increment;
            return this;
        }
    }
}