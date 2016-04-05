using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SharpUtility.Threading.Tests
{
    [TestFixture]
    public class BackgroundWorkerTests
    {
        int i;

        public void DoWork()
        {
            for (i = 0; i < 10; i++) { }
        }

        public async Task DoWorkAsync()
        {
            await Task.Delay(1000);
            for (i = 0; i < 10; i++) { }
        }

        [Test]
        public async Task Test()
        {
            /* Arrange */
            var worker = BackgroundWorker.Create();

            /* Act */
            await worker.RunWorkerAsync(() => DoWork());

            /* Assert */
            Assert.AreEqual(10, i);
        }

        [Test]
        public async Task AsyncTest()
        {
            /* Arrange */
            var worker = BackgroundWorker.Create();

            /* Act */
            await worker.RunWorkerAsync(DoWorkAsync);

            /* Assert */
            Assert.AreEqual(10, i);
        }

        [Test]
        public void CancelTest()
        {
            var tokenSource = new CancellationTokenSource();
            /* Arrange */
            var worker = BackgroundWorker.Create();

            /* Act */
            var task = worker.RunWorkerAsync(DoWorkAsync, tokenSource.Token);
            tokenSource.Cancel();

            /* Assert */
            // ReSharper disable once MethodSupportsCancellation
            Assert.That(() => task.Wait(), Throws.TypeOf<AggregateException>().And.InnerException.AssignableFrom<TaskCanceledException>());
        }

        [Test]
        public async Task PclTest()
        {
            /* Arrange */

            /* Act */
            await CounterWorker.RunWorkerAsync(DoWorkAsync);

            /* Assert */
            Assert.AreEqual(10, i);
        }
    }
}
