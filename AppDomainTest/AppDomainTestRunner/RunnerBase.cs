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
        public Dictionary<string, T> Exports { get; private set; }

        public void Initialize()
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
            Initialize(pluginPath);
        }

        public void Initialize(string pluginPath)
        {
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
            Console.WriteLine("{0} exports in AppDomain {1}", Exports.Count, AppDomain.CurrentDomain.FriendlyName);
        }

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
                    OnExportUpdate(ExportUpdateEventType.Delete, null, pair.Value);
                    Exports.Remove(pair.Value.Name);
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
