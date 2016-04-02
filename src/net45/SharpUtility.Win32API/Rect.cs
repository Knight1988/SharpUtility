using System.Runtime.InteropServices;

namespace SharpUtility.Win32API
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        /// <summary>
        /// x position of upper-left corner
        /// </summary>
        public int Left;
        /// <summary>
        ///  y position of upper-left corner
        /// </summary>
        public int Top;        
        /// <summary>
        /// x position of lower-right corner
        /// </summary>
        public int Right;       
        /// <summary>
        /// y position of lower-right corner
        /// </summary>
        public int Bottom;      
    }
}