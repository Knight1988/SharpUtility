using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using SharpUtility.MEF.Annotations;

namespace SharpUtility.MEF
{
    [Serializable]
    public class RunnerBase<T> : INotifyPropertyChanged where T : IExporterBase
    {
        private CompositionContainer _container;
        private List<DirectoryCatalog> _directoryCatalogs = new List<DirectoryCatalog>();
        private string _pluginPath;
        private IEnumerable<string> _searchPatterns;
        public Dictionary<string, T> Exports { get; private set; }

        public string PluginPath
        {
            get { return _pluginPath; }
            set
            {
                _pluginPath = value;
                OnPropertyChanged();
            }
        }

        public string SearchPatterns
        {
            get { return string.Join(",", _searchPatterns); }
            set
            {
                _searchPatterns = value.Split(',').Select(p => p.Trim());
                OnPropertyChanged();
            }
        }

        public RunnerBase()
        {
            PluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
            SearchPatterns = "*.dll";
            Initialize();
            PropertyChanged += (sender, args) => Initialize();
        }

        private void Initialize()
        {
            // Use RegistrationBuilder to set up our MEF parts.
            var regBuilder = new RegistrationBuilder();
            regBuilder.ForTypesDerivedFrom<T>().Export<T>();

            var catalog = new AggregateCatalog();
            _directoryCatalogs = new List<DirectoryCatalog>();
            foreach (var searchPattern in _searchPatterns)
            {
                var directoryCatalog = new DirectoryCatalog(PluginPath, searchPattern, regBuilder);
                catalog.Catalogs.Add(directoryCatalog);
                _directoryCatalogs.Add(directoryCatalog);
            }

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

        public void Recompose()
        {
            foreach (var directoryCatalog in _directoryCatalogs)
            {
                directoryCatalog.Refresh();
                _container.ComposeParts(directoryCatalog.Parts);
            }
            Exports = GetExportedValues();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}