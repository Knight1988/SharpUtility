using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace SharpUtility.InputSimulate
{
    /// <summary>
    ///     Object that holds window specific information.
    /// </summary>
    public class WindowInformation
    {
        #region Constructor

        #endregion

        #region Properties

        /// <summary>
        ///     The window caption.
        /// </summary>
        public string Caption = string.Empty;

        /// <summary>
        ///     The window class.
        /// </summary>
        public string Class = string.Empty;

        /// <summary>
        ///     Children of the window.
        /// </summary>
        public List<WindowInformation> ChildWindows = new List<WindowInformation>();

        /// <summary>
        ///     Unmanaged code to get the process and thres IDs of the window.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        [DllImport("user32")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);

        /// <summary>
        ///     The string representation of the window.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Window " + Handle + " \"" + Caption + "\" " + Class;
        }

        /// <summary>
        ///     The handles of the child windows.
        /// </summary>
        public List<IntPtr> ChildWindowHandles
        {
            get
            {
                try
                {
                    var handles = from c in ChildWindows.AsEnumerable()
                        select c.Handle;
                    return handles.ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///     The window handle.
        /// </summary>
        public IntPtr Handle;

        /// <summary>
        ///     The parent window.
        /// </summary>
        public WindowInformation Parent { get; set; }

        /// <summary>
        ///     The handle of the parent of the window.
        /// </summary>
        public IntPtr ParentHandle
        {
            get
            {
                if (Parent != null) return Parent.Handle;
                return IntPtr.Zero;
            }
        }

        /// <summary>
        ///     The corresponding process.
        /// </summary>
        public Process Process
        {
            get
            {
                try
                {
                    int processId;
                    GetWindowThreadProcessId(Handle, out processId);
                    return Process.GetProcessById(processId);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///     Sibling window information.
        /// </summary>
        public List<WindowInformation> SiblingWindows = new List<WindowInformation>();

        /// <summary>
        ///     The handles of the sibling windows.
        /// </summary>
        public List<IntPtr> SiblingWindowHandles
        {
            get
            {
                try
                {
                    var handles = from s in SiblingWindows.AsEnumerable()
                        select s.Handle;
                    return handles.ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///     The thread ID of the window. Returns -1 on exception.
        /// </summary>
        public int ThreadId
        {
            get
            {
                try
                {
                    int dummy;
                    return GetWindowThreadProcessId(Handle, out dummy);
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        #endregion
    }
}