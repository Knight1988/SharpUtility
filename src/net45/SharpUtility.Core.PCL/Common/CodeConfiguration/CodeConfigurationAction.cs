using System;

namespace SharpUtility.Common
{
    public class CodeConfigurationAction : CodeConfiguration
    {
        private Action Action { get; }

        public CodeConfigurationAction(Action action, int maxRetries)
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
        public CodeConfigurationAction Catch<TException>(Action<TException> exceptionHandlerAction, int maxRetries)
            where TException : Exception
        {
            ExceptionHandlerActions.Add(new ExceptionHandlerAction
            {
                ExceptionType = typeof (TException),
                Action = exceptionHandlerAction,
                MaxRetries = maxRetries
            });
            return this;
        }

        public void Execute()
        {
            Retry:
            try
            {
                Action();
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

                    exceptionHandlerAction.Action(e);
                }
            }
        }
    }
}