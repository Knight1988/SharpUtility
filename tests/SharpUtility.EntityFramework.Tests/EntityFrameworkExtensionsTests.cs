using System;
using NUnit.Framework;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using Ionic.Zip;

namespace SharpUtility.EntityFramework.Tests
{
    [TestFixture()]
    public class EntityFrameworkExtensionsTests
    {
        protected string MetaData = "res://*/AdventureWorksModel.csdl|res://*/AdventureWorksModel.ssdl|res://*/AdventureWorksModel.msl";

        [SetUp]
        public void Setup()
        {
            var dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            var zipPath = Path.Combine(dataFolder, "AdventureWorks2008R2-Full Database Backup.zip");
            var backupFile = Path.Combine(dataFolder, "AdventureWorks2008R2-Full Database Backup.bak");
            var dataFile = Path.Combine(dataFolder, "AdventureWorks2008R2.mdf");
            var logFile = Path.Combine(dataFolder, "AdventureWorks2008R2.ldf");
            var sql = $@"RESTORE DATABASE [AdventureWorks2008R2] FROM  DISK = N'{backupFile}' WITH  FILE = 1,  MOVE N'AdventureWorks2008R2_Data' TO N'{dataFile}',  MOVE N'AdventureWorks2008R2_Log' TO N'{logFile}',  NOUNLOAD,  REPLACE,  STATS = 10";

            // Extract backup file
            using (ZipFile zip = ZipFile.Read(zipPath))
            {
                // here, we extract every entry, but we could extract conditionally
                // based on entry name, size, date, checkbox status, etc.  
                foreach (ZipEntry e in zip)
                {
                    e.Extract(dataFolder, ExtractExistingFileAction.OverwriteSilently);
                }
            }

            using (var cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["MasterConnection"].ConnectionString))
            {
                var cmd = new SqlCommand(sql, cnn);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        [Test()]
        public void ToEntityConnectionTest()
        {
            // Arrange
            var db = new Entities(ConfigurationManager.ConnectionStrings["AdventureWorkConnection"].ConnectionString.ToEntityFrameworkConnectionString(MetaData));
            
            // Act
            var actual = db.AWBuildVersions.Any();

            // Assert
            Assert.True(actual);
        }
    }
}