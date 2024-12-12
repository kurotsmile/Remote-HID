
using System.Diagnostics;
using System.Media;
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

        private ActionSystem actSys;
        private ActionSpeech actSpeech;

        string[,] actions = new string[,]
        {
                { "Tắt máy", "Khởi động lại", "Ngủ", "Powershell", "This Pc" },
                { "Start Menu", "Trình Đa nhiệm", "Cài đặt", "Game PS2", "Youtube" },
                { "Màn hình", "Thông báo", "Outlook Email", "Kết nối", "Bluetooth" },
                { "Bàn phím ảo", "Khẩu lệnh", "Tùy chọn", "Hướng dẫn", "Quản lý" },
                { "Thoát", "Khôi phục", "Cài đặt hệ thống", "Quản lý người dùng", "Đăng nhập" },
                { "Chạy ứng dụng", "Hẹn giờ", "Tải xuống", "Ghi chú", "Mở thư mục" },
                { "Chạy ứng dụng", "Hẹn giờ", "Tải xuống", "Ghi chú", "Mở thư mục" },
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
                Image.FromStream(new MemoryStream(Properties.Resources.setting)),
                Image.FromStream(new MemoryStream(Properties.Resources.game)),
                Image.FromStream(new MemoryStream(Properties.Resources.youtube))
            },
            {
                Image.FromStream(new MemoryStream(Properties.Resources.display)),
                Image.FromStream(new MemoryStream(Properties.Resources.notification)),
                Image.FromStream(new MemoryStream(Properties.Resources.email)),
                Image.FromStream(new MemoryStream(Properties.Resources.router)),
                Image.FromStream(new MemoryStream(Properties.Resources.bluetooth))
            },
            {
                Image.FromStream(new MemoryStream(Properties.Resources.keyboard)),
                Image.FromStream(new MemoryStream(Properties.Resources.mic_on)),
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
            InitializeGrid(8, 5);
            this.StartPosition = FormStartPosition.CenterScreen;
            GlobalKeyHook hook = new GlobalKeyHook();
            hook.Start(this);
            this.actSys = new ActionSystem();

            EffectBlur effectBlur = new EffectBlur();
            effectBlur.EnableBlur(this.Handle);

            this.actSpeech = new ActionSpeech();
            this.actSpeech.InitializeSpeechRecognition(this);
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
                        Font = new Font("Segoe UI", 15, FontStyle.Regular),
                        Tag = new Point(i, j),
                        FlatStyle = FlatStyle.Flat,
                        ForeColor = Color.Black,
                        Padding = new Padding(0),
                        Margin = new Padding(0)
                    };
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Image = icons[i, j];
                    btn.ImageAlign = ContentAlignment.TopCenter;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
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
                btn.FlatAppearance.BorderSize = 0;
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
            this.PlaySound(Properties.Resources.selectClick);
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
                this.actSys.OpenTaskView();
            }

            if (currentRow == 1 && currentCol == 2)
            {
                this.Hide();
                this.actSys.OpenSettings();
            }

            if (currentRow == 1 && currentCol == 3)
            {
                this.Hide();
                this.actSys.OpenProgram(@"J:\PCSX2\pcsx2-qt.exe");
            }

            if (currentRow == 1 && currentCol == 4)
            {
                this.Hide();
                this.actSys.OpenUrl("https://www.youtube.com/");
            }


            if (currentRow == 2 && currentCol == 0)
            {
                this.Hide();
                this.actSys.OpenProjectMenu();
            }

            if (currentRow == 2 && currentCol == 1)
            {
                this.Hide();
                this.actSys.OpenActionCenter();
            }

            if (currentRow == 2 && currentCol == 2)
            {
                this.Hide();
                this.actSys.OpenUrl("https://mail.google.com/");
            }

            if (currentRow == 2 && currentCol == 3)
            {
                this.Hide();
                this.actSys.OpenSettingWifi();
            }

            if (currentRow == 2 && currentCol == 4)
            {
                this.Hide();
                this.actSys.OpenSettingBluetooth();
            }

            if (currentRow == 3 && currentCol == 0)
            {
                this.Hide();
                this.actSys.OpenOnScreenKeyboard();
            }
            this.PlaySound(Properties.Resources.enterMenu);
        }

        public void PlaySound(byte[] data_sound)
        {
            using (MemoryStream stream = new MemoryStream(data_sound))
            {
                using (SoundPlayer player = new SoundPlayer(stream))
                {
                    player.Play();
                }
            }
        }

        public Button get_Btn_speech()
        {
            return buttons[3, 1];
        }

        public ActionSystem act_sys()
        {
            return this.actSys;
        }

    }
}
