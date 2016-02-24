using System;
using System.Windows.Forms;

namespace SharpUtility.WinForm.Hotkeys
{
    internal class HotkeyInfo
    {
        public Action Action { get; set; }
        public int Id { get; set; }
        public Keys Keys { get; set; }
        public ModifierKeys ModifierKeys { get; set; }
    }
}