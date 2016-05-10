using System;
using System.Threading.Tasks;

namespace SharpUtility.Common
{
    public class CodeConfigurationActionAsync : CodeConfiguration
    {
        private Func<Task> Action { get; }

        /// <summary>
        ///     Delay between each retries (only work in async)
        /// </summary>
        protected int RetryDelay { get; set; }

        public CodeConfigurationActionAsync(Func<Task> action, int maxRetries, int delay)
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
        public CodeConfigurationActionAsync Catch<TException>(Func<TException, Task> exceptionHandlerAction, int maxRetries)
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

        public async Task ExecuteAsync()
        {
            Retry:
            try
            {
                await Action();
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

                    await exceptionHandlerAction.Action(e);
                    return;
                }

                throw;
            }
        }
    }
}