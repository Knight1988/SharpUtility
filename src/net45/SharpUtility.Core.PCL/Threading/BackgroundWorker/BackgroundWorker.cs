using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpUtility.Threading
{
    public class BackgroundWorker
    {
        protected Action Action;
        protected Func<Task> ActionTask;

        /// <summary>
        /// Current progressPercentage
        /// </summary>
        public double ProgressPercentage { get; private set; }

        /// <summary>
        /// Create backgroundWorker without a task. 
        /// Task must be set in delivery class.
        /// </summary>
        protected BackgroundWorker()
        {
        }

        /// <summary>
        /// Create backgroundWorker run an async task
        /// </summary>
        /// <param name="action"></param>
        public BackgroundWorker(Func<Task> action)
        {
            ActionTask = action;
        }

        /// <summary>
        /// Create Background worker run a task
        /// </summary>
        /// <param name="action"></param>
        public BackgroundWorker(Action action)
        {
            Action = action;
        }

        /// <summary>
        /// Report progress
        /// </summary>
        /// <param name="progressPercentage"></param>
        public void ReportProgress(double progressPercentage)
        {
            ProgressPercentage = progressPercentage;
            OnProgressChanged(new ProgressChangedEventArgs(progressPercentage, null));
        }

        /// <summary>
        /// Report progress with userState
        /// </summary>
        /// <param name="progressPercentage"></param>
        /// <param name="userState"></param>
        public void ReportProgress(double progressPercentage, object userState)
        {
            ProgressPercentage = progressPercentage;
            OnProgressChanged(new ProgressChangedEventArgs(progressPercentage, userState));
        }

        /// <summary>
        /// Start the task
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task RunWorkerAsync(CancellationToken token)
        {
            if (Action != null)
            {
                await Task.Run(Action, token);
            }

            if (ActionTask != null)
            {
                await Task.Run(ActionTask, token);
            }
        }

        /// <summary>
        /// Start the task
        /// </summary>
        /// <returns></returns>
        public Task RunWorkerAsync()
        {
            if (Action != null)
            {
                return Task.Run(Action);
            }

            return ActionTask != null ? Task.Run(ActionTask) : Task.FromResult(true);
        }

        /// <summary>
        /// Raise complete event
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnCompleted(CompleteEventArgs e)
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