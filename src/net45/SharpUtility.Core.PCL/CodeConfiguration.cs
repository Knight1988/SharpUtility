using System;
using System.Threading;
using System.Threading.Tasks;
using SharpUtility.Core.Threading;

namespace SharpUtility.Core
{
    public class CodeConfiguration
    {
        protected TimeSpan Delay { get; set; }
        protected int MaxRetries { get; set; }
        protected TimeSpan RetryDelay { get; set; }

        public CodeConfiguration()
        {
            RetryIfFail(3);
            RunAfter(0);
            DelayBetweenEachRetry(1000);
        }

        public CodeConfiguration DelayBetweenEachRetry(int millisecondsTimeout)
        {
            return DelayBetweenEachRetry(new TimeSpan(0, 0, 0, 0, millisecondsTimeout));
        }

        public CodeConfiguration DelayBetweenEachRetry(TimeSpan timeout)
        {
            RetryDelay = timeout;
            return this;
        }

        public async Task ExecuteAsync(Func<Task> func)
        {
            await ExecuteAsync(func, null);
        }

        public async Task ExecuteAsync(Func<Task> func, Func<Exception, Task> onError)
        {
            await Task.Delay(Delay);
            var num = 0;

            Retry:
            try
            {
                await func();
            }
            catch (Exception e)
            {
                num++;
                if (num >= MaxRetries)
                {
                    if (onError == null) throw;
                    await onError(e);
                    return;
                }

                await Task.Delay(RetryDelay);
                goto Retry;
            }
        }

        public async Task<T> ExecuteAsync<T>(Func<T> func)
        {
            return await ExecuteAsync(func, null);
        }

        public async Task<T> ExecuteAsync<T>(Func<T> func, Func<Exception, T> onError)
        {
            await Task.Delay(Delay);
            var num = 0;

            Retry:
            try
            {
                return func();
            }
            catch (Exception e)
            {
                num++;
                if (num >= MaxRetries)
                {
                    if (onError == null) throw;
                    return onError(e);
                }

                await Task.Delay(RetryDelay);
                goto Retry;
            }
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> func)
        {
            return await ExecuteAsync(func, null);
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> func, Func<Exception, Task<T>> onError)
        {
            await Task.Delay(Delay);
            var num = 0;

            Retry:
            try
            {
                return await func();
            }
            catch (Exception e)
            {
                num++;
                if (num >= MaxRetries)
                {
                    if (onError == null) throw;
                    return await onError(e);
                }

                await Task.Delay(RetryDelay);
                goto Retry;
            }
        }

        public Task ExecuteAsync(Action action)
        {
            return ExecuteAsync(action, null);
        }

        public async Task ExecuteAsync(Action action, Action<Exception> onError)
        {
            await Task.Delay(Delay);
            var num = 0;
            Retry:
            try
            {
                action();
            }
            catch (Exception e)
            {
                num++;
                if (num >= MaxRetries)
                {
                    if (onError == null) throw;
                    onError(e);
                    return;
                }

                await Task.Delay(RetryDelay);
                goto Retry;
            }
        }

        public CodeConfiguration RetryIfFail(int maxRetries)
        {
            MaxRetries = maxRetries;
            return this;
        }

        public CodeConfiguration RunAfter(int millisecondsTimeout)
        {
            return RunAfter(new TimeSpan(0, 0, 0, 0, millisecondsTimeout));
        }

        public CodeConfiguration RunAfter(TimeSpan timeout)
        {
            Delay = timeout;
            return this;
        }
    }
}