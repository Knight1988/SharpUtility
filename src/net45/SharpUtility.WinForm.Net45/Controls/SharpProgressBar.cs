using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpUtility.Threading;
using SharpUtility.Time;

namespace SharpUtility.WinForm.Controls
{
    public class SharpProgressBar : ProgressBar
    {
        protected Action Action;
        protected Func<Task> ActionTask;

        /// <summary>
        /// Create backgroundWorker without a task. 
        /// Task must be set in delivery class.
        /// </summary>
        protected SharpProgressBar()
        {
        }

        /// <summary>
        /// Create backgroundWorker run an async task
        /// </summary>
        /// <param name="action"></param>
        public SharpProgressBar(Func<Task> action)
        {
            ActionTask = action;
        }

        /// <summary>
        /// Create Background worker run a task
        /// </summary>
        /// <param name="action"></param>
        public SharpProgressBar(Action action)
        {
            Action = action;
        }

        /// <summary>
        /// Start the task
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task RunWorkerAsync(CancellationToken token)
        {
            if (Action != null)
            {
                await Task.Run(Action, token);
            }

            if (ActionTask != null)
            {
                await Task.Run(ActionTask, token);
            }
        }

        /// <summary>
        /// Start the task
        /// </summary>
        /// <returns></returns>
        public Task RunWorkerAsync()
        {
            if (Action != null)
            {
                return Task.Run(Action);
            }

            if (ActionTask != null)
            {
                return Task.Run(ActionTask);
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Set the value with thread safe
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="maximum">maximum</param>
        public void SetValue(int value, int maximum)
        {
            this.InvokeIfRequired(() =>
            {
                Value = value;
                Maximum = maximum;
            });
        }
    }
}
