using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SharpUtility.WinForm.Hotkeys
{
    public sealed class KeyboardHook : IDisposable
    {
        private readonly Window _window = new Window();
        private int _currentId;

        public KeyboardHook()
        {
            _window.KeyPressed += (sender, args) =>
            {
                KeyPressed?.Invoke(this, args);
            };
        }

        public int RegisterHotKey(ModifierKeys modifier, Keys key)
        {
            _currentId = _currentId + 1;
            if (!RegisterHotKey(_window.Handle, _currentId, (uint) modifier, (uint) key))
            {
                throw new InvalidOperationException("Couldn’t register the hot key.");
            }
            return _currentId;
        }

        public void UnregisterHotkey(int id)
        {
            if (!UnregisterHotKey(_window.Handle, id))
            {
                throw new InvalidOperationException("Couldn’t unregister the hot key.");
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public void Dispose()
        {
            for (var i = _currentId; i > 0; i--)
            {
                UnregisterHotKey(_window.Handle, i);
            }
            _window.Dispose();
        }

        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        private sealed class Window : NativeWindow, IDisposable
        {
            private const int WM_HOTKEY = 786;

            public Window()
            {
                CreateHandle(new CreateParams());
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                if (m.Msg == WM_HOTKEY)
                {
                    var lParam = (Keys) ((int) m.LParam >> 16 & 65535);
                    var modifierKey = (ModifierKeys) ((int) m.LParam & 65535);
                    KeyPressed?.Invoke(this, new KeyPressedEventArgs(modifierKey, lParam));
                }
            }

            public void Dispose()
            {
                DestroyHandle();
            }

            public event EventHandler<KeyPressedEventArgs> KeyPressed;
        }
    }
}