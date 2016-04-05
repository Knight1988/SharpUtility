using System;

namespace SharpUtility.Threading
{
    public class ProgressChangedEventArgs : EventArgs
    {
        public double ProgressPercentage { get; }

        public object UserState { get; }

        public ProgressChangedEventArgs(double progressPercentage, object userState)
        {
            ProgressPercentage = progressPercentage;
            UserState = userState;
        }
    }
}