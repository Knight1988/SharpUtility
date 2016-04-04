using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpUtility.Threading
{
    public class BackgroundWorker : IBackgroundWorker
    {
        public event EventHandler<CompleteEventArgs> Completed;

        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

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

        public static IBackgroundWorker Create()
        {
            return Create<BackgroundWorker>();
        }

        public static IBackgroundWorker Create<T>() where T : IBackgroundWorker, new()
        {
            return new T();
        }

        protected virtual void OnCompleted(CompleteEventArgs e)
        {
            Completed?.Invoke(this, e);
        }

        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }
    }
}