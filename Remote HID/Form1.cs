
using System.Diagnostics;
using System.Media;
using Newtonsoft.Json;

namespace Remote_HID
{
    public class AppSettings
    {
        public int col = 0;
        public int row = 0;
    }

    public partial class Form1 : Form
    {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;

        private Button[,] buttons;
        private GlobalKeyHook hook;

        private ActionSystem actSys;
        private ActionSpeech actSpeech;
        private string settingsFile = "setting.json";
        AppSettings settings;
        string[,] actions = new string[,]
        {
            { "Shut down", "Restart", "Sleep", "Powershell", "Computer" },
            { "Start", "Multitasking", "Setting", "Game", "Youtube" },
            { "Screen", "Notification", "Email", "Wifi", "Bluetooth" },
            { "Keyboard", "Voice Command", "Mission", "Unity", "Device" },
            { "Notepad", "Calculator", "Translate", "Devloper", "Location" },
            { "Design", "Sound", "Art", "Recording", "Browser" },
            { "Chat", "Github", "Camera", "Webcam", "Store" },
            { "Book", "Write Book", "Document", "Amazon Book", "Store Book" }
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
                Image.FromStream(new MemoryStream(Properties.Resources.rank)),
                Image.FromStream(new MemoryStream(Properties.Resources.unity)),
                Image.FromStream(new MemoryStream(Properties.Resources.devices))
            },
            {
                Image.FromStream(new MemoryStream(Properties.Resources.notes)),
                Image.FromStream(new MemoryStream(Properties.Resources.calc)),
                Image.FromStream(new MemoryStream(Properties.Resources.translate)),
                Image.FromStream(new MemoryStream(Properties.Resources.programming)),
                Image.FromStream(new MemoryStream(Properties.Resources.location))
            },
            {
                Image.FromStream(new MemoryStream(Properties.Resources.image_editing)),
                Image.FromStream(new MemoryStream(Properties.Resources.speaker)),
                Image.FromStream(new MemoryStream(Properties.Resources.art)),
                Image.FromStream(new MemoryStream(Properties.Resources.recording)),
                Image.FromStream(new MemoryStream(Properties.Resources.website))
            },
            {
                Image.FromStream(new MemoryStream(Properties.Resources.freelancer)),
                Image.FromStream(new MemoryStream(Properties.Resources.github)),
                Image.FromStream(new MemoryStream(Properties.Resources.camera)),
                Image.FromStream(new MemoryStream(Properties.Resources.webcam)),
                Image.FromStream(new MemoryStream(Properties.Resources.store))
            },
            {
                Image.FromStream(new MemoryStream(Properties.Resources.bookshelf)),
                Image.FromStream(new MemoryStream(Properties.Resources.reading_book)),
                Image.FromStream(new MemoryStream(Properties.Resources.document)),
                Image.FromStream(new MemoryStream(Properties.Resources.amazon_book)),
                Image.FromStream(new MemoryStream(Properties.Resources.store_book))
            }
        };

        public Form1()
        {
            this.settings = File.Exists(settingsFile) ? LoadSettings() : new AppSettings();
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

            EffectBlur effectBlur = new EffectBlur();
            effectBlur.EnableBlur(this.Handle);

            this.Icon = new Icon(new MemoryStream(Properties.Resources.icon_app));
            this.KeyPreview = false;
            this.Load += (s, e) => { this.Hide(); };
            this.actSpeech = new ActionSpeech();
            InitializeGrid(8, 5);
            this.StartPosition = FormStartPosition.CenterScreen;
            GlobalKeyHook hook = new GlobalKeyHook();
            hook.Start(this);
            this.actSys = new ActionSystem();
            
        }

        private void ShowWindow(object sender, EventArgs e)
        {
            this.On_Show();
        }

        private void ExitApp(object sender, EventArgs e)
        {
            if(hook!=null) hook.Stop();
            notifyIcon.Visible = false;
            this.actSpeech.Stop();
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
            IList<string> list_cmd=new List<string>();

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
                    list_cmd.Add(actions[i, j].ToLower());
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Image = icons[i, j];
                    btn.ImageAlign = ContentAlignment.TopCenter;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.TextImageRelation = TextImageRelation.ImageAboveText;
                    buttons[i, j] = btn;
                    table.Controls.Add(btn, j, i);
                }
            }
            HighlightButton(this.settings.row, this.settings.col);
            this.Controls.Add(table);
            this.actSpeech.InitializeSpeechRecognition(this,list_cmd);
            this.actSpeech.Start();
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
            int newRow = this.settings.row, newCol = this.settings.col;
            switch (k)
            {
                case Keys.Up:
                    newRow = (this.settings.row - 1 + buttons.GetLength(0)) % buttons.GetLength(0);
                    break;
                case Keys.Down:
                    newRow = (this.settings.row + 1) % buttons.GetLength(0);
                    break;
                case Keys.Left:
                    newCol = (this.settings.col - 1 + buttons.GetLength(1)) % buttons.GetLength(1);
                    break;
                case Keys.Right:
                    newCol = (this.settings.col + 1) % buttons.GetLength(1);
                    break;
            }
            this.settings.row = newRow;
            this.settings.col = newCol;
            HighlightButton(this.settings.row, this.settings.col);
            this.PlaySound(Properties.Resources.selectClick);
            this.SaveSettings(this.settings);
        }

        public void Act_Menu()
        {
            if (this.settings.row == 0 && this.settings.col == 0) Process.Start("shutdown", "/s /f /t 0");
            if (this.settings.row == 0 && this.settings.col == 1) Process.Start("shutdown", "/r /f /t 0");
            if (this.settings.row == 0 && this.settings.col == 2) Process.Start("rundll32.exe", "powrprof.dll,SetSuspendState Sleep");
            if (this.settings.row == 0 && this.settings.col == 3) Process.Start("powershell.exe");
            if (this.settings.row == 0 && this.settings.col == 4)
            {
                this.Hide();
                Process.Start("explorer.exe", "shell:MyComputerFolder");
            }

            if (this.settings.row == 1 && this.settings.col == 0)
            {
                this.Hide();
                SendKeys.SendWait("^{ESC}");
            }

            if (this.settings.row == 1 && this.settings.col == 1)
            {
                this.Hide();
                this.actSys.OpenTaskView();
            }

            if (this.settings.row == 1 && this.settings.col == 2)
            {
                this.Hide();
                this.actSys.OpenSettings();
            }

            if (this.settings.row == 1 && this.settings.col == 3)
            {
                this.Hide();
                this.actSys.OpenProgram(@"J:\PCSX2\pcsx2-qt.exe");
            }

            if (this.settings.row == 1 && this.settings.col == 4)
            {
                this.Hide();
                this.actSys.OpenProgram("https://www.youtube.com/");
            }

            if (this.settings.row == 2 && this.settings.col == 0)
            {
                this.Hide();
                this.actSys.OpenProjectMenu();
            }

            if (this.settings.row == 2 && this.settings.col == 1)
            {

                this.Hide();
                this.actSys.OpenActionCenter();
            }

            if (this.settings.row == 2 && this.settings.col == 2)
            {
                this.Hide();
                this.actSys.OpenUrl("https://mail.google.com/");
            }

            if (this.settings.row == 2 && this.settings.col == 3)
            {
                this.Hide();
                this.actSys.OpenSettingWifi();
            }

            if (this.settings.row == 2 && this.settings.col == 4)
            {
                this.Hide();
                this.actSys.OpenSettingBluetooth();
            }

            if (this.settings.row == 3 && this.settings.col == 0)
            {
                this.Hide();
                this.actSys.OpenOnScreenKeyboard();
            }


            if (this.settings.row == 3 && this.settings.col == 2)
            {
                this.On_hide();
                this.actSys.OpenProgram(@"J:\Rewards_mission\start.bat");
            }

            if (this.settings.row == 3 && this.settings.col == 3)
            {
                this.On_hide();
                this.actSys.OpenProgram(@"D:\Unity3d\Unity Hub\Unity Hub.exe");
            }

            if (this.settings.row == 3 && this.settings.col == 4)
            {
                this.On_hide();
                this.actSys.OpenDeviceManager();
            }

            if (this.settings.row == 4 && this.settings.col == 0)
            {
                this.On_hide();
                this.actSys.OpenProgram(@"C:\Windows\System32\notepad.exe");
            }

            if (this.settings.row == 4 && this.settings.col == 1)
            {
                this.On_hide();
                this.actSys.OpenProgram(@"calc");
            }

            if (this.settings.row == 4 && this.settings.col == 2)
            {
                this.On_hide();
                this.actSys.OpenUrl("https://translate.google.com/?sl=vi&tl=en&op=translate");
            }

            if (this.settings.row == 4 && this.settings.col == 3)
            {
                this.On_hide();
                this.actSys.OpenProgram(@"C:\Users\trant\AppData\Local\Programs\Microsoft VS Code\Code.exe");
            }

            if (this.settings.row == 4 && this.settings.col == 4)
            {
                this.On_hide();
                this.actSys.OpenUrl(@"https://maps.google.com/");
            }

            if (this.settings.row == 5 && this.settings.col == 0)
            {
                this.On_hide();
                this.actSys.OpenProgram(@"J:\Program Files\a\RWPaint.exe");
            } 

            if (this.settings.row == 5 && this.settings.col == 1)
            {
                this.On_hide();
                this.actSys.OpenProgram(@"ms-settings:sound");
            }

            if (this.settings.row == 5 && this.settings.col == 2)
            {
                this.On_hide();
                this.actSys.OpenProgram(@"E:\Paint Tool SAI 2.0\sai2.exe");
            }

            if (this.settings.row == 5 && this.settings.col == 3)
            {
                this.On_hide();
                this.actSys.OpenProgram(@"J:\obs-studio\bin\64bit\obs64.exe");
            }

            if (this.settings.row == 5 && this.settings.col == 4)
            {
                this.On_hide();
                this.actSys.OpenUrl(@"https://www.google.com/");
            }

            if (this.settings.row == 6 && this.settings.col == 0)
            {
                this.On_hide();
                this.actSys.OpenUrl(@"https://chatgpt.com/");
            }

            if (this.settings.row == 6 && this.settings.col == 1)
            {
                this.On_hide();
                this.actSys.OpenUrl(@"https://github.com/kurotsmile");
            }

            if (this.settings.row == 6 && this.settings.col == 2)
            {
                this.On_hide();
                this.actSys.OpenProgram(@"microsoft.windows.camera:");
            }

            if (this.settings.row == 6 && this.settings.col == 3)
            {
                this.On_hide();
                this.actSys.OpenProgram(@"J:\Imou_en\bin\Imou_en.exe");
            }

            if (this.settings.row == 6 && this.settings.col == 4)
            {
                this.On_hide();
                this.actSys.OpenProgram(@"ms-windows-store:");
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

        void SaveSettings(AppSettings settings)
        {
            string json = JsonConvert.SerializeObject(settings, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(settingsFile, json);
        }

        AppSettings LoadSettings()
        {
            string json = File.ReadAllText(settingsFile);
            return JsonConvert.DeserializeObject<AppSettings>(json);
        }

        public void On_Show()
        {
            this.Show();
            this.actSpeech.Start();
        }

        public void On_hide()
        {
            this.Hide();
            this.actSpeech.Pause();
        }
    }
}
