namespace Remote_HID
{
    public partial class Form_Setting : Form
    {
        private Form1 frm;
        public Form_Setting(Form1 frm)
        {
            InitializeComponent();
            this.frm = frm;
        }

        private void Form_Setting_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Name", 100);
            listView1.Columns.Add("cmd", 150);
            listView1.Columns.Add("Function", 100);

            this.Update_List();
        }

        public void Update_List()
        {
            ImageList imageList = new ImageList
            {
                ImageSize = new Size(16, 16)
            };

            for (int i = 0; i < this.frm.settings.actItems.Count; i++)
            {
                Act_item act_data=this.frm.settings.actItems[i];
                if (act_data.path_icon=="")
                    if(act_data.index_icon!=-1) imageList.Images.Add(this.frm.list_icon[act_data.index_icon]);
                else
                    imageList.Images.Add(Image.FromFile(act_data.path_icon));
            }

            listView1.SmallImageList = imageList;

            listView1.Items.Clear();
            for (int i = 0; i < this.frm.settings.actItems.Count; i++)
            {
                ListViewItem item1 = new ListViewItem(this.frm.settings.actItems[i].name, i);
                item1.SubItems.Add(this.frm.settings.actItems[i].cmd);
                item1.SubItems.Add(this.frm.settings.actItems[i].func);
                listView1.Items.Add(item1);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int selectedIndex = listView1.SelectedIndices[0];
                this.frm.settings.actItems.RemoveAt(selectedIndex);
                this.Update_List();
                this.frm.Update_table();
                this.frm.SaveSettings();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Form_Add_Item frm_add = new Form_Add_Item(this.frm,this);
            frm_add.Show();
        }
    }
}
