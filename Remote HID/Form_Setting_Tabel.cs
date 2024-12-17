
namespace Remote_HID
{
    public partial class Form_Setting_Tabel : Form
    {
        private Form1 frm;
        private string s_name_font_family_cur = "";
        private float s_name_font_size_cur = 13f;
        private Font font_cur = null;

        public Form_Setting_Tabel(Form1 frm)
        {
            InitializeComponent();

            this.s_name_font_family_cur = frm.settings.font_family;
            this.s_name_font_size_cur= frm.settings.font_size;

            this.font_cur = new Font(new FontFamily(this.s_name_font_family_cur), this.s_name_font_size_cur, FontStyle.Regular);
            this.label_name_font_sel.Font = font_cur;
            
            this.frm = frm;
            this.txt_row.Text = frm.settings.row_length.ToString();
            this.txt_col.Text = frm.settings.col_length.ToString();
        }

        private void button_done_Click(object sender, EventArgs e)
        {
            this.frm.settings.font_family = this.s_name_font_family_cur;
            this.frm.settings.font_size = this.s_name_font_size_cur;
            this.frm.settings.row_length = int.Parse(this.txt_row.Text);
            this.frm.settings.col_length = int.Parse(this.txt_col.Text);
            this.frm.Update_table();
            this.frm.SaveSettings();
            this.Hide();
        }

        private void btn_sel_font_Click(object sender, EventArgs e)
        {
            this.fontDialog_label.Font = new Font(
              this.s_name_font_family_cur,
              this.s_name_font_size_cur,
              FontStyle.Regular
            );

            if (this.fontDialog_label.ShowDialog(this) == DialogResult.OK)
            {
                font_cur = this.fontDialog_label.Font;
                string nameFont = this.fontDialog_label.Font.FontFamily.Name;
                this.s_name_font_family_cur = this.fontDialog_label.Font.Name;
                this.s_name_font_size_cur = this.fontDialog_label.Font.Size;
                this.label_name_font_sel.Text = nameFont;
                this.label_name_font_sel.Font = font_cur;
            }
        }
    }
}
