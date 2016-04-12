using System;
using System.Linq;
using System.Threading.Tasks;
using SharpUtility.Time;

namespace SharpUtility.Common
{
    public class JobCounter
    {
        /// <summary>
        ///     RemainingTimer
        /// </summary>
        private readonly RemainingTimer _remainingTimer = new RemainingTimer();

        /// <summary>
        ///     LastOutput of the console
        /// </summary>
        public string LastOutput { get; private set; } = string.Empty;

        /// <summary>
        ///     Max value
        /// </summary>
        public double MaxValue { get; set; }

        /// <summary>
        ///     Current value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        ///     Allow display to console in silent mode
        /// </summary>
        public bool DisplayToConsole { get; set; } = true;

        /// <summary>
        ///     Allow display remaining time
        /// </summary>
        public bool DisplayRemainingTime { get; set; } = true;

        /// <summary>
        ///  check JobCounter is running
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        ///     Init JobCounter with value and max
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxValue"></param>
        public JobCounter(double value, double maxValue)
        {
            Value = value;
            MaxValue = maxValue;
        }

        /// <summary>
        ///     Init JobCounter with 0 value and max
        /// </summary>
        /// <param name="maxValue"></param>
        public JobCounter(double maxValue) : this(0, maxValue)
        {
        }

        /// <summary>
        ///     Display value in percent
        /// </summary>
        /// <returns></returns>
        public JobCounter WriteToConsole()
        {
            var output = ToString();
            // Set cursor to begin
            Console.SetCursorPosition(Console.CursorLeft - LastOutput.Length, Console.CursorTop);
            // Clear output
            Console.Write(new string(LastOutput.Select(p => ' ').ToArray()));
            // Set cursor to begin
            Console.SetCursorPosition(Console.CursorLeft - LastOutput.Length, Console.CursorTop);
            // write output
            Console.Write(output);
            LastOutput = output;
            return this;
        }

        /// <summary>
        ///     Run silent until value >= max
        /// </summary>
        public async void RunSilent()
        {
            await Task.Yield();

            if (IsRunning) return;
            IsRunning = true;

            // Init remaining timer
            _remainingTimer.StopAndReset();
            _remainingTimer.Start();
            _remainingTimer.TargetValue = MaxValue;
            _remainingTimer.WindowDuration = new TimeSpan(0, 0, 45);

            // Display value every 250ms
            while (Value < MaxValue)
            {
                if (DisplayRemainingTime) _remainingTimer.Mark(Value);
                if (DisplayToConsole) WriteToConsole();
                await Task.Delay(250);
            }

            // write last value on finish
            if (System.Math.Abs(Value - MaxValue) < double.Epsilon) WriteToConsole();
            IsRunning = false;
        }

        /// <summary>
        /// Increase the value
        /// </summary>
        /// <param name="increment">value to increase</param>
        /// <returns></returns>
        public JobCounter IncreaseValue(long increment = 1)
        {
            Value = Value + increment;
            return this;
        }

        /// <summary>
        /// Retrn job done by percent
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var value = Value/MaxValue;
            if (DisplayRemainingTime)
            {
                return $"{value:P} - {_remainingTimer}";
            }
            return $"{value:P}";
        }
    }
}