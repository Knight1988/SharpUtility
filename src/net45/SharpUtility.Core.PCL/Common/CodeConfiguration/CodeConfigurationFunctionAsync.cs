using System;
using System.Threading.Tasks;

namespace SharpUtility.Common
{
    public class CodeConfigurationFunctionAsync<TResult> : CodeConfiguration
    {
        private Func<Task<TResult>> Action { get; }

        /// <summary>
        ///     Delay between each retries (only work in async)
        /// </summary>
        protected int RetryDelay { get; set; }

        public CodeConfigurationFunctionAsync(Func<Task<TResult>> action, int maxRetries, int delay)
        {
            Action = action;
            MaxRetries = maxRetries;
            RetryDelay = delay;
        }

        /// <summary>
        ///     Catch the exception
        /// </summary>
        /// <param name="exceptionHandlerAction"></param>
        /// <param name="maxRetries"></param>
        /// <returns></returns>
        public CodeConfigurationFunctionAsync<TResult> Catch<TException>(Func<TException, Task<TResult>> exceptionHandlerAction, int maxRetries)
            where TException : Exception
        {
            ExceptionHandlerActions.Add(new ExceptionHandlerAction
            {
                ExceptionType = typeof(TException),
                Action = exceptionHandlerAction,
                MaxRetries = maxRetries
            });
            return this;
        }

        public async Task<TResult> ExecuteAsync()
        {
            Retry:
            try
            {
                return await Action();
            }
            catch (Exception e)
            {
                foreach (var exceptionHandlerAction in ExceptionHandlerActions)
                {
                    if (e.GetType() != exceptionHandlerAction.ExceptionType) continue;

                    if (exceptionHandlerAction.Retry < exceptionHandlerAction.MaxRetries)
                    {
                        exceptionHandlerAction.Retry++;
                        goto Retry;
                    }

                    if (exceptionHandlerAction.Action == null) throw;

                    return await exceptionHandlerAction.Action(e);
                }

                throw;
            }
        }
    }
}