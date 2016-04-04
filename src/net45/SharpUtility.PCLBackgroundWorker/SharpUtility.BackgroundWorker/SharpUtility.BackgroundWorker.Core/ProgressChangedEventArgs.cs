using System;

namespace SharpUtility.Threading
{
    public class ProgressChangedEventArgs : EventArgs
    {
        public ProgressChangedEventArgs(double progressPercentage, object userState)
        {
            ProgressPercentage = progressPercentage;
            UserState = userState;
        }

        public double ProgressPercentage { get; }

        public object UserState { get; }
    }
}