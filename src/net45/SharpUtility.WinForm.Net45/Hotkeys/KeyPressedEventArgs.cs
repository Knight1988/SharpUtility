using System;
using System.Windows.Forms;

namespace SharpUtility.WinForm.Hotkeys
{
    public class KeyPressedEventArgs : EventArgs
    {
        private readonly Keys _key;
        private readonly ModifierKeys _modifier;

        public Keys Key
        {
            get { return _key; }
        }

        public ModifierKeys Modifier
        {
            get { return _modifier; }
        }

        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            _modifier = modifier;
            _key = key;
        }
    }
}