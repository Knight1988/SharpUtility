using System;

namespace SharpUtility.Common
{
    public class CodeConfigurationFunction<TResult> : CodeConfiguration
    {
        private Func<TResult> Action { get; }

        public CodeConfigurationFunction(Func<TResult> action, int maxRetries)
        {
            Action = action;
            MaxRetries = maxRetries;
        }

        /// <summary>
        ///     Catch the exception
        /// </summary>
        /// <param name="exceptionHandlerAction"></param>
        /// <param name="maxRetries"></param>
        /// <returns></returns>
        public CodeConfigurationFunction<TResult> Catch<TException>(Func<TException, TResult> exceptionHandlerAction, int maxRetries)
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

        public TResult Execute()
        {
            Retry:
            try
            {
                return Action();
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

                    return exceptionHandlerAction.Action(e);
                }

                throw;
            }
        }
    }
}