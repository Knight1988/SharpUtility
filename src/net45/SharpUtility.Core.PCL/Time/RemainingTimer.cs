using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SharpUtility.Time {

    /// <summary>
    /// Copy from http://www.codeproject.com/Articles/27436/Remaining-Timer
    /// Useage:
    /// - Create object
    ///     var remainingTimer = new RemainingTimer();
    /// - Start timer 
    ///     remainingTimer.Start()
    /// - Set max value
    ///     remainingTimer.TargetValue = 100.0; 
    /// - Set time frame
    ///     remainingTimer.WindowDuration = new TimeSpan(0, 0, 45);
    /// - update value
    ///     remainingTimer.Mark(Value);
    /// - Call remainingTimer.ToString() to get the remaining time in string
    /// - Or call remainingTimer.GetRemainingEstimation() to get the timespan object for custom format
    /// </summary>
    public class RemainingTimer {
        /// <summary>
        ///     Calculate time left
        /// </summary>
        /// <param name="start">Time start</param>
        /// <param name="overallProgress">progress</param>
        /// <param name="totalProgress">total progress</param>
        /// <returns>Seconds</returns>
        public static double CalculateSecondsLeft(DateTime start, double overallProgress, double totalProgress)
        {
            double num;
            if (System.Math.Abs(overallProgress) >= 4.94065645841247E-324)
            {
                var totalSeconds = (DateTime.Now - start).TotalSeconds / overallProgress;
                num = totalSeconds * (totalProgress - overallProgress);
            }
            else
            {
                num = 0;
            }
            return num;
        }

        /// <summary>
        ///     Calculate time left
        /// </summary>
        /// <param name="start">Time start</param>
        /// <param name="overallProgress">progress</param>
        /// <param name="totalProgress">total progress</param>
        /// <returns>Timespan left</returns>
        public static TimeSpan CalulateTimeLeft(DateTime start, double overallProgress, double totalProgress)
        {
            var num = CalculateSecondsLeft(start, overallProgress, totalProgress);
            return TimeSpan.FromSeconds(num);
        }

        private class TimedValue {
            public TimedValue(long timeStamp, double value) {
                TimeStamp = timeStamp;
                Value = value;
            }

            public double Value { get; }

            public long TimeStamp { get; }
        }

        #region Members

        private readonly object _syncLock = new object();

        private readonly Stopwatch _sw; // Elapsed time stopwatch
        private readonly LinkedList<TimedValue> _data; // Collected data

        private double _lastSlope; // AKA m
        private double _lastYint; // AKA b
        private TimeSpan _lastElapsed;
        private bool _needToRecomputeEstimation; // only if there was a change from the last call.

        #endregion

        #region Ctor

        public RemainingTimer() {
            _sw = new Stopwatch();
            _data = new LinkedList<TimedValue>();
            TargetValue = 100.0;
            WindowDuration = new TimeSpan(0, 0, 45); // default window is 45 seconds.
            _needToRecomputeEstimation = true;
            _lastSlope = 0.0;
            _lastYint = 0.0;
            Correletion = 0.0;
        }

        #endregion

        #region Public

        /// <summary>
        /// Start timer
        /// </summary>
        public void Start() {
            lock (_syncLock) {
                _sw.Start();
            }
        }

        /// <summary>
        /// Pause timer
        /// </summary>
        public void Pause() {
            lock (_syncLock) {
                _sw.Stop();
            }
        }

        public void StopAndReset() {
            lock (_syncLock) {
                _sw.Stop();
                _data.Clear();
                _needToRecomputeEstimation = true;
                _lastSlope = 0.0;
                _lastYint = 0.0;
                Correletion = 0.0;
                _sw.Reset();
            }
        }

        public void Mark(double value) {
            TimedValue tv = new TimedValue(_sw.ElapsedMilliseconds, value);
            lock (_syncLock) {
                _data.AddFirst(tv);
                _needToRecomputeEstimation = true;
                ClearOutOfWindowData(); // remove out of window data
            }
        }

        public TimeSpan GetRemainingEstimation() {
            if (_needToRecomputeEstimation) {
                lock (_syncLock) {
                    ClearOutOfWindowData();
                    if (_data.Count != 0) {

                        ComputeLinearCoefficients(); // compute linear coefficients using linear regression
                        double remeiningMilliseconds = (TargetValue - _lastYint) / _lastSlope; //  y = m*x+b --> (y-b)/m = x

                        if (double.IsNaN(remeiningMilliseconds) || double.IsInfinity(remeiningMilliseconds)) {
                            _lastElapsed = TimeSpan.MaxValue; // no data ot invalid data
                        } else {
                            _lastElapsed = TimeSpan.FromMilliseconds(remeiningMilliseconds);
                        }

                        _needToRecomputeEstimation = false; // until data list is changed again.
                    }
                }
            }

            if (_lastElapsed != TimeSpan.MaxValue) {
                return _lastElapsed - _sw.Elapsed;
            }

            return TimeSpan.MaxValue;
        }

        /// <summary>
        /// Max value of the progress
        /// </summary>
        public double TargetValue { get; set; }

        /// <summary>
        /// Returns the "r" value which can indicate how "good" is the estimation (the closer to 1.0 the better)
        /// </summary>
        public double Correletion { get; private set; }

        /// <summary>
        /// The time frame increase the value if as longer the task, default = 45s
        /// </summary>
        public TimeSpan WindowDuration { get; set; }

        public override string ToString() {
            TimeSpan ts = GetRemainingEstimation();

            if (ts == TimeSpan.MaxValue) return "Unavailable";

            StringBuilder sb = new StringBuilder();
            if (ts.Days != 0) {
                sb.Append(ts.Days);
                sb.Append(".");
            }
            sb.Append(ts.Hours.ToString("D2"));
            sb.Append(":");
            sb.Append(ts.Minutes.ToString("D2"));
            sb.Append(":");
            sb.Append(ts.Seconds.ToString("D2"));
            return sb.ToString();
        }

        #endregion

        #region Private

        private void ClearOutOfWindowData() {
            lock (_syncLock) {
                if (_data.Count == 0) return;
                while (_data.First.Value.TimeStamp - _data.Last.Value.TimeStamp > (long)WindowDuration.TotalMilliseconds) {

                    _data.RemoveLast(); // Don't need this any more.
                    _needToRecomputeEstimation = true;

                    if (_data.Count == 0) return;
                }
            }
        }

        private void ComputeLinearCoefficients() {

            double sumTime = 0.0;
            double sumValue = 0.0;
            double sumValueTime = 0.0;
            double sumTime2 = 0.0;
            double sumValue2 = 0.0;
            int n = 0;

            lock (_syncLock) {
                IEnumerator<TimedValue> e = _data.GetEnumerator();
                while (e.MoveNext()) {
                    double d = e.Current.TimeStamp;
                    double val = e.Current.Value;
                    sumTime += d;
                    sumTime2 += d * d;
                    sumValue += val;
                    sumValue2 += val * val;
                    sumValueTime += val * d;
                    n++;
                }
            }

            if (n == 0) { // no data at all
                _lastSlope = 0.0;
                _lastYint = 0.0;
                Correletion = 0.0;
                return;
            }

            double sum2Time = sumTime * sumTime;
            double sum2Value = sumValue * sumValue;
            double ndouble = n;

            _lastSlope = ((ndouble * sumValueTime) - (sumTime * sumValue)) / ((ndouble * sumTime2) - sum2Time);
            _lastYint = (sumValue - _lastSlope * sumTime) / ndouble;
            Correletion = ((ndouble * sumValueTime) - (sumTime * sumValue)) / System.Math.Sqrt(((ndouble * sumTime2) - sum2Time) * ((ndouble * sumValue2) - sum2Value));
        }

        #endregion

    }

}
