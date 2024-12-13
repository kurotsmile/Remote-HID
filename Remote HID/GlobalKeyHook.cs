using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Remote_HID
{
    internal class GlobalKeyHook
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;
        private Form1 frm;

        public GlobalKeyHook()
        {
            _proc = HookCallback;
        }

        public void Start(Form1 f)
        {
            this.frm = f;
            _hookID = SetHook(_proc);
        }

        public void Stop()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if ((Keys)vkCode == Keys.Apps)
                {
                    if (frm.Visible)
                    {
                        frm.PlaySound(Properties.Resources.boxClose);
                        frm.On_hide();
                    }
                    else
                    {
                        frm.PlaySound(Properties.Resources.boxOpen);
                        frm.On_Show();
                    }
                    return (IntPtr)1;
                }

                if (frm.Visible)
                {
                    if ((Keys)vkCode == Keys.Enter) frm.Act_Menu();
                    if ((Keys)vkCode == Keys.Back) frm.Act_Menu();
                    if ((Keys)vkCode == Keys.Left) frm.Move_menu(Keys.Left);
                    if ((Keys)vkCode == Keys.Right) frm.Move_menu(Keys.Right);
                    if ((Keys)vkCode == Keys.Up) frm.Move_menu(Keys.Up);
                    if ((Keys)vkCode == Keys.Down) frm.Move_menu(Keys.Down);
                    return (IntPtr)0;
                }
            }
            
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
