using System;

namespace SharpUtility.Threading
{
    public class DoWorkEventArgs : EventArgs
    {
        public object[] Arguments { get; }

        public DoWorkEventArgs() { }

        public DoWorkEventArgs(object[] arguments)
        {
            Arguments = arguments;
        }
    }
}