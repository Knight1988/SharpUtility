using System;

namespace SharpUtility.Threading
{
    public class DoWorkEventArgs : EventArgs
    {
        public DoWorkEventArgs(object[] arguments)
        {
            Arguments = arguments;
        }

        public object[] Arguments { get; }
    }
}