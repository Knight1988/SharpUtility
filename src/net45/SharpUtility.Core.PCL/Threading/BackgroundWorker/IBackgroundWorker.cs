﻿using System;

namespace SharpUtility.Threading
{
    public interface IBackgroundWorker
    {
        /// <summary>
        /// Progress percentage
        /// </summary>
        double ProgressPercentage { get; set; }
        /// <summary>
        /// Report the task progress
        /// </summary>
        /// <param name="progressPercentage"></param>
        /// <param name="userState"></param>
        void ReportProgress(double progressPercentage, object userState);
        /// <summary>
        /// Report the task progress
        /// </summary>
        /// <param name="progressPercentage"></param>
        void ReportProgress(double progressPercentage);
        /// <summary>
        /// Report the task progress
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        /// <param name="userState"></param>
        void ReportProgress(double value, double max, object userState);
        /// <summary>
        /// Report the task progress
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        void ReportProgress(double value, double max);

        /// <summary>
        /// Resume a paused action
        /// </summary>
        void Resume();
        event EventHandler<DoWorkEventArgs> DoWork;
        /// <summary>
        /// Complete event
        /// </summary>
        event EventHandler<CompleteEventArgs> Completed;
        /// <summary>
        /// Progress changed event
        /// </summary>
        event EventHandler<ProgressChangedEventArgs> ProgressChanged;
    }
}