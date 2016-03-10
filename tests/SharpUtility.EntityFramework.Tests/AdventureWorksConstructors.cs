using System.Data.Common;

namespace SharpUtility.EntityFramework.Tests
{
    public partial class Entities
    {
        public Entities(DbConnection dbConnection) : base(dbConnection, true)
        {
            
        }

        public Entities(string connectionString) : base(connectionString)
        {
            
        }
    }
}
