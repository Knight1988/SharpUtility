using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpUtility.Threading
{
    public class BackgroundWorker
    {
        public long ProgressPercentage { get; private set; }

        public void ReportProgress(long progressPercentage)
        {
            ProgressPercentage = progressPercentage;
            OnProgressChanged(new ProgressChangedEventArgs(progressPercentage, null));
        }

        public void ReportProgress(long progressPercentage, object userState)
        {
            ProgressPercentage = progressPercentage;
            OnProgressChanged(new ProgressChangedEventArgs(progressPercentage, userState));
        }

        public Task RunWorkerAsync(Func<Task> action, CancellationToken token)
        {
            return Task.Run(action, token);
        }

        public Task RunWorkerAsync(Action action, CancellationToken token)
        {
            return Task.Run(action, token);
        }

        public Task RunWorkerAsync(Func<Task> action)
        {
            return Task.Run(action);
        }

        public Task RunWorkerAsync(Action action)
        {
            return Task.Run(action);
        }

        protected virtual void OnCompleted(CompleteEventArgs e)
        {
            Completed?.Invoke(this, e);
        }

        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }

        public event EventHandler<CompleteEventArgs> Completed;

        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;
    }
}