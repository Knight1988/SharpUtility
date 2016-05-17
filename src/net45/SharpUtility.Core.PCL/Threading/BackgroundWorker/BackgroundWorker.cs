using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpUtility.Threading
{
    public class BackgroundWorker
    {
        public async Task RunWorkerAsync()
        {
            await Task.Factory.StartNew(() => OnDoWork(new DoWorkEventArgs()), TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Report the task progress
        /// </summary>
        /// <param name="progressPercentage"></param>
        /// <param name="userState"></param>
        public void ReportProgress(double progressPercentage, object userState)
        {
            OnProgressChanged(new ProgressChangedEventArgs(progressPercentage, userState));
        }

        /// <summary>
        /// Report the task progress
        /// </summary>
        /// <param name="progressPercentage"></param>
        public void ReportProgress(double progressPercentage)
        {
            OnProgressChanged(new ProgressChangedEventArgs(progressPercentage, null));
        }

        /// <summary>
        /// Report the task progress
        /// </summary>
        /// <param name="max"></param>
        /// <param name="userState"></param>
        /// <param name="value"></param>
        public void ReportProgress(double value, double max, object userState)
        {
            OnProgressChanged(new ProgressChangedEventArgs(value/max, userState));
        }

        /// <summary>
        /// Report the task progress
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        public void ReportProgress(double value, double max)
        {
            OnProgressChanged(new ProgressChangedEventArgs(value/max, null));
        }

        public event EventHandler<DoWorkEventArgs> DoWork;

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

        protected virtual void OnDoWork(DoWorkEventArgs e)
        {
            DoWork?.Invoke(this, e);
        }
    }
}