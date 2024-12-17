using System.Diagnostics;
using System.Runtime.InteropServices;
namespace Remote_HID
{
    public class ActionSystem
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("PowrProf.dll", SetLastError = true)]
        public static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

        private const byte VK_LWIN = 0x5B;
        private const byte VK_TAB = 0x09;
        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const byte VK_A = 0x41;

        private Form1 frm;

        public ActionSystem(Form1 f)
        {
            this.frm= f;
        }

        public void OpenTaskView()
        {
            keybd_event(VK_LWIN, 0, 0, 0);
            keybd_event(VK_TAB, 0, 0, 0);
            keybd_event(VK_TAB, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_LWIN, 0, KEYEVENTF_KEYUP, 0);
        }

        public void OpenProgram(string programPath)
        {
                Process.Start(new ProcessStartInfo
                {
                    FileName = programPath,
                    UseShellExecute = true
                });
        }

        public void OpenProgramCMD(string programPath)
        {
            if (System.IO.File.Exists(programPath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c \"{programPath}\"",
                    Verb = "runas",
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
            }
            else
            {
                MessageBox.Show("File không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OpenProjectMenu()
        {
            keybd_event(0x5B, 0, 0, 0);
            keybd_event(0x50, 0, 0, 0);
            Thread.Sleep(100);
            keybd_event(0x5B, 0, 2, 0);
            keybd_event(0x50, 0, 2, 0);
        }

        public void OpenActionCenter()
        {
            keybd_event(VK_LWIN, 0, 0, 0);
            keybd_event(VK_A, 0, 0, 0); 

            keybd_event(VK_A, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_LWIN, 0, KEYEVENTF_KEYUP, 0);
        }

        public void OpenOnScreenKeyboard()
        {
            try
            {
                Process.Start("osk.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở bàn phím ảo: " + ex.Message);
            }
        }

        public void OpenDeviceManager()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "devmgmt.msc", 
                    UseShellExecute = true,
                    Verb = "runas"
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể mở Device Manager: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void NextMedia()
        {
            const int APPCOMMAND_MEDIA_NEXTTRACK = 0xB0000;
            const int WM_APPCOMMAND = 0x0319;
            SendMessage(this.frm.Handle, WM_APPCOMMAND, this.frm.Handle, (IntPtr)APPCOMMAND_MEDIA_NEXTTRACK);
        }

        public void PreviousMedia()
        {
            const int APPCOMMAND_MEDIA_PREVIOUSTRACK = 0xC0000;
            const int WM_APPCOMMAND = 0x0319;
            SendMessage(this.frm.Handle, WM_APPCOMMAND, this.frm.Handle, (IntPtr)APPCOMMAND_MEDIA_PREVIOUSTRACK);
        }

        public void FastForwardMedia()
        {
            const byte VK_MEDIA_FORWARD = 0xB0;
            keybd_event(VK_MEDIA_FORWARD, 0, 0,0);
            keybd_event(VK_MEDIA_FORWARD, 0, KEYEVENTF_KEYUP, 0);
        }

        public void FastRewindMedia()
        {
            const byte VK_MEDIA_FAST_REWIND = 0xB4;
            keybd_event(VK_MEDIA_FAST_REWIND, 0, 0, 0);
            keybd_event(VK_MEDIA_FAST_REWIND, 0, KEYEVENTF_KEYUP,0);
        }

        public void PlayPauseMedia()
        {
            const byte VK_MEDIA_PLAY_PAUSE = 0xB3;
            keybd_event(VK_MEDIA_PLAY_PAUSE, 0, 0, 0);
            keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_KEYUP, 0);
        }

        public void Shutdown()
        {
            Process.Start(new ProcessStartInfo("shutdown", "/s /t 0")
            {
                CreateNoWindow = true,
                UseShellExecute = false
            });
        }

        public void Restart()
        {
            Process.Start(new ProcessStartInfo("shutdown", "/r /t 0")
            {
                CreateNoWindow = true,
                UseShellExecute = false
            });
        }

        public void SleepPC()
        {
            SetSuspendState(false, true, false);
        }
    }
}
