using System;

namespace SharpUtility.Threading
{
    public class CompleteEventArgs : EventArgs
    {
        public object Result { get; }
        public object UserState { get; }

        public CompleteEventArgs(object result, object userState)
        {
            Result = result;
            UserState = userState;
        }
    }
}