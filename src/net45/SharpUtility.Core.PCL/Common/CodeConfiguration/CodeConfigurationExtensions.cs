using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpUtility.Common
{
    public static class CodeConfigurationExtensions
    {
        public static CodeConfigurationAction Try(this Action action, int maxRetries)
        {
            return CodeConfiguration.Try(action, maxRetries);
        }

        public static CodeConfigurationActionAsync Try(this Func<Task> action, int maxRetries, int delay)
        {
            return CodeConfiguration.Try(action, maxRetries, delay);
        }

        public static CodeConfigurationFunction<TResult> Try<TResult>(this Func<TResult> action, int maxRetries)
        {
            return CodeConfiguration.Try(action, maxRetries);
        }

        public static CodeConfigurationFunctionAsync<TResult> Try<TResult>(this Func<Task<TResult>> action, int maxRetries, int delay)
        {
            return CodeConfiguration.Try(action, maxRetries, delay);
        }
    }
}
