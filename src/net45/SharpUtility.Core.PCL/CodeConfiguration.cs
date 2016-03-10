using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpUtility
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
            DelayBetweenEachRetry(0);
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

        public CodeConfiguration DelayBetweenEachRetry(int millisecondsTimeout)
        {
            return DelayBetweenEachRetry(new TimeSpan(0, 0, 0, 0, millisecondsTimeout));
        }

        public CodeConfiguration DelayBetweenEachRetry(TimeSpan timeout)
        {
            RetryDelay = timeout;
            return this;
        }

        #region ExecuteAsync

        /// <summary>
        ///     Execute a async function
        /// </summary>
        /// <param name="func">func to run</param>
        /// <returns></returns>
        public async Task ExecuteAsync(Func<Task> func)
        {
            await ExecuteAsync(func, null);
        }

        /// <summary>
        ///     Execute a async function
        /// </summary>
        /// <param name="func">func to run</param>
        /// <param name="onError">call this function on exception</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Execute a function then retun value
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="func">func to run</param>
        /// <returns></returns>
        public async Task<T> ExecuteAsync<T>(Func<T> func)
        {
            return await ExecuteAsync(func, null);
        }

        /// <summary>
        ///     Execute a function then retun value
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="func">func to run</param>
        /// <param name="onError">return this function value on exception</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Execute a async function
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="func">function to run</param>
        /// <returns></returns>
        public async Task<T> ExecuteAsync<T>(Func<Task<T>> func)
        {
            return await ExecuteAsync(func, null);
        }

        /// <summary>
        ///     Execute a async function
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="func">function to run</param>
        /// <param name="onError">run then return this func value on exception</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Run an action
        /// </summary>
        /// <param name="action">action to run</param>
        /// <returns></returns>
        public Task ExecuteAsync(Action action)
        {
            return ExecuteAsync(action, null);
        }

        /// <summary>
        ///     Run an action
        /// </summary>
        /// <param name="action">action to run</param>
        /// <param name="onError">call this action on exception</param>
        /// <returns></returns>
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

        #endregion

#if !PCL
#region Execute

        /// <summary>
        /// Execute an action
        /// </summary>
        /// <param name="action">action to run</param>
        public void Execute(Action action)
        {
            Execute(action, null);
        }

        /// <summary>
        /// Execute an action
        /// </summary>
        /// <param name="action">action to run</param>
        /// <param name="onError">run this action on exception</param>
        public void Execute(Action action, Action<Exception> onError)
        {
            Thread.Sleep(Delay);
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

                Thread.Sleep(RetryDelay);
                goto Retry;
            }
        }

        /// <summary>
        /// Execute a function then return value
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="func">func to run</param>
        /// <returns></returns>
        public T Execute<T>(Func<T> func)
        {
            return Execute(func, null);
        }

        /// <summary>
        /// Execute a function then return value
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="func">func to run</param>
        /// <param name="onError">return value of this function on exception</param>
        /// <returns></returns>
        public T Execute<T>(Func<T> func, Func<Exception, T> onError)
        {
            Thread.Sleep(Delay);
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

                Thread.Sleep(RetryDelay);
                goto Retry;
            }
        }
#endregion
#endif
    }
}