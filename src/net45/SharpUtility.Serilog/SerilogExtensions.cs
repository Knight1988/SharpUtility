using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace SharpUtility.Serilog
{
    public static class SerilogExtensions
    {
        public static void Debug(this ILogger logger, string messageTemplate, [CallerMemberName]
            string callerName = null, [CallerFilePath] string
            callerFilePath = null, [CallerLineNumber] int callerLineNumber = -1)
        {
            messageTemplate = messageTemplate.Replace("{callerName}", callerName)
                .Replace("{callerFilePath}", callerFilePath)
                .Replace("{callerLineNumber}", callerLineNumber.ToString());
        }
    }
}
