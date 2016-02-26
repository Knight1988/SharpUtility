using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Reflection;

namespace SharpUtility.EntityFramework
{
    public static class EntityFrameworkExtensions
    {
        public static EntityConnection ToEntityConnection(this SqlConnection sqlConnection)
        {
            var workspace = new MetadataWorkspace(new[] {"res://*/"}, new[] {Assembly.GetExecutingAssembly()});

            return new EntityConnection(workspace, sqlConnection);
        }
    }
}