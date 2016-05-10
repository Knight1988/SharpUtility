using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpUtility.Common
{
    public class CodeConfiguration
    {
        protected internal CodeConfiguration() { }

        /// <summary>
        ///     Maximu Retries
        /// </summary>
        protected int MaxRetries { get; set; }

        protected internal List<ExceptionHandlerAction> ExceptionHandlerActions { get; } = new List<ExceptionHandlerAction>();

        /// <summary>
        ///     Try execture the code
        /// </summary>
        /// <param name="action">Action to execute</param>
        /// <returns></returns>
        public static CodeConfigurationAction Try(Action action)
        {
            return Try(action, 0);
        }

        /// <summary>
        ///     Try execture the code
        /// </summary>
        /// <param name="action">Action to execute</param>
        /// <param name="maxRetries">maximm retries</param>
        /// <returns></returns>
        public static CodeConfigurationAction Try(Action action, int maxRetries)
        {
            return new CodeConfigurationAction(action, maxRetries);
        }

        /// <summary>
        ///     Try execture the code
        /// </summary>
        /// <param name="action">Action to execute</param>
        /// <param name="maxRetries">maximm retries</param>
        /// <returns></returns>
        public static CodeConfigurationFunction<TResult> Try<TResult>(Func<TResult> action, int maxRetries)
        {
            return new CodeConfigurationFunction<TResult>(action, maxRetries);
        }

        /// <summary>
        ///     Try execture the code
        /// </summary>
        /// <param name="action">Action to execute</param>
        /// <param name="maxRetries">maximm retries</param>
        /// <param name="delay">delay between retry, in miliseconds</param>
        /// <returns></returns>
        public static CodeConfigurationActionAsync Try(Func<Task> action, int maxRetries, int delay)
        {
            return new CodeConfigurationActionAsync(action, maxRetries, delay);
        }

        /// <summary>
        ///     Try execture the code
        /// </summary>
        /// <param name="action">Action to execute</param>
        /// <param name="maxRetries">maximm retries</param>
        /// <param name="delay">delay between retry, in miliseconds</param>
        /// <returns></returns>
        public static CodeConfigurationFunctionAsync<TResult> Try<TResult>(Func<Task<TResult>> action, int maxRetries, int delay)
        {
            return new CodeConfigurationFunctionAsync<TResult>(action, maxRetries, delay);
        }
    }
}