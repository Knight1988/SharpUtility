using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpUtility.Core.Threading;

namespace SharpUtility.Core.Tests.Threading
{
    [TestFixture]
    public class AsyncLazyUnitTests
    {
        [Test]
        public void AsyncLazy_NeverAwaited_DoesNotCallFunc()
        {
            Func<int> func = () =>
            {
                Assert.Fail();
                return 13;
            };

            var lazy = new AsyncLazy<int>(func);
        }

        [Test]
        public void AsyncLazy_CallsFuncOnThreadPool()
        {
            var testThread = Thread.CurrentThread.ManagedThreadId;
            var funcThread = testThread;
            Func<int> func = () =>
            {
                funcThread = Thread.CurrentThread.ManagedThreadId;
                return 13;
            };
            var lazy = new AsyncLazy<int>(func);

            Task.Run(async () => await lazy).Wait();

            Assert.AreNotEqual(testThread, funcThread);
        }

        [Test]
        public void AsyncLazy_CallsAsyncFuncOnThreadPool()
        {
            var testThread = Thread.CurrentThread.ManagedThreadId;
            var funcThread = testThread;
            Func<Task<int>> func = async () =>
            {
                funcThread = Thread.CurrentThread.ManagedThreadId;
                await Task.Yield();
                return 13;
            };
            var lazy = new AsyncLazy<int>(func);

            Task.Run(async () => await lazy).Wait();

            Assert.AreNotEqual(testThread, funcThread);
        }

        [Test]
        public async Task AsyncLazy_Start_CallsFunc()
        {
            var tcs = new TaskCompletionSource<bool>();
            Func<int> func = () =>
            {
                tcs.SetResult(true);
                return 13;
            };
            var lazy = new AsyncLazy<int>(func);

            lazy.Start();
            await tcs.Task;
        }

        [Test]
        public async Task AsyncLazy_Await_ReturnsFuncValue()
        {
            Func<int> func = () => 13;
            var lazy = new AsyncLazy<int>(func);

            var result = await lazy;
            Assert.AreEqual(13, result);
        }

        [Test]
        public async Task AsyncLazy_Await_ReturnsAsyncFuncValue()
        {
            Func<Task<int>> func = async () =>
            {
                await Task.Yield();
                return 13;
            };
            var lazy = new AsyncLazy<int>(func);

            var result = await lazy;
            Assert.AreEqual(13, result);
        }

        [Test]
        public async Task AsyncLazy_MultipleAwaiters_OnlyInvokeFuncOnce()
        {
            int invokeCount = 0;
            var mre = new ManualResetEvent(false);
            Func<int> func = () =>
            {
                Interlocked.Increment(ref invokeCount);
                mre.WaitOne();
                return 13;
            };
            var lazy = new AsyncLazy<int>(func);

            var task1 = Task.Factory.StartNew(async () => await lazy).Result;
            var task2 = Task.Factory.StartNew(async () => await lazy).Result;

            Assert.IsFalse(task1.IsCompleted);
            Assert.IsFalse(task2.IsCompleted);
            mre.Set();
            var results = await Task.WhenAll(task1, task2);
            Assert.IsTrue(results.SequenceEqual(new[] { 13, 13 }));
            Assert.AreEqual(1, invokeCount);
        }

        [Test]
        public async Task AsyncLazy_MultipleAwaiters_OnlyInvokeAsyncFuncOnce()
        {
            int invokeCount = 0;
            var tcs = new TaskCompletionSource<bool>();
            Func<Task<int>> func = async () =>
            {
                Interlocked.Increment(ref invokeCount);
                await tcs.Task;
                return 13;
            };
            var lazy = new AsyncLazy<int>(func);

            var task1 = Task.Factory.StartNew(async () => await lazy).Result;
            var task2 = Task.Factory.StartNew(async () => await lazy).Result;

            Assert.IsFalse(task1.IsCompleted);
            Assert.IsFalse(task2.IsCompleted);
            tcs.SetResult(true);
            var results = await Task.WhenAll(task1, task2);
            Assert.IsTrue(results.SequenceEqual(new[] { 13, 13 }));
            Assert.AreEqual(1, invokeCount);
        }
    }
}
