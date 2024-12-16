namespace Remote_HID
{
    public partial class Form_Add_Item : Form
    {
        private Form1 frm;
        private Form_Setting frm_setting;
        private int index_sel_icon;
        public Form_Add_Item(Form1 frm, Form_Setting frm_setting)
        {
            this.index_sel_icon = -1;
            InitializeComponent();
            this.frm = frm;
            this.frm_setting = frm_setting;
        }

        private void btn_done_Click(object sender, EventArgs e)
        {
            if (this.txt_name.Text.Trim() == "")
            {
                MessageBox.Show("Name cannot be blank!", "Add Item Menu");
                return;
            }
            Act_item act_item = new Act_item();
            act_item.name = this.txt_name.Text;
            act_item.func = this.comboBox_func.Text;
            act_item.cmd = this.txt_val.Text;
            act_item.path_icon = this.txt_icon_path.Text;
            act_item.index_icon = this.index_sel_icon;
            this.Hide();
            this.frm.settings.actItems.Add(act_item);
            this.frm_setting.Update_List();
            this.frm.SaveSettings();
            this.frm.Update_table();
            MessageBox.Show("Add item menu success!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_Icon frm_icon = new Form_Icon(this.frm, this);
            frm_icon.Show();
        }

        public void Set_Icon_view_by_index(int index)
        {
            this.index_sel_icon = index;
            this.picture_icon_view.Image = this.frm.list_icon[index];
        }

        private void btn_select_file_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog_icon.ShowDialog(this) == DialogResult.OK)
            {
                string filePath = this.openFileDialog_icon.FileName;
                txt_icon_path.Text = filePath;
                Image originalImage = Image.FromFile(filePath);
                Image resizedImage = originalImage.GetThumbnailImage(50, 50, null, IntPtr.Zero);
                this.picture_icon_view.Image = resizedImage;
            }
        }

        private void picture_icon_view_Click(object sender, EventArgs e)
        {
            btn_select_file_Click(sender, e);
        }

        private void btn_select_file_program_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog_icon.ShowDialog(this) == DialogResult.OK)
            {
                string filePath = this.openFileDialog_icon.FileName;
                this.txt_val.Text = filePath;
            }
        }

        private void btn_sel_folder_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                string filePath = this.folderBrowserDialog.SelectedPath;
                this.txt_val.Text = filePath;
            }
        }
    }
}
