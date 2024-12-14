using System.Windows.Forms;

namespace Remote_HID
{
    public class NotifyApp
    {
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private Form1 form1;

        public NotifyApp(Form1 form1)
        {
            this.form1 = form1;
            notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Text = "App Runging...";
            notifyIcon.Icon = new Icon(new MemoryStream(Properties.Resources.icon_app));
            this.Update_Menu();
        }

        public void Update_Menu()
        {
            contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.Add("Hiện cửa sổ", null, this.form1.ShowWindow);
            if(this.form1.settings.sound==0)
                contextMenuStrip.Items.Add("Bật âm thanh thao tác", null, Change_status_sound_action);
            else
                contextMenuStrip.Items.Add("Tắt âm thanh thao tác", null, Change_status_sound_action);

            if(this.form1.settings.voice_command==0)
                contextMenuStrip.Items.Add("Bật khẩu lệnh", null, Change_status_voice_command);
            else
                contextMenuStrip.Items.Add("Tắt khẩu lệnh", null, Change_status_voice_command);

            contextMenuStrip.Items.Add(new ToolStripSeparator());
            contextMenuStrip.Items.Add("Cài đặt Menu", null, Show_setting);
            contextMenuStrip.Items.Add("Cài đặt Tabel", null, Show_setting_table);
            contextMenuStrip.Items.Add(new ToolStripSeparator());

            contextMenuStrip.Items.Add("Thoát", null, this.form1.ExitApp);
            notifyIcon.ContextMenuStrip = contextMenuStrip;
        }

        public void Change_status_sound_action(object sender, EventArgs e)
        {
            if (this.form1.settings.sound == 0)
                this.form1.settings.sound = 1;
            else
                this.form1.settings.sound = 0;
            this.form1.SaveSettings();
            this.Update_Menu();
        }

        public void Change_status_voice_command(object sender, EventArgs e)
        {
            this.form1.Change_status_voice_command();
        }

        public void Stop()
        {
            notifyIcon.Visible = false;
        }

        public void Show_setting(object sender, EventArgs e)
        {
            Form_Setting fs = new Form_Setting(this.form1);
            fs.Show();
        }

        public void Show_setting_table(object sender, EventArgs e)
        {
            Form_Setting_Tabel ft = new Form_Setting_Tabel(this.form1);
            ft.Show();
        }
    }
}
