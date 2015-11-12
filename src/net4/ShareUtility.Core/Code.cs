using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpUtility.Core
{
    public static class Code
    {
        #region Execute Action

        /// <summary>
        ///     Execute function
        /// </summary>
        /// <param name="action">function</param>
        /// <param name="maxRetries">how many fail until throw exception</param>
        /// <param name="delay">deplay between retry</param>
        /// <param name="onError">action on error</param>
        public static void ExecuteRetry(Action action, int maxRetries, int delay, Action<Exception> onError)
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
        ///     Execute function
        /// </summary>
        /// <param name="action">function</param>
        public static void ExecuteRetry(Action action)
        {
            ExecuteRetry(action, 3, 1000, null);
        }

        /// <summary>
        ///     Execute function
        /// </summary>
        /// <param name="action">function</param>
        /// <param name="maxRetries">how many fail until throw exception</param>
        public static void ExecuteRetry(Action action, int maxRetries)
        {
            ExecuteRetry(action, maxRetries, 1000, null);
        }

        /// <summary>
        ///     Execute function
        /// </summary>
        /// <param name="action">function</param>
        /// <param name="maxRetries">how many fail until throw exception</param>
        /// <param name="delay">deplay between retry</param>
        public static void ExecuteRetry(Action action, int maxRetries, int delay)
        {
            ExecuteRetry(action, maxRetries, delay, null);
        }

        /// <summary>
        ///     Execute function
        /// </summary>
        /// <param name="action">function</param>
        /// <param name="maxRetries">how many fail until throw exception</param>
        /// <param name="onError">action on error</param>
        public static void ExecuteRetry(Action action, int maxRetries, Action<Exception> onError)
        {
            ExecuteRetry(action, maxRetries, 1000, onError);
        }

        /// <summary>
        ///     Execute function
        /// </summary>
        /// <param name="action">function</param>
        /// <param name="onError">action on error</param>
        public static void ExecuteRetry(Action action, Action<Exception> onError)
        {
            ExecuteRetry(action, 3, 1000, onError);
        }

        #endregion

        #region Execute function

        /// <summary>
        ///     Execute function
        /// </summary>
        /// <typeparam name="T">Type return</typeparam>
        /// <param name="func">function</param>
        /// <param name="maxRetries">how many fail until throw exception</param>
        /// <param name="delay">deplay between retry</param>
        /// <param name="onError">action on error</param>
        /// <returns>return action value</returns>
        public static T ExecuteRetry<T>(Func<T> func, int maxRetries, int delay, Func<Exception, T> onError)
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
        ///     Execute function
        /// </summary>
        /// <typeparam name="T">Type return</typeparam>
        /// <param name="func">function</param>
        /// <returns>return action value</returns>
        public static T ExecuteRetry<T>(Func<T> func)
        {
            return ExecuteRetry(func, 3, 1000, null);
        }

        /// <summary>
        ///     Execute function
        /// </summary>
        /// <typeparam name="T">Type return</typeparam>
        /// <param name="func">function</param>
        /// <param name="maxRetries">how many fail until throw exception</param>
        /// <returns>return action value</returns>
        public static T ExecuteRetry<T>(Func<T> func, int maxRetries)
        {
            return ExecuteRetry(func, maxRetries, 1000, null);
        }

        /// <summary>
        ///     Execute function
        /// </summary>
        /// <typeparam name="T">Type return</typeparam>
        /// <param name="func">function</param>
        /// <param name="maxRetries">how many fail until throw exception</param>
        /// <param name="delay">deplay between retry</param>
        /// <returns>return action value</returns>
        public static T ExecuteRetry<T>(Func<T> func, int maxRetries, int delay)
        {
            return ExecuteRetry(func, maxRetries, delay, null);
        }

        /// <summary>
        ///     Execute function
        /// </summary>
        /// <typeparam name="T">Type return</typeparam>
        /// <param name="func">function</param>
        /// <param name="maxRetries">how many fail until throw exception</param>
        /// <param name="onError">action on error</param>
        /// <returns>return action value</returns>
        public static T ExecuteRetry<T>(Func<T> func, int maxRetries, Func<Exception, T> onError)
        {
            return ExecuteRetry(func, maxRetries, 1000, onError);
        }

        /// <summary>
        ///     Execute function
        /// </summary>
        /// <typeparam name="T">Type return</typeparam>
        /// <param name="func">function</param>
        /// <param name="onError">action on error</param>
        /// <returns>return action value</returns>
        public static T ExecuteRetry<T>(Func<T> func, Func<Exception, T> onError)
        {
            return ExecuteRetry(func, 3, 1000, onError);
        }

        #endregion
    }
}