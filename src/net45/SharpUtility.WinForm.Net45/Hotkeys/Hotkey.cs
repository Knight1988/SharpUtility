using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SharpUtility.WinForm.Hotkeys
{
    public static class Hotkey
    {
        private static readonly List<HotkeyInfo> Actions;
        private static readonly KeyboardHook Hook;

        static Hotkey()
        {
            Actions = new List<HotkeyInfo>();
            Hook = new KeyboardHook();
            Hook.KeyPressed += HookOnKeyPressed;
        }

        public static void DisableHotkey(ModifierKeys modifierKeys, Keys keys)
        {
            var hotkeyInfo = GetHotkeyInfo(modifierKeys, keys);
            if (hotkeyInfo != null)
            {
                Hook.UnregisterHotkey(hotkeyInfo.Id);
                Actions.Remove(hotkeyInfo);
            }
        }

        public static void DisableHotkey(Keys keys)
        {
            DisableHotkey(0, keys);
        }

        public static void SetHotkey(ModifierKeys modifierKeys, Keys keys, Action action)
        {
            var num = Hook.RegisterHotKey(modifierKeys, keys);
            var hotkeyInfo = new HotkeyInfo
            {
                Id = num,
                ModifierKeys = modifierKeys,
                Keys = keys,
                Action = action
            };
            Actions.Add(hotkeyInfo);
        }

        public static void SetHotkey(Keys keys, Action action)
        {
            SetHotkey(0, keys, action);
        }

        private static HotkeyInfo GetHotkeyInfo(ModifierKeys modifierKeys, Keys keys)
        {
            return Actions.FirstOrDefault(p =>
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