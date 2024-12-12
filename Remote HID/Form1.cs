﻿


using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Remote_HID
{
    public partial class Form1 : Form
    {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;

        private Button[,] buttons;
        private int currentRow = 0;
        private int currentCol = 0;
        private GlobalKeyHook hook;
        string[,] actions = new string[,]
        {
                { "Tắt máy", "Khởi động lại", "Ngủ", "Powershell", "This Pc" },
                { "Star Menu", "Trình Đa nhiệm", "Tìm kiếm", "Đổi mật khẩu", "Người dùng" },
                { "Thông báo", "Lịch sử", "Báo cáo", "Kết nối", "Thông tin hệ thống" },
                { "Cập nhật", "Gỡ bỏ", "Tùy chọn", "Hướng dẫn", "Quản lý" },
                { "Thoát", "Khôi phục", "Cài đặt hệ thống", "Quản lý người dùng", "Đăng nhập" },
                { "Chạy ứng dụng", "Hẹn giờ", "Tải xuống", "Ghi chú", "Mở thư mục" }
        };

        Image[,] icons = new Image[,]
        {
            {
                Image.FromStream(new MemoryStream(Properties.Resources.power_off)),
                Image.FromStream(new MemoryStream(Properties.Resources.reset)),
                Image.FromStream(new MemoryStream(Properties.Resources.sleep)),
                Image.FromStream(new MemoryStream(Properties.Resources.powershell)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer))
            },
            {
                Image.FromStream(new MemoryStream(Properties.Resources.start_menu)),
                Image.FromStream(new MemoryStream(Properties.Resources.apps)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer))
            },
            {
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer))
            },
            {
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer))
            },
            {
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer))
            },
            {
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer)),
                Image.FromStream(new MemoryStream(Properties.Resources.computer))
            }
        };

        public Form1()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.BackColor = Color.Black;
            this.TransparencyKey = this.BackColor;

            notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Text = "App Runging...";
            notifyIcon.Icon = new Icon(new MemoryStream(Properties.Resources.icon_app));

            contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("Hiện cửa sổ", null, ShowWindow);
            contextMenuStrip.Items.Add("Thoát", null, ExitApp);
            notifyIcon.ContextMenuStrip = contextMenuStrip;

            this.Icon = new Icon(new MemoryStream(Properties.Resources.icon_app));
            this.KeyPreview = false;
            this.Load += (s, e) => { this.Hide(); };
            EffectBlur effectBlur = new EffectBlur();
            effectBlur.EnableBlur(this.Handle);
            InitializeGrid(6, 5);
            this.StartPosition = FormStartPosition.CenterScreen;
            GlobalKeyHook hook = new GlobalKeyHook();
            hook.Start(this);
        }

        private void ShowWindow(object sender, EventArgs e)
        {
            this.Show();
        }

        private void ExitApp(object sender, EventArgs e)
        {
            hook.Stop();
            notifyIcon.Visible = false;
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
            base.OnFormClosing(e);
        }

        private void InitializeGrid(int rows, int cols)
        {
            TableLayoutPanel table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = rows,
                ColumnCount = cols,
            };
            for (int i = 0; i < rows; i++) table.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rows));
            for (int j = 0; j < cols; j++) table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / cols));

            buttons = new Button[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button btn = new Button
                    {
                        Text = actions[i, j],
                        Dock = DockStyle.Fill,
                        BackColor = Color.Blue,
                        Font = new Font("Verdana", 15, FontStyle.Regular),
                        Tag = new Point(i, j),
                        FlatStyle = FlatStyle.Flat,
                        ForeColor = Color.Black
                    };
                    btn.Image = icons[i, j];
                    btn.ImageAlign = ContentAlignment.TopCenter;
                    btn.TextAlign = ContentAlignment.BottomCenter;
                    btn.TextImageRelation = TextImageRelation.ImageAboveText;
                    buttons[i, j] = btn;
                    table.Controls.Add(btn, j, i);
                }
            }

            HighlightButton(currentRow, currentCol);

            this.Controls.Add(table);
        }

        private void HighlightButton(int row, int col)
        {
            foreach (var btn in buttons)
            {
                btn.BackColor = Color.Black;
                btn.ForeColor = Color.FromArgb(60, 30, 40);
            }
            buttons[row, col].BackColor = Color.SkyBlue;
            buttons[row, col].ForeColor = Color.AliceBlue;
        }


        public void Move_menu(Keys k)
        {
            int newRow = currentRow, newCol = currentCol;
            switch (k)
            {
                case Keys.Up:
                    newRow = (currentRow - 1 + buttons.GetLength(0)) % buttons.GetLength(0);
                    break;
                case Keys.Down:
                    newRow = (currentRow + 1) % buttons.GetLength(0);
                    break;
                case Keys.Left:
                    newCol = (currentCol - 1 + buttons.GetLength(1)) % buttons.GetLength(1);
                    break;
                case Keys.Right:
                    newCol = (currentCol + 1) % buttons.GetLength(1);
                    break;
            }
            currentRow = newRow;
            currentCol = newCol;
            HighlightButton(currentRow, currentCol);
        }

        public void Act_Menu()
        {
            if (currentRow == 0 && currentCol == 0) Process.Start("shutdown", "/s /f /t 0");
            if (currentRow == 0 && currentCol == 1) Process.Start("shutdown", "/r /f /t 0");
            if (currentRow == 0 && currentCol == 2) Process.Start("rundll32.exe", "powrprof.dll,SetSuspendState Sleep");
            if (currentRow == 0 && currentCol == 3) Process.Start("powershell.exe");
            if (currentRow == 0 && currentCol == 4)
            {
                this.Hide();
                Process.Start("explorer.exe", "shell:MyComputerFolder");
            }

            if (currentRow == 1 && currentCol == 0)
            {
                this.Hide();
                SendKeys.SendWait("^{ESC}");
            }

            if (currentRow == 1 && currentCol == 1)
            {
                this.Hide();
                this.OpenTaskView();
            }
        }

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        private const byte VK_LWIN = 0x5B;
        private const byte VK_TAB = 0x09;
        private const uint KEYEVENTF_KEYUP = 0x0002;

        public void OpenTaskView()
        {
            keybd_event(VK_LWIN, 0, 0, 0);
            keybd_event(VK_TAB, 0, 0, 0);
            keybd_event(VK_TAB, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_LWIN, 0, KEYEVENTF_KEYUP, 0);
        }

    }
}