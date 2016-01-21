using System;
using System.IO;
using NUnit.Framework;
using SharpUtility.Core;

namespace SharpUtility.Tests
{
    [TestFixture]
    public class FileWatcherTests
    {
        [SetUp]
        public void SetUp()
        {
            File.Delete(_testFile);
        }

        private readonly string _testFile = Path.Combine(Environment.CurrentDirectory, "Test.txt");
        private readonly string _testFile2 = Path.Combine(Environment.CurrentDirectory, "Test2.txt");

        [Test]
        public void FileWatcherTest()
        {
            var watcher = new FileWatcher(Environment.CurrentDirectory);

            // Test created
            CodeManager.CreateCodeConfiguration()
                .RunAfter(1000)
                .ExecuteAsync(() => File.WriteAllText(_testFile, @"Test"));
            watcher.WaitForChanged(WatcherChangeTypes.Created);

            // Test changed
            CodeManager.CreateCodeConfiguration()
                .RunAfter(1000)
                .ExecuteAsync(() => File.WriteAllText(_testFile, @"Test 2"));
            watcher.WaitForChanged(WatcherChangeTypes.Changed);

            // Test Rename
            CodeManager.CreateCodeConfiguration()
                .RunAfter(1000)
                .ExecuteAsync(() => File.Move(_testFile, _testFile2));
            watcher.WaitForChanged(WatcherChangeTypes.Renamed);

            // Test Rename
            CodeManager.CreateCodeConfiguration()
                .RunAfter(1000)
                .ExecuteAsync(() => File.Delete(_testFile2));
            watcher.WaitForChanged(WatcherChangeTypes.Deleted);
        }
    }
}