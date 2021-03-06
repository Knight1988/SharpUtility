using System;
using System.Windows.Forms;

namespace SharpUtility.WinForm.Hotkeys
{
    public class KeyPressedEventArgs : EventArgs
    {
        private readonly Keys _key;
        private readonly ModifierKeys _modifier;

        public Keys Key => _key;

        public ModifierKeys Modifier => _modifier;

        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            _modifier = modifier;
            _key = key;
        }
    }
}