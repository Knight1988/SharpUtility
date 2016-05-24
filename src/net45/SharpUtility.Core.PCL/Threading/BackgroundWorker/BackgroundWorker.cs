using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;
using Nito.AsyncEx.Synchronous;

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
        public async Task<T> ExecuteAsync<T>(Func<Task<T>> action, Func<Exception, Task> onRetry)
        {
            Re: try
            {
                return await action();
            }
            catch (Exception ex)
            {
                await onRetry(ex);
                await _autoResetEvent.WaitAsync();
                goto Re;
            }
        }

        /// <summary>
        /// Try execute action
        /// </summary>
        /// <param name="action"></param>
        /// <param name="onException"></param>
        public async Task<T> ExecuteAsync<T>(Func<Task<T>> action, Func<Exception, Task<T>> onException)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                return await onException(ex);
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

    public sealed class AsyncAutoResetEvent
    {
        /// <summary>
        /// The queue of TCSs that other tasks are awaiting.
        /// </summary>
        private readonly IAsyncWaitQueue<object> _queue;

        /// <summary>
        /// The current state of the event.
        /// </summary>
        private bool _set;

        /// <summary>
        /// The semi-unique identifier for this instance. This is 0 if the id has not yet been created.
        /// </summary>
        private int _id;

        /// <summary>
        /// The object used for mutual exclusion.
        /// </summary>
        private readonly object _mutex;

        /// <summary>
        /// Creates an async-compatible auto-reset event.
        /// </summary>
        /// <param name="set">Whether the auto-reset event is initially set or unset.</param>
        /// <param name="queue">The wait queue used to manage waiters.</param>
        public AsyncAutoResetEvent(bool set, IAsyncWaitQueue<object> queue)
        {
            _queue = queue;
            _set = set;
            _mutex = new object();
            //if (set)
            //    Enlightenment.Trace.AsyncAutoResetEvent_Set(this);
        }

        /// <summary>
        /// Creates an async-compatible auto-reset event.
        /// </summary>
        /// <param name="set">Whether the auto-reset event is initially set or unset.</param>
        public AsyncAutoResetEvent(bool set)
            : this(set, new DefaultAsyncWaitQueue<object>())
        {
        }

        /// <summary>
        /// Creates an async-compatible auto-reset event that is initially unset.
        /// </summary>
        public AsyncAutoResetEvent()
          : this(false, new DefaultAsyncWaitQueue<object>())
        {
        }

        /// <summary>
        /// Whether this event is currently set. This member is seldom used; code using this member has a high possibility of race conditions.
        /// </summary>
        public bool IsSet
        {
            get { lock (_mutex) return _set; }
        }

        /// <summary>
        /// Asynchronously waits for this event to be set. If the event is set, this method will auto-reset it and return immediately, even if the cancellation token is already signalled. If the wait is canceled, then it will not auto-reset this event.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used to cancel this wait.</param>
        public Task WaitAsync(CancellationToken cancellationToken)
        {
            Task ret;
            lock (_mutex)
            {
                if (_set)
                {
                    _set = false;
                    ret = TaskConstants.Completed;
                }
                else
                {
                    ret = _queue.Enqueue(_mutex, cancellationToken);
                }
                //Enlightenment.Trace.AsyncAutoResetEvent_TrackWait(this, ret);
            }

            return ret;
        }

        /// <summary>
        /// Synchronously waits for this event to be set. If the event is set, this method will auto-reset it and return immediately, even if the cancellation token is already signalled. If the wait is canceled, then it will not auto-reset this event. This method may block the calling thread.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token used to cancel this wait.</param>
        public void Wait(CancellationToken cancellationToken)
        {
            Task ret;
            lock (_mutex)
            {
                if (_set)
                {
                    _set = false;
                    return;
                }

                ret = _queue.Enqueue(_mutex, cancellationToken);
            }

            ret.WaitAndUnwrapException();
        }

        /// <summary>
        /// Asynchronously waits for this event to be set. If the event is set, this method will auto-reset it and return immediately.
        /// </summary>
        public Task WaitAsync()
        {
            return WaitAsync(CancellationToken.None);
        }

        /// <summary>
        /// Synchronously waits for this event to be set. If the event is set, this method will auto-reset it and return immediately. This method may block the calling thread.
        /// </summary>
        public void Wait()
        {
            Wait(CancellationToken.None);
        }

        /// <summary>
        /// Sets the event, atomically completing a task returned by <see cref="o:WaitAsync"/>. If the event is already set, this method does nothing.
        /// </summary>
        public void Set()
        {
            IDisposable finish = null;
            lock (_mutex)
            {
                //Enlightenment.Trace.AsyncAutoResetEvent_Set(this);
                if (_queue.IsEmpty)
                    _set = true;
                else
                    finish = _queue.Dequeue();
            }
            if (finish != null)
                finish.Dispose();
        }

        // ReSharper disable UnusedMember.Local
        [DebuggerNonUserCode]
        private sealed class DebugView
        {
            private readonly AsyncAutoResetEvent _are;

            public DebugView(AsyncAutoResetEvent are)
            {
                _are = are;
            }

            public bool IsSet { get { return _are._set; } }

            public IAsyncWaitQueue<object> WaitQueue { get { return _are._queue; } }
        }
        // ReSharper restore UnusedMember.Local
    }

    public class WorkerPolicy
    {
        
    }
}