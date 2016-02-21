using System;
using System.Runtime.InteropServices;

namespace HarmanAmbient
{
    public class HotKeys
    {
        public const int SingleMode = 1;
        public const int SplitMode = 2;
        public const int IncreaseBrightness = 3;
        public const int DecreaseBrightness = 4;
        public const int ManualBrightnessMode = 5;
        public const int AutomaticBrightnessMode = 6;

        // DLL libraries used to manage hotkeys
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}