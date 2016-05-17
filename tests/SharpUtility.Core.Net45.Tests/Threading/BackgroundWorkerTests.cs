﻿using System.Threading.Tasks;
using NUnit.Framework;
using SharpUtility.Threading;

namespace SharpUtility.Core.Tests.Threading
{
    [TestFixture]
    internal class BackgroundWorkerTests
    {
        [Test]
        public async Task BasicWorkerTest()
        {
            var worker = new BackgroundWorker();
            var j = 0;

            worker.DoWork += (sender, args) =>
            {
                for (var i = 0; i < 10; i++)
                {
                    worker.ReportProgress(i, 9);
                }
            };
            worker.ProgressChanged += (sender, args) => { j++; };

            await worker.RunWorkerAsync();

            Assert.AreEqual(10, j);
        }
    }
}