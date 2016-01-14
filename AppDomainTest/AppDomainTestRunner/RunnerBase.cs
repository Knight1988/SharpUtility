using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Linq;

namespace AppDomainTestRunner
{
    public class RunnerBase<T> : MarshalByRefObject where T : ExporterBase
    {
        private CompositionContainer _container;
        private DirectoryCatalog _directoryCatalog;
        private FileSystemWatcher _watcher;
        private bool _autoRecompose;
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

        public void Initialize()
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
            Initialize(pluginPath);
        }

        private void AddFileWatcher()
        {
            if (_watcher != null || !Directory.Exists(PluginPath)) return;

            _watcher = new FileSystemWatcher(PluginPath)
            {
                EnableRaisingEvents = true,
                NotifyFilter =
                    NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName |
                    NotifyFilters.DirectoryName
            };
            _watcher.Changed += WatcherOnChanged;
            _watcher.Deleted += WatcherOnChanged;
            _watcher.Created += WatcherOnChanged;
            _watcher.Renamed += WatcherOnChanged;
        }

        private void RemoveFileWatcher()
        {
            _watcher.Dispose();
            _watcher = null;
        }

        private void WatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            Recompose();
        }

        public void Initialize(string pluginPath)
        {
            PluginPath = pluginPath;
            AutoRecompose = AutoRecompose;

            // Use RegistrationBuilder to set up our MEF parts.
            var regBuilder = new RegistrationBuilder();
            regBuilder.ForTypesDerivedFrom<T>().Export<T>();

            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(RunnerBase<T>).Assembly, regBuilder));
            _directoryCatalog = new DirectoryCatalog(pluginPath, regBuilder);
            catalog.Catalogs.Add(_directoryCatalog);

            _container = new CompositionContainer(catalog);
            _container.ComposeExportedValue(_container);

            // Get our exports available to the rest of Program.
            Exports = _container.GetExportedValues<T>().ToDictionary(p => p.Name, p => p);
            
        }

        public string PluginPath { get; private set; }

        public void Recompose()
        {
            // Gimme 3 steps...
            _directoryCatalog.Refresh();
            _container.ComposeParts(_directoryCatalog.Parts);
            var exports = _container.GetExportedValues<T>().ToDictionary(p => p.Name, p => p);

            // get insert, updated
            foreach (var pair in exports)
            {
                if (!Exports.ContainsKey(pair.Value.Name))
                {
                    // get inserted
                    Exports.Add(pair.Key, pair.Value);
                    OnExportUpdate(ExportUpdateEventType.Insert, pair.Value, null);
                }
                else
                {
                    // skip if same version
                    if (Exports[pair.Value.Name].Version == pair.Value.Version) continue;

                    // update new version
                    Exports[pair.Value.Name] = pair.Value;
                    OnExportUpdate(ExportUpdateEventType.Update, pair.Value, Exports[pair.Value.Name]);
                }
            }

            // get deleted
            foreach (var pair in Exports.ToDictionary(p => p.Key, p => p.Value))
            {
                if (!exports.ContainsKey(pair.Value.Name))
                {
                    // get deleted
                    Exports.Remove(pair.Value.Name);
                    OnExportUpdate(ExportUpdateEventType.Delete, null, pair.Value);
                }
            }
        }

        public event EventHandler<ExportUpdateEventArgs<T>> ExportUpdate;

        protected virtual void OnExportUpdate(ExportUpdateEventArgs<T> e)
        {
            var handler = ExportUpdate;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnExportUpdate(ExportUpdateEventType eventType, T inserted, T deleted)
        {
            OnExportUpdate(new ExportUpdateEventArgs<T>(eventType, inserted, deleted));
        }
    }

    public class ExportUpdateEventArgs<T> : EventArgs where T : ExporterBase
    {
        public ExportUpdateEventArgs(ExportUpdateEventType eventType, T inserted, T deleted)
        {
            EventType = eventType;
            Inserted = inserted;
            Deleted = deleted;
        }

        public ExportUpdateEventType EventType { get; set; }
        public T Inserted { get; set; }
        public T Deleted { get; set; }
    }

    public enum ExportUpdateEventType
    {
        Insert,
        Update,
        Delete
    }
}
