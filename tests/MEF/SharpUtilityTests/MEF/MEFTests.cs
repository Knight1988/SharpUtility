using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpUtility.MEF;

namespace SharpUtilityTests.MEF
{
    [TestClass]
    public class MEFTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var t = new ContractImporter();
            t.DoImport();
            Assert.AreEqual(1, t.AvailableNumberOfOperation);

            var result = t.Operations.First().Value.ManipulateOperation(125, 5, 10, 27, 45, 19, 10);
            Assert.AreEqual(241, result);
        }

        [TestMethod]
        public void Test2()
        {
            var importer = new Importer();
            var dlls = Environment.CurrentDirectory + "\\Dlls";
            importer.Recompose();
        }
    }
}
