using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpUtility.MEF;
using Contracts;
using SharpUtilityTests.MEF;

namespace SharpUtilityTests
{
    [TestClass]
    public class MEFTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var t = new ContractImporter(Environment.CurrentDirectory);
            t.DoImport();
            Assert.AreEqual(1, t.AvailableNumberOfOperation);

            var result = t.Operations.First().Value.ManipulateOperation(125, 5, 10, 27, 45, 19, 10);
            Assert.AreEqual(241, result);
        }
    }
}
