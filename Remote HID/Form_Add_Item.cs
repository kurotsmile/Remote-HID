using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remote_HID
{
    public partial class Form_Add_Item : Form
    {
        public Form_Add_Item()
        {
            InitializeComponent();
        }

        private void btn_done_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_Icon frm_icon= new Form_Icon();
            frm_icon.Show();
        }
    }
}
