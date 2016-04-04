using System;

namespace SharpUtility.Threading
{
    public class ProgressChangedEventArgs : EventArgs
    {
        public ProgressChangedEventArgs(long progressPercentage, object userState)
        {
            ProgressPercentage = progressPercentage;
            UserState = userState;
        }

        public long ProgressPercentage { get; }

        public object UserState { get; }
    }
}