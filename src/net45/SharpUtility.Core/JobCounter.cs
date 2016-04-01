using System;
using System.Runtime.CompilerServices;

namespace SharpUtility
{
    public class JobCounter
    {
        public string LastOutput { get; private set; } = string.Empty;

        public long MaxValue { get; set; }

        public long Value { get; set; }

        public JobCounter(long value, long maxValue)
        {
            Value = value;
            MaxValue = maxValue;
        }

        public JobCounter(long maxValue) : this(0, maxValue)
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