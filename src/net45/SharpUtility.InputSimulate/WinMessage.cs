namespace SharpUtility.InputSimulate
{
    internal enum WinMessage
    {
        /// <summary>
        ///     Command message is sent when the user selects a command item from a menu,
        ///     when a control sends a notification message to its parent window, or when an
        ///     accelerator keystroke is translated.
        /// </summary>
        Command = 0x111,

        /// <summary>
        ///     Left button down
        /// </summary>
        LeftButtonDown = 0x201,

        /// <summary>
        ///     Left button up
        /// </summary>
        LeftButtonUp = 0x202,

        /// <summary>
        ///     Left button double click
        /// </summary>
        LeftButtonDoubleClick = 0x203,

        /// <summary>
        ///     Right button down
        /// </summary>
        RightButtonDown = 0x204,

        /// <summary>
        ///     Right button up
        /// </summary>
        RightButtonUp = 0x205,

        /// <summary>
        ///     Right button double click
        /// </summary>
        RightButtonDoubleClick = 0x206,

        /// <summary>
        ///     Key down
        /// </summary>
        KeyDown = 0x100,

        /// <summary>
        ///     Key up
        /// </summary>
        KeyUp = 0x101
    }
}