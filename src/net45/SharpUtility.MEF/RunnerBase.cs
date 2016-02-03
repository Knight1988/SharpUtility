using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Linq;
using System.Threading;

namespace SharpUtility.MEF
{
    public class RunnerBase<T> : MarshalByRefObject where T : IExporterBase
    {
        private bool _autoRecompose;
        private CompositionContainer _container;
        private DirectoryCatalog _directoryCatalog;
        private FileSystemWatcher _watcher;
        public Dictionary<string, T> Exports { get; private set; }

        // watch the plugin folder and raise event if file changed
        public bool AutoRecompose
        {
            get { return _autoRecompose; }
            set
            {
                _autoRecompose = value;
                if (value) AddFileWatcher();
                else RemoveFileWatcher();
            }
        }

        public string PluginPath { get; private set; }

        private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(false);

        public void Initialize()
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
            Initialize(pluginPath);
        }

        public void WaitForUpdate()
        {
            _autoResetEvent.WaitOne();
        }

        public void Initialize(string pluginPath)
        {
            PluginPath = pluginPath;
            AutoRecompose = AutoRecompose;

            // Use RegistrationBuilder to set up our MEF parts.
            var regBuilder = new RegistrationBuilder();
            regBuilder.ForTypesDerivedFrom<T>().Export<T>();

            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof (RunnerBase<T>).Assembly, regBuilder));
            _directoryCatalog = new DirectoryCatalog(pluginPath, regBuilder);
            catalog.Catalogs.Add(_directoryCatalog);

            _container = new CompositionContainer(catalog);
            _container.ComposeExportedValue(_container);

            // Get our exports available to the rest of Program.
            Exports = GetExportedValues();
        }

        private Dictionary<string, T> GetExportedValues()
        {
            var values = _container.GetExportedValues<T>();

            return values.ToDictionary(p =>
            {
                if (string.IsNullOrWhiteSpace(p.Name)) p.Name = p.GetType().FullName;
                return p.Name;
            }, p => p);
        }

        private void PrivateRecompose()
        {
            // Gimme 3 steps...
            _directoryCatalog.Refresh();
            _container.ComposeParts(_directoryCatalog.Parts);
            var exports = GetExportedValues();

            // get insert, updated
            foreach (var pair in exports)
            {
                if (!Exports.ContainsKey(pair.Value.Name))
                {
                    // get inserted
                    Exports.Add(pair.Key, pair.Value);
                    OnExportUpdate(ExportUpdateEventType.Insert, pair.Value, default(T));
                }
            }

            // get deleted
            foreach (var pair in Exports.ToDictionary(p => p.Key, p => p.Value))
            {
                if (!exports.ContainsKey(pair.Value.Name))
                {
                    // get deleted
                    Exports.Remove(pair.Value.Name);
                    OnExportUpdate(ExportUpdateEventType.Delete, default(T), pair.Value);
                }
            }
        }

        public void Recompose()
        {
            if (AutoRecompose) throw new Exception("Cannot manual call Recompose while AutoRecompose is on.");

            PrivateRecompose();
        }

        protected virtual void OnExportUpdate(ExportUpdateEventArgs<T> e)
        {
            _autoResetEvent.Set();
            var handler = ExportUpdate;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnExportUpdate(ExportUpdateEventType eventType, T inserted, T deleted)
        {
            OnExportUpdate(new ExportUpdateEventArgs<T>(eventType, inserted, deleted));
        }

        private void AddFileWatcher()
        {
            if (_watcher != null || !Directory.Exists(PluginPath)) return;

            _watcher = new FileSystemWatcher(PluginPath)
            {
                EnableRaisingEvents = true
            };
            _watcher.Deleted += WatcherOnChanged;
            _watcher.Created += WatcherOnChanged;
        }

        private void RemoveFileWatcher()
        {
            if (_watcher == null) return;
            _watcher.Dispose();
            _watcher = null;
        }

        private void WatcherOnChanged(object sender, FileSystemEventArgs args)
        {
            PrivateRecompose();
        }

        public event EventHandler<ExportUpdateEventArgs<T>> ExportUpdate;
    }

    public class ExportUpdateEventArgs<T> : EventArgs where T : IExporterBase
    {
        public ExportUpdateEventType EventType { get; set; }
        public T Inserted { get; set; }
        public T Deleted { get; set; }

        public ExportUpdateEventArgs(ExportUpdateEventType eventType, T inserted, T deleted)
        {
            EventType = eventType;
            Inserted = inserted;
            Deleted = deleted;
        }
    }

    public enum ExportUpdateEventType
    {
        Insert,
        Update,
        Delete
    }
}