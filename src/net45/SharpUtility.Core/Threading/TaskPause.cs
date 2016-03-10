using System.Threading;
using System.Threading.Tasks;

namespace SharpUtility.Threading
{
    /// <summary>
    /// Original: http://blogs.msdn.com/b/pfxteam/archive/2013/01/13/cooperatively-pausing-async-methods.aspx
    /// </summary>
    public class PauseTokenSource
    {
        internal static readonly Task CompletedTask = Task.FromResult(true);
        private TaskCompletionSource<bool> _paused;

        internal bool IsPaused
        {
            get { return _paused != null; }
            set
            {
                if (value)
                {
                    Interlocked.CompareExchange(
                        ref _paused, new TaskCompletionSource<bool>(), null);
                }
                else
                {
                    while (true)
                    {
                        var tcs = _paused;
                        if (tcs == null) return;
                        if (Interlocked.CompareExchange(ref _paused, null, tcs) == tcs)
                        {
                            tcs.SetResult(true);
                            break;
                        }
                    }
                }
            }
        }

        public PauseToken Token
        {
            get { return new PauseToken(this); }
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }

        internal Task WaitWhilePausedAsync()
        {
            var cur = _paused;
            return cur != null ? cur.Task : CompletedTask;
        }
    }

    public struct PauseToken
    {
        private readonly PauseTokenSource _source;

        internal PauseToken(PauseTokenSource source)
        {
            _source = source;
        }

        public bool IsPaused
        {
            get { return _source != null && _source.IsPaused; }
        }

        public Task WaitWhilePausedAsync()
        {
            return IsPaused
                ? _source.WaitWhilePausedAsync()
                : PauseTokenSource.CompletedTask;
        }
    }
}