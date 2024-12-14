
namespace Remote_HID
{
    public partial class Form_Setting_Tabel : Form
    {
        private Form1 frm;
        public Form_Setting_Tabel(Form1 frm)
        {
            InitializeComponent();
            this.frm = frm;
            this.txt_row.Text = frm.settings.row_length.ToString();
            this.txt_col.Text=frm.settings.col_length.ToString();
        }

        private void button_done_Click(object sender, EventArgs e)
        {
            this.frm.settings.row_length = int.Parse(this.txt_row.Text);
            this.frm.settings.col_length = int.Parse(this.txt_col.Text);
            this.frm.Update_table();
            this.frm.SaveSettings();
            this.Hide();
        }
    }
}
