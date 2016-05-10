using System;

namespace SharpUtility.Common
{
    public class ExceptionHandlerAction
    {
        public Type ExceptionType { get; set; }
        public dynamic Action { get; set; }
        public int MaxRetries { get; set; }
        public int Retry { get; set; }
    }
}