using System;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace SharpUtility.Threading
{
    public class BackgroundWorker : IBackgroundWorker
    {
        readonly AsyncAutoResetEvent _autoResetEvent = new AsyncAutoResetEvent(false);

        /// <summary>
        /// Try execute action
        /// </summary>
        /// <param name="action"></param>
        /// <param name="onRetry"></param>
        public async void ExecuteAsync(Func<Task> action, Func<Task> onRetry)
        {
            Re: try
            {
                await action();
            }
            catch (Exception)
            {
                await onRetry();
                await _autoResetEvent.WaitAsync();
                goto Re;
            }
        }

        public void Resume()
        {
            _autoResetEvent.Set();
        }

        /// <summary>
        ///     Report the task progress
        /// </summary>
        /// <param name="progressPercentage"></param>
        /// <param name="userState"></param>
        public void ReportProgress(double progressPercentage, object userState)
        {
            OnProgressChanged(new ProgressChangedEventArgs(progressPercentage, userState));
        }

        /// <summary>
        ///     Report the task progress
        /// </summary>
        /// <param name="progressPercentage"></param>
        public void ReportProgress(double progressPercentage)
        {
            ReportProgress(progressPercentage, null);
        }

        /// <summary>
        ///     Report the task progress
        /// </summary>
        /// <param name="max"></param>
        /// <param name="userState"></param>
        /// <param name="value"></param>
        public void ReportProgress(double value, double max, object userState)
        {
            ReportProgress(value/max, userState);
        }

        /// <summary>
        ///     Report the task progress
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        public void ReportProgress(double value, double max)
        {
            ReportProgress(value/max, null);
        }

        public event EventHandler<DoWorkEventArgs> DoWork;

        /// <summary>
        ///     Complete event
        /// </summary>
        public event EventHandler<CompleteEventArgs> Completed;

        /// <summary>
        ///     Progress changed event
        /// </summary>
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        public async Task RunWorkerAsync(params object[] arguments)
        {
            await Task.Factory.StartNew(() => OnDoWork(new DoWorkEventArgs(arguments)), TaskCreationOptions.LongRunning);
        }

        /// <summary>
        ///     Raise complete event
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnCompleted(CompleteEventArgs e)
        {
            Completed?.Invoke(this, e);
        }

        /// <summary>
        ///     Raise progress changed event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressPercentage = e.ProgressPercentage;
            ProgressChanged?.Invoke(this, e);
        }

        public double ProgressPercentage { get; set; }

        protected virtual void OnDoWork(DoWorkEventArgs e)
        {
            DoWork?.Invoke(this, e);
        }

        public override string ToString()
        {
            return ProgressPercentage.ToString("P");
        }
    }

    public class WorkerPolicy
    {
        
    }
}