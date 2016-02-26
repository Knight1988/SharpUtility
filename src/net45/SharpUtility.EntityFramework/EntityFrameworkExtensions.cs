using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

namespace SharpUtility.EntityFramework
{
    public static class EntityFrameworkExtensions
    {
        public static string ToEntityFrameworkConnectionString(this string sqlConnectionString, string metaData)
        {
            const string providerName = "System.Data.SqlClient";

            var efBuilder = new EntityConnectionStringBuilder
            {
                Metadata = metaData,
                Provider = providerName,
                ProviderConnectionString = sqlConnectionString
            };

            return efBuilder.ConnectionString;
        }
    }
}