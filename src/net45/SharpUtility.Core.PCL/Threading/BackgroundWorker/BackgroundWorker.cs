using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpUtility.Threading
{
    public class BackgroundWorker
    {
        private readonly Func<Task> _actionTask;
        private readonly Action _action;

        public BackgroundWorker(Func<Task> action)
        {
            _actionTask = action;
        }

        public BackgroundWorker(Action action)
        {
            _action = action;
        }

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