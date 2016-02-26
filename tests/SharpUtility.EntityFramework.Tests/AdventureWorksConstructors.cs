using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
