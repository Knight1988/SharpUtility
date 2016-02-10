using System;
using System.Threading;
using System.Threading.Tasks;

namespace SharpUtility.Core.Threading
{
    public static class TaskManager
    {
        public static void Forget(this Task task)
        {
            
        }

#if !PCL
        /// <summary>
        /// Start a STA Task
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Task<T> StartSTATask<T>(Func<T> func)
        {
            var taskCompletionSource = new TaskCompletionSource<T>();
            var thread = new Thread(() =>
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

        /// <summary>
        /// Start a STA Task
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Task StartSTATask<T>(Action action)
        {
            Task task = null;
            var thread = new Thread(() =>
            {
                task = Task.Run(action);
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return task;
        }
#endif
    }
}
