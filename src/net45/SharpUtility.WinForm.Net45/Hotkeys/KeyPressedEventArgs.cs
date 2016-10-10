using System;
using System.Windows.Forms;

namespace SharpUtility.WinForm.Hotkeys
{
    public class KeyPressedEventArgs : EventArgs
    {
        public Keys Key { get; }

        public ModifierKeys Modifier { get; }

        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            Modifier = modifier;
            Key = key;
        }
    }
}