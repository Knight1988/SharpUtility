using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpUtility.Win32API;

namespace SharpUtility.InputSimulate
{
    public static class KeyboardSimulator
    {
        /// <summary>
        ///     Send a key down to a given window
        /// </summary>
        /// <param name="handle">handle to destination window</param>
        /// <param name="key">key to send</param>
        public static void Down(IntPtr handle, Keys key)
        {
            Win32.PostMessage(handle, (IntPtr) WinMessage.KeyDown, (IntPtr) key, IntPtr.Zero);
        }

        /// <summary>
        ///     Sends simulated keystrokes to a given window.
        /// </summary>
        /// <param name="handle">handle to destination window</param>
        /// <param name="key">key to send</param>
        /// <param name="keyDownDelay">
        ///     Alters the length of time a key is held down before being released during a keystroke.
        ///     For applications that take a while to register keypresses you may need to raise this value from the default.
        ///     A value of 0 removes the delay completely.
        ///     Time in milliseconds to pause(default=5).
        /// </param>
        /// <returns></returns>
        public static async Task SendAsync(IntPtr handle, Keys key, int keyDownDelay)
        {
            Down(handle, key);
            await Task.Delay(keyDownDelay);
            Up(handle, key);
        }

        /// <summary>
        ///     Sends simulated keystrokes to a given window.
        /// </summary>
        /// <param name="handle">handle to destination window</param>
        /// <param name="key">key to send</param>
        /// <returns></returns>
        public static async Task SendAsync(IntPtr handle, Keys key)
        {
            await SendAsync(handle, key, 5);
        }

        /// <summary>
        ///     Send a key up to a given window
        /// </summary>
        /// <param name="handle">handle to destination window</param>
        /// <param name="key">key to send</param>
        public static void Up(IntPtr handle, Keys key)
        {
            Win32.PostMessage(handle, (IntPtr) WinMessage.KeyUp, (IntPtr) key, IntPtr.Zero);
        }
    }
}