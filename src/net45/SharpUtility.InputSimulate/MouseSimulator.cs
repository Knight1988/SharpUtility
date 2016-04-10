using System;
using System.Threading.Tasks;
using SharpUtility.Win32API;

namespace SharpUtility.InputSimulate
{
    public static class MouseSimulator
    {
        /// <summary>
        ///     Sends a mouse click command to a given window.
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="mouseButton">The button to click, "left", "right". Default is the left button.</param>
        /// <param name="x">The x position to click within the control. Default is center.</param>
        /// <param name="y">The y position to click within the control. Default is center.</param>
        /// <param name="clicks">The number of times to click the mouse. Default is 1.</param>
        /// <param name="clickDelay">
        ///     Alters the length of the brief pause in between mouse clicks.
        ///     Time in milliseconds to pause (default=10).
        /// </param>
        /// <param name="clickDownDelay">
        ///     Alters the length a click is held down before release.
        ///     Time in milliseconds to pause (default=10).
        /// </param>
        public static async Task ClickAsync(IntPtr windowHandle, MouseButton mouseButton, int x, int y, int clicks,
            int clickDelay, int clickDownDelay)
        {
            for (var i = 0; i < clicks; i++)
            {
                Down(windowHandle, mouseButton, x, y);
                await Task.Delay(clickDownDelay);
                Up(windowHandle, mouseButton, x, y);
                await Task.Delay(clickDelay);
            }
        }

        /// <summary>
        ///     Sends a mouse click command to a given window.
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="mouseButton">The button to click, "left", "right". Default is the left button.</param>
        /// <param name="x">The x position to click within the control. Default is center.</param>
        /// <param name="y">The y position to click within the control. Default is center.</param>
        /// <param name="clicks">The number of times to click the mouse. Default is 1.</param>
        /// <param name="clickDelay"></param>
        public static async Task ClickAsync(IntPtr windowHandle, MouseButton mouseButton, int x, int y, int clicks,
            int clickDelay)
        {
            await ClickAsync(windowHandle, mouseButton, x, y, clicks, clickDelay, 10);
        }

        /// <summary>
        ///     Sends a mouse click command to a given window.
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="mouseButton">The button to click, "left", "right". Default is the left button.</param>
        /// <param name="x">The x position to click within the control. Default is center.</param>
        /// <param name="y">The y position to click within the control. Default is center.</param>
        /// <param name="clicks">The number of times to click the mouse. Default is 1.</param>
        public static async Task ClickAsync(IntPtr windowHandle, MouseButton mouseButton, int x, int y, int clicks)
        {
            await ClickAsync(windowHandle, mouseButton, x, y, 1, 10, 10);
        }

        /// <summary>
        ///     Sends a mouse click command to a given window.
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="mouseButton">The button to click, "left", "right". Default is the left button.</param>
        /// <param name="x">The x position to click within the control. Default is center.</param>
        /// <param name="y">The y position to click within the control. Default is center.</param>
        public static async Task ClickAsync(IntPtr windowHandle, MouseButton mouseButton, int x, int y)
        {
            await ClickAsync(windowHandle, mouseButton, x, y, 1);
        }

        /// <summary>
        ///     Sends a mouse down command to a given window.
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="mouseButton">The button to click, "left", "right". Default is the left button.</param>
        /// <param name="x">The x position to click within the control. Default is center.</param>
        /// <param name="y">The y position to click within the control. Default is center.</param>
        public static void Down(IntPtr windowHandle, MouseButton mouseButton, int x, int y)
        {
            WinMessage msg;
            switch (mouseButton)
            {
                    case MouseButton.Right:
                    msg = WinMessage.RightButtonDown;
                    break;
                default:
                    msg = WinMessage.LeftButtonDown;
                    break;

            }
            Win32.PostMessage(windowHandle, (IntPtr)msg, (IntPtr) 0x00000001,
                Win32.CreateLParam(x, y));
        }

        /// <summary>
        ///     Sends a mouse up command to a given window.
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="mouseButton">The button to click, "left", "right". Default is the left button.</param>
        /// <param name="x">The x position to click within the control. Default is center.</param>
        /// <param name="y">The y position to click within the control. Default is center.</param>
        public static void Up(IntPtr windowHandle, MouseButton mouseButton, int x, int y)
        {
            WinMessage msg;
            switch (mouseButton)
            {
                case MouseButton.Right:
                    msg = WinMessage.RightButtonUp;
                    break;
                default:
                    msg = WinMessage.LeftButtonUp;
                    break;

            }
            Win32.PostMessage(windowHandle, (IntPtr)msg, (IntPtr)0x00000001,
                Win32.CreateLParam(x, y));
        }
    }
}