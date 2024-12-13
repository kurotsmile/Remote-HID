using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Remote_HID
{
    public class GlobalKeyHook
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private IntPtr _hookID = IntPtr.Zero;
        private LowLevelKeyboardProc _proc;
        private Form1 frm;
        public event Action<Keys> KeyPressed;

        public GlobalKeyHook(Form1 frm)
        {
            _proc = HookCallback;
            this.frm = frm;
        }

        public void Start()
        {
            if (_hookID == IntPtr.Zero)
            {
                _hookID = SetHook(_proc);
            }
        }

        public void Stop()
        {
            if (_hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
            }
        }

        public void Dispose()
        {
            Stop();
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN )
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;

                KeyPressed?.Invoke(key);

                if ((Keys)vkCode == Keys.Apps)
                {
                    if (frm.Visible)
                    {
                        frm.PlaySound(Properties.Resources.boxClose);
                        frm.On_hide();
                        Thread.Sleep(800);
                        SendEscapeKey();
                        return (IntPtr)1;
                    }
                    else
                    {
                        frm.PlaySound(Properties.Resources.boxOpen);
                        frm.On_Show();
                        return (IntPtr)1;
                    }
                }

                if (frm.Visible)
                {
                    if ((Keys)vkCode == Keys.Enter) frm.Act_Menu();
                    if ((Keys)vkCode == Keys.Back) frm.Act_Menu();
                    if ((Keys)vkCode == Keys.Left) frm.Move_menu(Keys.Left);
                    if ((Keys)vkCode == Keys.Right) frm.Move_menu(Keys.Right);
                    if ((Keys)vkCode == Keys.Up) frm.Move_menu(Keys.Up);
                    if ((Keys)vkCode == Keys.Down) frm.Move_menu(Keys.Down);
                    return (IntPtr)1;
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public uint type;
            public InputUnion u;
        }


        [StructLayout(LayoutKind.Explicit)]
        struct InputUnion
        {
            [FieldOffset(0)] public KEYBDINPUT ki;
            [FieldOffset(0)] public MOUSEINPUT mi;
            [FieldOffset(0)] public HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        const uint INPUT_KEYBOARD = 1;
        const uint KEYEVENTF_KEYDOWN = 0x0000;
        const uint KEYEVENTF_KEYUP = 0x0002;

        private void SendEscapeKey()
        {
            INPUT[] inputs = new INPUT[2];

            // Nhấn phím ESC
            inputs[0].type = INPUT_KEYBOARD;
            inputs[0].u.ki.wVk = 0x1B;
            inputs[0].u.ki.dwFlags = KEYEVENTF_KEYDOWN;

            // Nhả phím ESC
            inputs[1].type = INPUT_KEYBOARD;
            inputs[1].u.ki.wVk = 0x1B;
            inputs[1].u.ki.dwFlags = KEYEVENTF_KEYUP;

            // Gửi phím ESC
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
