using System;
using System.Windows.Forms;

namespace SharpUtility.WinForm
{
    public static class FormExtensions
    {
        /// <summary>
        /// Thread safe update a control
        /// </summary>
        /// <typeparam name="TControl">Control type</typeparam>
        /// <param name="control">Control to update</param>
        /// <param name="action">update action</param>
        public static void InvokeIfRequired<TControl>(this TControl control, Action<TControl> action) where TControl : Control
        {
            if (!control.InvokeRequired)
            {
                action(control);
                return;
            }
            control.Invoke(action, control);
        }

        /// <summary>
        /// Thread safe update a control
        /// </summary>
        /// <typeparam name="TControl">Control type</typeparam>
        /// <param name="control">Control to update</param>
        /// <param name="action">update action</param>
        public static void InvokeIfRequired<TControl>(this TControl control, Action action) where TControl : Control
        {
            if (!control.InvokeRequired)
            {
                action();
                return;
            }
            control.Invoke(action);
        }

        /// <summary>
        /// Thread safe update a control
        /// </summary>
        /// <typeparam name="TControl">Control type</typeparam>
        /// <typeparam name="TReturn">Return type</typeparam>
        /// <param name="control">Control to update</param>
        /// <param name="action">update action</param>
        public static TReturn InvokeIfRequired<TControl, TReturn>(this TControl control, Func<TReturn> action) where TControl : Control
        {
            if (!control.InvokeRequired)
            {
                return action();
            }
            return (TReturn) control.Invoke(action);
        }

        /// <summary>
        /// Thread safe update a control
        /// </summary>
        /// <typeparam name="TControl">Control type</typeparam>
        /// <typeparam name="TReturn">Return type</typeparam>
        /// <param name="control">Control to update</param>
        /// <param name="action">update action</param>
        public static TReturn InvokeIfRequired<TControl, TReturn>(this TControl control, Func<TControl, TReturn> action) where TControl : Control
        {
            if (!control.InvokeRequired)
            {
                return action(control);
            }
            return (TReturn) control.Invoke(action, control);
        }
    }
}