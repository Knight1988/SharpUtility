using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpUtility.Win32API;

namespace SharpUtility.InputSimulate
{
    public static class WindowFinder
    {
        public static IntPtr FindWindow(string className, string windowName)
        {
            return Win32.FindWindow(className, windowName);
        }
    }
}
