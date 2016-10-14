using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpUtility.StringManipulation;

namespace SharpUtility.Core.Tests.StringManipulation
{
    [TestFixture()]
    internal class StringConvertTests
    {
        [TestCase(ByteArrayToHexMode.BitConverter)]
        [TestCase(ByteArrayToHexMode.ByteManipulation)]
        [TestCase(ByteArrayToHexMode.ByteManipulation2)]
        [TestCase(ByteArrayToHexMode.Lookup)]
        [TestCase(ByteArrayToHexMode.LookupPerByte)]
        [TestCase(ByteArrayToHexMode.LookupAndShift)]
        [TestCase(ByteArrayToHexMode.LookupUnsafe)]
        [TestCase(ByteArrayToHexMode.LookupUnsafeDirect)]
        [TestCase(ByteArrayToHexMode.StringBuilderForEachByteToString)]
        [TestCase(ByteArrayToHexMode.StringBuilderForEachAppendFormat)]
        [TestCase(ByteArrayToHexMode.StringBuilderAggregateByteToString)]
        [TestCase(ByteArrayToHexMode.StringBuilderAggregateAppendFormat)]
        public void ByteArrayToHexTest(ByteArrayToHexMode mode)
        {
            var sw = new Stopwatch();
            sw.Start();
            const string str = "a à á ả ã ạ";
            var bytes = StringConvert.StringToBytes(str);
            var hex = StringConvert.ByteArrayToHex(bytes, mode);
            for (var i = 0; i < 10; i++)
            {
                hex = StringConvert.ByteArrayToHex(bytes, mode);
            }
            Assert.AreEqual("61002000E0002000E1002000A31E2000E3002000A11E", hex);
            sw.Stop();
            Console.WriteLine(@"{0:n0}", sw.ElapsedTicks);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        public void ByteArrayToHexPerformanceTest(int testStringLength)
        {
            var sb = new StringBuilder("a à á ả ã ạ");
            for (var i = 0; i < testStringLength; i++)
            {
                sb.Append("a à á ả ã ạ");
            }
            var bytes = StringConvert.StringToBytes(sb.ToString());
            var results = StringConvertPerformance.ByteArrayToHexPerformanceTest(bytes, 10).OrderBy(p => p.ElapsedTicks);
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        [TestCase(HexToByteArrayMode.Lookup)]
        [TestCase(HexToByteArrayMode.Complex)]
        [TestCase(HexToByteArrayMode.ByteParse)]
        [TestCase(HexToByteArrayMode.Enumerable)]
        [TestCase(HexToByteArrayMode.ByteCasting)]
        [TestCase(HexToByteArrayMode.ConvertToByte)]
        [TestCase(HexToByteArrayMode.DictionaryAndList)]
        public void HexToByteArrayTest(HexToByteArrayMode mode)
        {
            var sw = new Stopwatch();
            sw.Start();
            const string str = "a à á ả ã ạ";
            var bytes = StringConvert.StringToBytes(str);
            var hex = StringConvert.ByteArrayToHex(bytes);
            for (var i = 0; i < 10; i++)
            {
                bytes = StringConvert.HexToByteArray(hex, mode);
            }
            var result = StringConvert.BytesToString(bytes);
            Assert.AreEqual(str, result);
            sw.Stop();
            Console.WriteLine(@"{0:n0}", sw.ElapsedTicks);
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        [TestCase(100000)]
        public void HexToByteArrayPerformanceTest(int testStringLength)
        {
            var sb = new StringBuilder("a à á ả ã ạ");
            for (var i = 0; i < testStringLength; i++)
            {
                sb.Append("a à á ả ã ạ");
            }
            var bytes = StringConvert.StringToBytes(sb.ToString());
            var hex = StringConvert.ByteArrayToHex(bytes);
            var results = StringConvertPerformance.HexToByteArrayPerformanceTest(hex, 10).OrderBy(p => p.ElapsedTicks);
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }
    }
}
