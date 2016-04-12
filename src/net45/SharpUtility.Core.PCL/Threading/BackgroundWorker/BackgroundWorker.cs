using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpUtility.Threading
{
    public class BackgroundWorker
    {
        private readonly Action _action;
        private readonly Func<Task> _actionTask;

        /// <summary>
        /// Current progressPercentage
        /// </summary>
        public long ProgressPercentage { get; private set; }

        /// <summary>
        /// Create backgroundWorker run an async task
        /// </summary>
        /// <param name="action"></param>
        public BackgroundWorker(Func<Task> action)
        {
            _actionTask = action;
        }

        /// <summary>
        /// Create Background worker run a task
        /// </summary>
        /// <param name="action"></param>
        public BackgroundWorker(Action action)
        {
            _action = action;
        }

        /// <summary>
        /// Report progress
        /// </summary>
        /// <param name="progressPercentage"></param>
        public void ReportProgress(long progressPercentage)
        {
            ProgressPercentage = progressPercentage;
            OnProgressChanged(new ProgressChangedEventArgs(progressPercentage, null));
        }

        /// <summary>
        /// Report progress with userState
        /// </summary>
        /// <param name="progressPercentage"></param>
        /// <param name="userState"></param>
        public void ReportProgress(long progressPercentage, object userState)
        {
            ProgressPercentage = progressPercentage;
            OnProgressChanged(new ProgressChangedEventArgs(progressPercentage, userState));
        }

        /// <summary>
        /// Start the task
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task RunWorkerAsync(CancellationToken token)
        {
            if (_action != null)
            {
                return Task.Run(_action, token);
            }

            if (_actionTask != null)
            {
                return Task.Run(_actionTask, token);
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Start the task
        /// </summary>
        /// <returns></returns>
        public Task RunWorkerAsync()
        {
            if (_action != null)
            {
                return Task.Run(_action);
            }

            if (_actionTask != null)
            {
                return Task.Run(_actionTask);
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Raise complete event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCompleted(CompleteEventArgs e)
        {
            Completed?.Invoke(this, e);
        }

        /// <summary>
        /// Raise progress changed event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Complete event
        /// </summary>
        public event EventHandler<CompleteEventArgs> Completed;

        /// <summary>
        /// Progress changed event
        /// </summary>
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;
    }
}