using Contracts;
using SharpUtility.MEF;

namespace SharpUtilityTests.MEF
{
    internal class ContractImporter : ImporterBase<IComponent>
    {
        public ContractImporter()
        {
        }

        public ContractImporter(string pluginPath) : base(pluginPath)
        {
        }
    }
}