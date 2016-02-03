using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace SharpUtility
{
    public class FileWatcher : MarshalByRefObject
    {
        public bool EnableRaisingEvents { get; set; }

        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                Files = null;
            }
        }

        public bool IncludeSubdirectories
        {
            get { return _includeSubdirectories; }
            set
            {
                _includeSubdirectories = value;
                Files = null;
            }
        }

        public NotifyFilters NotifyFilter { get; set; }

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                Files = null;
            }
        }

        protected Dictionary<string, string> Files;
        private string _path;
        private string _filter;
        private bool _includeSubdirectories;
        private readonly Timer _timer;
        private readonly Dictionary<WatcherChangeTypes, ManualResetEvent> _manualResetEvents;

        public FileWatcher()
        {
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            _manualResetEvents = new Dictionary<WatcherChangeTypes, ManualResetEvent>()
            {
                {WatcherChangeTypes.All, new ManualResetEvent(true)},
                {WatcherChangeTypes.Changed, new ManualResetEvent(true)},
                {WatcherChangeTypes.Created, new ManualResetEvent(true)},
                {WatcherChangeTypes.Deleted, new ManualResetEvent(true)},
                {WatcherChangeTypes.Renamed, new ManualResetEvent(true)},
            };
            _timer = new Timer(250);
            _timer.Elapsed += TimerOnElapsed;
            _timer.Start();
        }

        public FileWatcher(string path) : this()
        {
            Path = path;
        }

        ~FileWatcher()
        {
            _timer.Dispose();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs args)
        {
            if (!Directory.Exists(Path)) return;

            var files = Directory.GetFiles(Path, Filter, IncludeSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            var fileInfos = files.AsParallel().Select(GetFileInfo).ToDictionary(p => p.Key, q => q.Value);

            if (Files == null)
            {
                Files = fileInfos;
                return;
            }

            foreach (var fileInfo in fileInfos)
            {
                var containKey = Files.ContainsKey(fileInfo.Key);
                var containValue = Files.ContainsValue(fileInfo.Value);

                if (!containKey && !containValue)
                {
                    // new file created
                    var dir = System.IO.Path.GetDirectoryName(fileInfo.Value);
                    // ReSharper disable once AssignNullToNotNullAttribute
                    if (EnableRaisingEvents) OnCreated(new FileSystemEventArgs(WatcherChangeTypes.Created, dir, fileInfo.Value));

                    _manualResetEvents[WatcherChangeTypes.All].Set();
                    _manualResetEvents[WatcherChangeTypes.Created].Set();
                }
                else if (!containKey)
                {
                    // File renamed
                    var dir = System.IO.Path.GetDirectoryName(fileInfo.Value);
                    // ReSharper disable once AssignNullToNotNullAttribute
                    if (EnableRaisingEvents) OnRenamed(new RenamedEventArgs(WatcherChangeTypes.Renamed, dir, fileInfo.Value, Files[fileInfo.Key]));

                    _manualResetEvents[WatcherChangeTypes.All].Set();
                    _manualResetEvents[WatcherChangeTypes.Renamed].Set();
                }
                else if (!containValue)
                {
                    // File changed
                    var dir = System.IO.Path.GetDirectoryName(fileInfo.Value);
                    // ReSharper disable once AssignNullToNotNullAttribute
                    if (EnableRaisingEvents) OnChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed, dir, fileInfo.Value));

                    _manualResetEvents[WatcherChangeTypes.All].Set();
                    _manualResetEvents[WatcherChangeTypes.Changed].Set();
                }
            }

            foreach (var file in Files)
            {
                if (!fileInfos.ContainsKey(file.Key))
                {
                    // File deleted
                    var dir = System.IO.Path.GetDirectoryName(file.Value);
                    // ReSharper disable once AssignNullToNotNullAttribute
                    if (EnableRaisingEvents) OnDeleted(new FileSystemEventArgs(WatcherChangeTypes.Changed, dir, file.Value));

                    _manualResetEvents[WatcherChangeTypes.All].Set();
                    _manualResetEvents[WatcherChangeTypes.Deleted].Set();
                }
            }
        }

        private KeyValuePair<string, string> GetFileInfo(string fileName)
        {
            var file = new FileInfo(fileName);
            var fileInfo = new List<string>();
            if (NotifyFilter.HasFlag(NotifyFilters.FileName))
            {
                fileInfo.Add(file.Name);
            }

            if (NotifyFilter.HasFlag(NotifyFilters.Attributes))
            {
                fileInfo.Add(file.Attributes.ToString());
            }

            if (NotifyFilter.HasFlag(NotifyFilters.CreationTime))
            {
                fileInfo.Add(file.CreationTime.ToString(CultureInfo.InvariantCulture));
            }

            if (NotifyFilter.HasFlag(NotifyFilters.DirectoryName))
            {
                fileInfo.Add(file.DirectoryName);
            }

            if (NotifyFilter.HasFlag(NotifyFilters.LastAccess))
            {
                fileInfo.Add(file.LastAccessTime.ToString(CultureInfo.InvariantCulture));
            }

            if (NotifyFilter.HasFlag(NotifyFilters.LastWrite))
            {
                fileInfo.Add(file.LastWriteTime.ToString(CultureInfo.InvariantCulture));
            }

            if (NotifyFilter.HasFlag(NotifyFilters.Size))
            {
                fileInfo.Add(file.Length.ToString());
            }

            return new KeyValuePair<string, string>(fileName, string.Join("|", fileInfo));
        }

        public void WaitForChanged(WatcherChangeTypes changeType)
        {
            _manualResetEvents[changeType].Reset();
            _manualResetEvents[changeType].WaitOne();
        }

        public void WaitForChanged(WatcherChangeTypes changeType, int timeout)
        {
            _manualResetEvents[changeType].Reset();
            _manualResetEvents[changeType].WaitOne(timeout);
        }

        public event EventHandler<FileSystemEventArgs> Changed;
        public event EventHandler<FileSystemEventArgs> Created;
        public event EventHandler<FileSystemEventArgs> Deleted;
        public event EventHandler<RenamedEventArgs> Renamed;

        protected virtual void OnChanged(FileSystemEventArgs e)
        {
            var handler = Changed;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnCreated(FileSystemEventArgs e)
        {
            var handler = Created;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnDeleted(FileSystemEventArgs e)
        {
            var handler = Deleted;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnRenamed(RenamedEventArgs e)
        {
            var handler = Renamed;
            if (handler != null) handler(this, e);
        }
    }
}
