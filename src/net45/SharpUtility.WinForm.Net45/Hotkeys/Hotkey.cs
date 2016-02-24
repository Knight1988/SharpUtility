using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SharpUtility.WinForm.Hotkeys
{
    public static class Hotkey
    {
        private static readonly List<HotkeyInfo> _actions;
        private static readonly KeyboardHook _hook;

        static Hotkey()
        {
            _actions = new List<HotkeyInfo>();
            _hook = new KeyboardHook();
            _hook.KeyPressed += HookOnKeyPressed;
        }

        public static void DisableHotkey(ModifierKeys modifierKeys, Keys keys)
        {
            var hotkeyInfo = GetHotkeyInfo(modifierKeys, keys);
            if (hotkeyInfo != null)
            {
                _hook.UnregisterHotkey(hotkeyInfo.Id);
                _actions.Remove(hotkeyInfo);
            }
        }

        public static void DisableHotkey(Keys keys)
        {
            DisableHotkey(0, keys);
        }

        public static void SetHotkey(ModifierKeys modifierKeys, Keys keys, Action action)
        {
            var num = _hook.RegisterHotKey(modifierKeys, keys);
            var hotkeyInfo = new HotkeyInfo
            {
                Id = num,
                ModifierKeys = modifierKeys,
                Keys = keys,
                Action = action
            };
            _actions.Add(hotkeyInfo);
        }

        public static void SetHotkey(Keys keys, Action action)
        {
            SetHotkey(0, keys, action);
        }

        private static HotkeyInfo GetHotkeyInfo(ModifierKeys modifierKeys, Keys keys)
        {
            return _actions.FirstOrDefault(p =>
            {
                if (p.ModifierKeys != modifierKeys)
                {
                    return false;
                }
                return p.Keys == keys;
            });
        }

        private static void HookOnKeyPressed(object sender, KeyPressedEventArgs args)
        {
            GetHotkeyInfo(args.Modifier, args.Key).Action();
        }
    }
}