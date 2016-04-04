using System;
using System.Threading.Tasks;

namespace SharpUtility.Threading
{
    public class CounterWorker
    {
        public static async Task RunWorkerAsync(Func<Task> doWorkAsync)
        {
            var worker = new BackgroundWorker();
            await worker.RunWorkerAsync(doWorkAsync);
        }
    }
}
