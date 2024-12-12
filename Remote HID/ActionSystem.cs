using System.Diagnostics;
using System.Runtime.InteropServices;
namespace Remote_HID
{
    public class ActionSystem
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        private const byte VK_LWIN = 0x5B;
        private const byte VK_TAB = 0x09;
        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const byte VK_A = 0x41;

        public void OpenSettings()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "ms-settings:",
                UseShellExecute = true
            });
        }

        public void OpenSettingWifi()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "ms-settings:network-wifi",
                UseShellExecute = true
            });
        }

        public void OpenSettingBluetooth()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "settings:bluetooth",
                UseShellExecute = true
            });
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
            if (System.IO.File.Exists(programPath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = programPath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("File không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OpenUrl(string url)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
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
    }
}
