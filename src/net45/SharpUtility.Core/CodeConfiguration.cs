﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpUtility.Core
{
    public class CodeConfiguration
    {
        protected TimeSpan Delay { get; set; }
        protected int MaxRetries { get; set; }
        protected TimeSpan RetryDelay { get; set; }

        public CodeConfiguration()
        {
            RetryIfFail(0);
            RunAfter(0);
            DelayBetweenEachRetry(0);
        }

        public CodeConfiguration DelayBetweenEachRetry(int millisecondsTimeout)
        {
            return DelayBetweenEachRetry(new TimeSpan(0, 0, 0, millisecondsTimeout));
        }

        public CodeConfiguration DelayBetweenEachRetry(TimeSpan timeout)
        {
            RetryDelay = timeout;
            return this;
        }

        public Task<T> ExecuteAsync<T>(Func<Task<T>> func, Func<Exception, Task<T>> onError)
        {
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

        public Task ExecuteAsync(Action action)
        {
            return ExecuteAsync(action, null);
        }

        public Task ExecuteAsync(Action action, Action<Exception> onError)
        {
            return Task.Factory.StartNew(() =>
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
            });
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