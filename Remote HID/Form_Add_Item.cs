
using System;

namespace Remote_HID
{
    public partial class Form_Add_Item : Form
    {
        private Form1 frm;

        public Form_Add_Item(Form1 frm)
        {
            InitializeComponent();
            this.frm = frm;
        }

        private void btn_done_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_Icon frm_icon = new Form_Icon(this.frm, this);
            frm_icon.Show();
        }

        public void Set_Icon_view_by_index(int index)
        {
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
