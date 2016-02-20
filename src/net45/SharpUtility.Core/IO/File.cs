using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace SharpUtility.Core.IO
{
    /// <summary>
    /// Unlock and move files,
    /// Unlocker is required
    /// </summary>
    public static class File
    {
        public static string UnlockerLocation
        {
            get
            {
                var value = Registry.GetValue(
                    @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Unlocker.exe",
                    "", "")?.ToString();
                return value ?? string.Empty;
            }
        }

        /// <summary>
        ///     Unlock a file using Unlocker
        /// </summary>
        /// <param name="path">path to file</param>
        /// <returns></returns>
        public static Process Unlock(string path)
        {
            if (System.IO.File.Exists(UnlockerLocation))
                return Unlock(path, UnlockerLocation);
            return null;
        }

        /// <summary>
        ///     Unlock a file using Unlocker
        /// </summary>
        /// <param name="path">path to file</param>
        /// <param name="unlockerPath">path to unlocker</param>
        /// <returns></returns>
        public static Process Unlock(string path, string unlockerPath)
        {
            var arg = $"\"{unlockerPath}\" /s \"{path}\"";

            if (System.IO.File.Exists(unlockerPath))
                return Process.Start(path, arg);
            
            throw new Exception("Incorrect unlocker path");
        }

        /// <summary>
        ///     Unlock and move file
        /// </summary>
        /// <param name="path">path to file</param>
        /// <param name="destination">file destination</param>
        public static void UnlockAndMove(string path, string destination)
        {
            UnlockAndMove(path, destination, UnlockerLocation);
        }

        /// <summary>
        ///     Unlock and move file
        /// </summary>
        /// <param name="path">path to file</param>
        /// <param name="destination">file destination</param>
        /// <param name="unlockerPath">path to unlocker</param>
        public static void UnlockAndMove(string path, string destination, string unlockerPath)
        {
            var process = Unlock(path, unlockerPath);
            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) => { System.IO.File.Move(path, destination); };
        }
    }
}