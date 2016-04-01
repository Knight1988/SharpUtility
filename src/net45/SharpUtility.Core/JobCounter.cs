using System;

namespace SharpUtility
{
    public class JobCounter
    {
        private string _lastOutput;

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
            Console.SetCursorPosition(Console.CursorLeft - _lastOutput.Length, Console.CursorTop);
            Console.Write(output);
            _lastOutput = output;
            return this;
        }

        public JobCounter IncreaseValue()
        {
            Value++;
            return this;
        }
    }
}