
using System.Media;
using Newtonsoft.Json;

namespace Remote_HID
{
    public class AppSettings
    {
        public int col = 0;
        public int row = 0;
        public int sound = 1;
        public int voice_command = 1;
    }

    public class Act_item
    {
        public string name = "";
        public string cmd = "";
        public string func = "open_program";
        public int col = 0;
        public int row = 0;
        public Image icon;
        public int state = 0;
    }

    public partial class Form1 : Form
    {
        private GlobalKeyHook hook;
        private NotifyApp notify;

        private Button[,] buttons;
        private Button btn_voice;
        

        private ActionSystem actSys;
        public ActionSpeech actSpeech;
        private string settingsFile = "setting.json";
        public AppSettings settings;

        private IList<Act_item> actItems;

        public Form1()
        {
            this.actItems = new List<Act_item>();

            this.actItems.Add(new Act_item { name = "Shut down", cmd= "shutdown /s /f /t 0", icon= Image.FromStream(new MemoryStream(Properties.Resources.power_off))});
            this.actItems.Add(new Act_item { name = "Restart", cmd = "shutdown /r /f /t 0", icon = Image.FromStream(new MemoryStream(Properties.Resources.reset))});
            this.actItems.Add(new Act_item { name = "Sleep", cmd = "rundll32.exe powrprof.dll,SetSuspendState Sleep", icon = Image.FromStream(new MemoryStream(Properties.Resources.sleep))});
            this.actItems.Add(new Act_item { name = "Powershell", cmd = "powershell.exe", icon = Image.FromStream(new MemoryStream(Properties.Resources.powershell))});
            this.actItems.Add(new Act_item { name = "Computer", cmd = "explorer.exe", icon = Image.FromStream(new MemoryStream(Properties.Resources.computer))});
            this.actItems.Add(new Act_item { name = "Start", cmd = "^{ESC}",func="sendkey", icon = Image.FromStream(new MemoryStream(Properties.Resources.start_menu))});
            this.actItems.Add(new Act_item { name = "Multitasking",func = "OpenTaskView", icon = Image.FromStream(new MemoryStream(Properties.Resources.apps))});
            this.actItems.Add(new Act_item { name = "Setting", cmd = "ms-settings:", icon = Image.FromStream(new MemoryStream(Properties.Resources.setting))});
            this.actItems.Add(new Act_item { name = "Game", cmd = @"J:\PCSX2\pcsx2-qt.exe", icon = Image.FromStream(new MemoryStream(Properties.Resources.game))});
            this.actItems.Add(new Act_item { name = "Youtube", cmd = "https://www.youtube.com", icon = Image.FromStream(new MemoryStream(Properties.Resources.youtube))});
            this.actItems.Add(new Act_item { name = "Screen", func = "OpenProjectMenu", icon = Image.FromStream(new MemoryStream(Properties.Resources.display))});
            this.actItems.Add(new Act_item { name = "Notification", func = "OpenActionCenter", icon = Image.FromStream(new MemoryStream(Properties.Resources.notification))});
            this.actItems.Add(new Act_item { name = "Email", cmd = "https://mail.google.com/", icon = Image.FromStream(new MemoryStream(Properties.Resources.email))});
            this.actItems.Add(new Act_item { name = "Wifi", cmd = "ms-settings:network-wifi", icon = Image.FromStream(new MemoryStream(Properties.Resources.router))});
            this.actItems.Add(new Act_item { name = "Bluetooth", cmd = "settings:bluetooth", icon = Image.FromStream(new MemoryStream(Properties.Resources.bluetooth))});
            this.actItems.Add(new Act_item { name = "Keyboard", cmd = "osk.exe", icon = Image.FromStream(new MemoryStream(Properties.Resources.keyboard))});
            this.actItems.Add(new Act_item { name = "Command", func = "Change_status_voice_command",state=1, icon = Image.FromStream(new MemoryStream(Properties.Resources.mic_on))});
            this.actItems.Add(new Act_item { name = "Mission", cmd = @"J:\Rewards_mission\start.bat", icon = Image.FromStream(new MemoryStream(Properties.Resources.rank))});
            this.actItems.Add(new Act_item { name = "Unity", cmd = @"D:\Unity3d\Unity Hub\Unity Hub.exe", icon = Image.FromStream(new MemoryStream(Properties.Resources.unity))});
            this.actItems.Add(new Act_item { name = "Device", cmd = "devmgmt.msc", icon = Image.FromStream(new MemoryStream(Properties.Resources.devices))});
            this.actItems.Add(new Act_item { name = "Note", cmd = @"C:\Windows\System32\notepad.exe", icon = Image.FromStream(new MemoryStream(Properties.Resources.notes))});
            this.actItems.Add(new Act_item { name = "Calculator", cmd = "calc", icon = Image.FromStream(new MemoryStream(Properties.Resources.calc))});
            this.actItems.Add(new Act_item { name = "Translate", cmd = "https://translate.google.com/?sl=vi&tl=en&op=translate", icon = Image.FromStream(new MemoryStream(Properties.Resources.translate))});
            this.actItems.Add(new Act_item { name = "Devloper", cmd = "code", icon = Image.FromStream(new MemoryStream(Properties.Resources.programming))});
            this.actItems.Add(new Act_item { name = "Location", cmd = "https://maps.google.com/", icon = Image.FromStream(new MemoryStream(Properties.Resources.location))});
            this.actItems.Add(new Act_item { name = "Design", cmd = @"J:\Program Files\a\RWPaint.exe", icon = Image.FromStream(new MemoryStream(Properties.Resources.image_editing))});
            this.actItems.Add(new Act_item { name = "Sound", cmd = "ms-settings:sound", icon = Image.FromStream(new MemoryStream(Properties.Resources.speaker))});
            this.actItems.Add(new Act_item { name = "Art", cmd = @"E:\Paint Tool SAI 2.0\sai2.exe", icon = Image.FromStream(new MemoryStream(Properties.Resources.art))});
            this.actItems.Add(new Act_item { name = "Recording", cmd = @"J:\obs-studio\bin\64bit\obs64.exe", icon = Image.FromStream(new MemoryStream(Properties.Resources.recording))});
            this.actItems.Add(new Act_item { name = "Browser", cmd = "https://www.google.com/", icon = Image.FromStream(new MemoryStream(Properties.Resources.website))});
            this.actItems.Add(new Act_item { name = "Chat", cmd = "https://chatgpt.com/", icon = Image.FromStream(new MemoryStream(Properties.Resources.freelancer))});
            this.actItems.Add(new Act_item { name = "Github", cmd = "https://github.com/kurotsmile", icon = Image.FromStream(new MemoryStream(Properties.Resources.github))});
            this.actItems.Add(new Act_item { name = "Camera", cmd = "microsoft.windows.camera:", icon = Image.FromStream(new MemoryStream(Properties.Resources.camera))});
            this.actItems.Add(new Act_item { name = "Webcam", cmd = @"J:\Imou_en\bin\Imou_en.exe", icon = Image.FromStream(new MemoryStream(Properties.Resources.webcam))});
            this.actItems.Add(new Act_item { name = "Store", cmd = "ms-windows-store:", icon = Image.FromStream(new MemoryStream(Properties.Resources.store))});
            this.actItems.Add(new Act_item { name = "Book", cmd = @"G:\Ebook", icon = Image.FromStream(new MemoryStream(Properties.Resources.bookshelf))});
            this.actItems.Add(new Act_item { name = "Write Book", cmd = @"G:\Kindle Create\Kindle Create.exe", icon = Image.FromStream(new MemoryStream(Properties.Resources.reading_book))});
            this.actItems.Add(new Act_item { name = "Document", cmd = "https://drive.google.com/drive/u/2/folders/1xcifsnQF-bQGystpkjphrRUlWFPPhkMw", icon = Image.FromStream(new MemoryStream(Properties.Resources.document))});
            this.actItems.Add(new Act_item { name = "Amazon Book", cmd = "https://kdp.amazon.com/en_US/bookshelf", icon = Image.FromStream(new MemoryStream(Properties.Resources.amazon_book))});
            this.actItems.Add(new Act_item { name = "Store Book", cmd = "https://www.amazon.com/s?i=digital-text&rh=p_27%3AThien+Thanh+Tran", icon = Image.FromStream(new MemoryStream(Properties.Resources.store_book))});
            this.actItems.Add(new Act_item { name = "Play", func = "PlayPauseMedia", state = 1, icon = Image.FromStream(new MemoryStream(Properties.Resources.play))});
            this.actItems.Add(new Act_item { name = "Previous", func= "PreviousMedia", state=1, icon = Image.FromStream(new MemoryStream(Properties.Resources.previous))});
            this.actItems.Add(new Act_item { name = "Next", func = "NextMedia", state = 1, icon = Image.FromStream(new MemoryStream(Properties.Resources.next))});
            this.actItems.Add(new Act_item { name = "Fast backward", func = "FastRewindMedia", state = 1, icon = Image.FromStream(new MemoryStream(Properties.Resources.fast_backward))});
            this.actItems.Add(new Act_item { name = "Fast forward", func = "FastForwardMedia", state = 1, icon = Image.FromStream(new MemoryStream(Properties.Resources.fast_forward))});
            this.actItems.Add(new Act_item { name = "Task Manager", cmd = "taskmgr", icon = Image.FromStream(new MemoryStream(Properties.Resources.task_manager))});
            this.actItems.Add(new Act_item { name = "Infomation", cmd = "msinfo32", icon = Image.FromStream(new MemoryStream(Properties.Resources.info))});
            this.actItems.Add(new Act_item { name = "Uninstall", cmd = "ms-settings:appsfeatures", icon = Image.FromStream(new MemoryStream(Properties.Resources.uninstall))});
            this.actItems.Add(new Act_item { name = "Mouse", cmd = "main.cpl", icon = Image.FromStream(new MemoryStream(Properties.Resources.mouse))});
            this.actItems.Add(new Act_item { name = "Service", cmd = "services.msc", icon = Image.FromStream(new MemoryStream(Properties.Resources.service))});
            this.actItems.Add(new Act_item { name = "Update", cmd = "ms-settings:windowsupdate", icon = Image.FromStream(new MemoryStream(Properties.Resources.update))});
            this.actItems.Add(new Act_item { name = "Firewall", cmd = "wf.msc", icon = Image.FromStream(new MemoryStream(Properties.Resources.firewall))});
            this.actItems.Add(new Act_item { name = "web", cmd = "msedge", icon = Image.FromStream(new MemoryStream(Properties.Resources.international))});
            this.actItems.Add(new Act_item { name = "Clear", cmd = "cleanmgr", icon = Image.FromStream(new MemoryStream(Properties.Resources.clear))});
            this.actItems.Add(new Act_item { name = "Disk", cmd = "diskmgmt.msc", icon = Image.FromStream(new MemoryStream(Properties.Resources.ssd))});
            this.actItems.Add(new Act_item { name = "Font", cmd = "control fonts", icon = Image.FromStream(new MemoryStream(Properties.Resources.font))});
            this.actItems.Add(new Act_item { name = "Zoom", cmd = "magnify", icon = Image.FromStream(new MemoryStream(Properties.Resources.zoom))});
            this.actItems.Add(new Act_item { name = "Music", cmd = "msmusic:", icon = Image.FromStream(new MemoryStream(Properties.Resources.music))});
            this.actItems.Add(new Act_item { name = "Telegram", cmd = "https://web.telegram.org/k/", icon = Image.FromStream(new MemoryStream(Properties.Resources.live_chat))});
            this.actItems.Add(new Act_item { name = "Tool", cmd = "notepad++", icon = Image.FromStream(new MemoryStream(Properties.Resources.gardening))});
            this.actItems.Add(new Act_item { name = "Regedit", cmd = "regedit", icon = Image.FromStream(new MemoryStream(Properties.Resources.register))});
            this.actItems.Add(new Act_item { name = "cmd", cmd = "cmd", icon = Image.FromStream(new MemoryStream(Properties.Resources.cmd))});
            this.actItems.Add(new Act_item { name = "Photo", cmd = "ms-photos:", icon = Image.FromStream(new MemoryStream(Properties.Resources.photo))});
            this.actItems.Add(new Act_item { name = "Screenshot", cmd = "snippingtool", icon = Image.FromStream(new MemoryStream(Properties.Resources.screenshot)) });

            this.settings = File.Exists(settingsFile) ? LoadSettings() : new AppSettings();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.BackColor = Color.Black;
            this.TransparencyKey = this.BackColor;
            this.TopMost = true;

            EffectBlur effectBlur = new EffectBlur();
            effectBlur.EnableBlur(this.Handle);

            this.Icon = new Icon(new MemoryStream(Properties.Resources.icon_app));
            this.KeyPreview = false;
            this.Load += (s, e) => { this.Hide(); };
            this.actSpeech = new ActionSpeech();
            InitializeGrid(8, 10);
            this.StartPosition = FormStartPosition.CenterScreen;
            GlobalKeyHook hook = new GlobalKeyHook(this);
            hook.Start();
            //this.FormClosed += (s, e) => hook.Dispose();
            this.actSys = new ActionSystem(this);
            this.notify = new NotifyApp(this);
        }

        public void ShowWindow(object sender, EventArgs e)
        {
            this.On_Show();
        }

        public void ExitApp(object sender, EventArgs e)
        {
            if(hook!=null) hook.Stop();
            this.notify.Stop();
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

            int count_btn_act = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Act_item act_data = null;
                    if (count_btn_act<this.actItems.Count)
                    {
                        act_data = this.actItems[count_btn_act];
                        act_data.row= i;
                        act_data.col = j;
                        list_cmd.Add(act_data.name.ToLower());
                    }
                    else
                    {
                        act_data = new Act_item();
                    }

                    Button btn = new Button
                    {
                        Text = act_data.name,
                        Dock = DockStyle.Fill,
                        BackColor = Color.Blue,
                        Font = new Font("Segoe UI", 15, FontStyle.Regular),
                        Tag = new Point(i, j),
                        FlatStyle = FlatStyle.Flat,
                        ForeColor = Color.Black
                    };
                    if (act_data.name == "Command")
                    {
                        this.btn_voice = btn;
                        this.Check_status_voice_command();
                    }
                    else
                    {
                        btn.Image = act_data.icon;
                    }
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.BorderColor = Color.White;
                    btn.FlatAppearance.MouseOverBackColor = Color.White;
                    btn.FlatAppearance.MouseDownBackColor = Color.White;
                    btn.Cursor = Cursors.Hand;
                    btn.ImageAlign = ContentAlignment.MiddleCenter;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.TextImageRelation = TextImageRelation.ImageAboveText;
                    btn.Click += (sender, e) =>
                    {
                        this.PlaySound(Properties.Resources.enterMenu);
                        this.RunCmd(act_data);
                    };
                    buttons[i, j] = btn;
                    table.Controls.Add(btn, j, i);
                    count_btn_act++;
                }
            }
            HighlightButton(this.settings.row, this.settings.col);
            this.Controls.Add(table);
            this.actSpeech.InitializeSpeechRecognition(this,list_cmd);
        }

        private void HighlightButton(int row, int col)
        {
            foreach (var btn in buttons)
            {
                btn.BackColor = Color.FromArgb(00, 30, 40, 20);
                btn.ForeColor = Color.FromArgb(60, 30, 40,20);
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
            this.Act_by_Position(this.settings.row,this.settings.col);
            this.PlaySound(Properties.Resources.enterMenu);
        }

        public void PlaySound(byte[] data_sound)
        {
            if (this.settings.sound == 1)
            {
                using (MemoryStream stream = new MemoryStream(data_sound))
                {
                    using (SoundPlayer player = new SoundPlayer(stream))
                    {
                        player.Play();
                    }
                }
            }
        }

        public Button get_Btn_speech()
        {
            return btn_voice;
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

        public void SaveSettings()
        {
            this.SaveSettings(this.settings);
        }

        AppSettings LoadSettings()
        {
            string json = File.ReadAllText(settingsFile);
            return JsonConvert.DeserializeObject<AppSettings>(json);
        }

        public void On_Show()
        {
            this.Show();
            if(this.settings.voice_command==1) this.actSpeech.Start();
            this.Focus();
        }

        public void On_hide()
        {
            this.Hide();
            this.actSpeech.Pause();
        }

        public void Act_by_Position(int row,int col)
        {
            for(int i = 0; i < actItems.Count; i++)
            {
                if(actItems[i].row == row && actItems[i].col == col) this.RunCmd(actItems[i]);
            }
        }

        public void Act_by_keyword(string keyword)
        {
            for (int i = 0; i < actItems.Count; i++)
            {
                if (actItems[i].name.ToLower()==keyword) this.RunCmd(actItems[i]);
            }
            this.PlaySound(Properties.Resources.enterMenu);
        }

        public void RunCmd(Act_item act_data)
        {
            if (act_data != null)
            {
                if (act_data.func == "OpenTaskView")
                    this.actSys.OpenTaskView();
                else if (act_data.func == "OpenProjectMenu")
                    this.actSys.OpenProjectMenu();
                else if (act_data.func == "OpenActionCenter")
                    this.actSys.OpenActionCenter();
                else if (act_data.func == "NextMedia")
                    this.actSys.NextMedia();
                else if (act_data.func == "PreviousMedia")
                    this.actSys.PreviousMedia();
                else if (act_data.func == "FastForwardMedia")
                    this.actSys.FastForwardMedia();
                else if (act_data.func == "FastRewindMedia")
                    this.actSys.FastRewindMedia();
                else if (act_data.func == "PlayPauseMedia")
                    this.actSys.PlayPauseMedia();
                else if (act_data.func == "Change_status_voice_command")
                    this.Change_status_voice_command();
                else if (act_data.func == "sendkey")
                    SendKeys.SendWait(act_data.cmd);
                else
                    this.actSys.OpenProgram(act_data.cmd);

                if (act_data.state == 0) this.On_hide();
            }
        }

        public void Check_status_voice_command()
        {
            if (this.settings.voice_command == 0)
                this.btn_voice.Image = Image.FromStream(new MemoryStream(Properties.Resources.mic_off));
            else
                this.btn_voice.Image = Image.FromStream(new MemoryStream(Properties.Resources.mic_on));
        }

        public void Change_status_voice_command()
        {
            if (this.settings.voice_command == 0)
            {
                this.settings.voice_command = 1;
            }
            else
            {
                this.settings.voice_command = 0;
                this.actSpeech.Pause();
            }
            this.SaveSettings();
            this.notify.Update_Menu();
            this.Check_status_voice_command();
        }
    }
}
