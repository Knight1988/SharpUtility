using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpUtility.Core
{
    public static class Code
    {
        /// <summary>
        ///     Execute function
        /// </summary>
        /// <param name="action">function</param>
        /// <param name="maxRetries">how many fail until throw exception</param>
        /// <param name="delay">deplay between retry</param>
        /// <param name="onError">action on error</param>
        public static void ExecuteRetry(Action action, int maxRetries = 3, int delay = 1000,
            Action<Exception> onError = null)
        {
            if (maxRetries < 0)
            {
                maxRetries = 2147483647;
            }
            var num = 0;

            Retry:
            try
            {
                action();
            }
            catch (Exception e)
            {
                num++;
                if (num >= maxRetries)
                {
                    if (onError == null) throw;
                    onError(e);
                    return;
                }

                Thread.Sleep(delay);
                goto Retry;
            }
        }

        /// <summary>
        ///     Execute Async function
        /// </summary>
        /// <param name="func">function</param>
        /// <param name="maxRetries">how many fail until throw exception</param>
        /// <param name="delay">deplay between retry</param>
        /// <param name="onError">action on error</param>
        public static async Task ExecuteRetryAsync(Func<Task> func, int maxRetries = 3, int delay = 1000,
            Action<Exception> onError = null)
        {
            if (maxRetries < 0)
            {
                maxRetries = 2147483647;
            }
            var num = 0;

            Retry:
            try
            {
                await func();
            }
            catch (Exception e)
            {
                num++;
                if (num >= maxRetries)
                {
                    if (onError == null) throw;
                    onError(e);
                    return;
                }

                Thread.Sleep(delay);
                goto Retry;
            }
        }

        /// <summary>
        ///     Execute function
        /// </summary>
        /// <typeparam name="T">Type return</typeparam>
        /// <param name="func">function</param>
        /// <param name="maxRetries">how many fail until throw exception</param>
        /// <param name="delay">deplay between retry</param>
        /// <param name="onError">action on error</param>
        /// <returns>return action value</returns>
        public static T ExecuteRetry<T>(Func<T> func, int maxRetries = 3, int delay = 1000,
            Func<Exception, T> onError = null)
        {
            if (maxRetries < 0)
            {
                maxRetries = 2147483647;
            }
            var num = 0;

            Retry:
            try
            {
                return func();
            }
            catch (Exception e)
            {
                num++;
                if (num >= maxRetries)
                {
                    if (onError == null) throw;
                    return onError(e);
                }

                Thread.Sleep(delay);
                goto Retry;
            }
        }

        /// <summary>
        ///     Execute async function
        /// </summary>
        /// <typeparam name="T">Type return</typeparam>
        /// <param name="func">function</param>
        /// <param name="maxRetries">how many fail until throw exception</param>
        /// <param name="delay">deplay between retry</param>
        /// <param name="onError">action on error</param>
        /// <returns>return action value</returns>
        public static Task<T> ExecuteRetryAsync<T>(Func<Task<T>> func, int maxRetries = 3, int delay = 1000,
            Func<Exception, Task<T>> onError = null)
        {
            if (maxRetries < 0)
            {
                maxRetries = 2147483647;
            }
            var num = 0;

            Retry:
            try
            {
                return func();
            }
            catch (Exception e)
            {
                num++;
                if (num >= maxRetries)
                {
                    if (onError == null) throw;
                    return onError(e);
                }

                Thread.Sleep(delay);
                goto Retry;
            }
        }
    }
}