using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpUtility.StringManipulation
{
    public static class StringConvertPerformance
    {
        public static IEnumerable<ByteArrayToHexResult> ByteArrayToHexPerformanceTest(byte[] bytes, int loops)
        {
            // loop al modes
            foreach (ByteArrayToHexMode mode in Enum.GetValues(typeof(ByteArrayToHexMode)))
            {
                // create test result
                var result = new ByteArrayToHexResult {Mode = mode};

                // start stopwatch
                var sw = new Stopwatch();
                sw.Start();

                // convert for a number of loops
                for (var i = 0; i < loops; i++)
                {
                    StringConvert.ByteArrayToHex(bytes, mode);
                }

                // stop watch
                sw.Stop();

                // return the result
                result.ElapsedTicks = sw.ElapsedTicks;
                yield return result;
            }
        }

        public class ByteArrayToHexResult
        {
            public ByteArrayToHexMode Mode { get; set; }
            public long ElapsedTicks { get; set; }

            public override string ToString()
            {
                return Mode.ToString().PadRight(40) + ElapsedTicks.ToString("N0");
            }
        }
    }
}
