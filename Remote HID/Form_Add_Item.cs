
using System;

namespace Remote_HID
{
    public partial class Form_Add_Item : Form
    {
        private Form1 frm;
        private Form_Setting frm_setting;
        private int index_sel_icon;
        public Form_Add_Item(Form1 frm,Form_Setting frm_setting)
        {
            this.index_sel_icon = -1;
            InitializeComponent();
            this.frm = frm;
            this.frm_setting = frm_setting;
        }

        private void btn_done_Click(object sender, EventArgs e)
        {
            Act_item act_item = new Act_item();
            act_item.name = this.txt_name.Text;
            act_item.func = this.comboBox_func.Text;
            act_item.func = this.txt_val.Text;
            act_item.path_icon=this.txt_icon_path.Text;
            act_item.index_icon = this.index_sel_icon;
            this.frm.settings.actItems.Add(act_item);
            this.frm.Update_table();
            MessageBox.Show("Add item menu success!");
            this.Hide();
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
                this.picture_icon_view.Image =Image.FromFile(filePath);
            }
        }
    }
}
