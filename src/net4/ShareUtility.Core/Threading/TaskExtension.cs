using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpUtility.Core.Threading
{
    public static class TaskExtension
    {
        public static void Forget(this Task task)
        {
            
        }

        /// <summary>
        /// Start a STA Task
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Task<T> StartSTATask<T>(Func<T> func)
        {
            TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
            Thread thread = new Thread(() =>
            {
                try
                {
                    taskCompletionSource.SetResult(func());
                }
                catch (Exception exception)
                {
                    taskCompletionSource.SetException(exception);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return taskCompletionSource.Task;
        }
    }
}
