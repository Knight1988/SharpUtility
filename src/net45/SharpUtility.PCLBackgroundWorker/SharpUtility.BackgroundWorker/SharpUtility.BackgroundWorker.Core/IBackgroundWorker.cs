using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpUtility.Threading
{
    public interface IBackgroundWorker
    {
        long ProgressPercentage { get; }
        void ReportProgress(long progressPercentage);
        void ReportProgress(long progressPercentage, object userState);
        Task RunWorkerAsync(Action action);
        Task RunWorkerAsync(Action action, CancellationToken token);
        Task RunWorkerAsync(Func<Task> action);
        Task RunWorkerAsync(Func<Task> action, CancellationToken token);
        event EventHandler<ProgressChangedEventArgs> ProgressChanged;
        event EventHandler<CompleteEventArgs> Completed;
    }
}