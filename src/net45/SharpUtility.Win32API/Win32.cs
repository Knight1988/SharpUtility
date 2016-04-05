using System;
using System.Runtime.InteropServices;

namespace SharpUtility.Win32API
{
    /// <summary>
    ///     Summary description for Win32.
    /// </summary>
    public class Win32
    {
        /// <summary>
        /// The FindWindow function retrieves a handle to the top-level window whose class name
        /// and window name match the specified strings. This function does not search child windows.
        /// This function does not perform a case-sensitive search.
        /// </summary>
        /// <param name="className"></param>
        /// <param name="windowName"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(string className, string windowName);

        /// <summary>
        /// The FindWindowEx function retrieves a handle to a window whose class name 
        /// and window name match the specified strings. The function searches child windows, beginning
        /// with the one following the specified child window. This function does not perform a case-sensitive search.
        /// </summary>
        /// <param name="hwndParent"></param>
        /// <param name="hwndChildAfter"></param>
        /// <param name="className"></param>
        /// <param name="windowName"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string className,
            string windowName);

        /// <summary>
        /// The PostMessage function sends the specified message to a 
        /// window or windows. It calls the window procedure for the specified window.
        /// </summary>
        /// <param name="hWnd">handle to destination window</param>
        /// <param name="msg">message</param>
        /// <param name="wParam">first message parameter</param>
        /// <param name="lParam">second message parameter</param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern int PostMessage(IntPtr hWnd, IntPtr msg, IntPtr wParam, IntPtr lParam);
        
        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window. 
        /// The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpRect">RECT structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern void GetWindowRect(IntPtr hWnd, out Rect lpRect);

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window. 
        /// The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <returns>RECT structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</returns>
        public static Rect GetWindowRect(IntPtr hWnd)
        {
            Rect lpRect;
            GetWindowRect(hWnd, out lpRect);
            return lpRect;
        }

        public static IntPtr CreateLParam(int loWord, int hiWord)
        {
            return (IntPtr)((hiWord << 16) | (loWord & 0xffff));
        }
    }
}