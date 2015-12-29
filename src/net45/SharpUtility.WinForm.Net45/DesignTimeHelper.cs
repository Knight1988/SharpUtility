using System.ComponentModel;
using System.Diagnostics;

namespace SharpUtility.WinForm
{
    public static class DesignTimeHelper
    {
        public static bool IsInDesignMode
        {
            get
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return true;

                using (var process = Process.GetCurrentProcess())
                {
                    return process.ProcessName.ToLowerInvariant().Contains("devenv");
                }
            }
        }
    }
}