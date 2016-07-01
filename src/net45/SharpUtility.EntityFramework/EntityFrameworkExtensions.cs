using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;

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

        public static IEnumerable<T> QueryInChunksOf<T>(this IQueryable<T> queryable, int chunkSize)
        {
            return queryable.QueryChunksOfSize(chunkSize).SelectMany(chunk => chunk);
        }

        public static IEnumerable<T[]> QueryChunksOfSize<T>(this IQueryable<T> queryable, int chunkSize)
        {
            int chunkNumber = 0;
            while (true)
            {
                var query = (chunkNumber == 0)
                    ? queryable
                    : queryable.Skip(chunkNumber * chunkSize);
                var chunk = query.Take(chunkSize).ToArray();
                if (chunk.Length == 0)
                    yield break;
                yield return chunk;
                chunkNumber++;
            }
        }
    }
}