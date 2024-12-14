
namespace Remote_HID
{
    public partial class Form_Icon : Form
    {
        private Form1 form1;
        private Form_Add_Item frm_add;

        public Form_Icon(Form1 f,Form_Add_Item frm_add)
        {
            this.form1 = f;
            this.frm_add = frm_add;
            InitializeComponent();
        }

        private void Form_Icon_Load(object sender, EventArgs e)
        {
            listView_icon.View = View.LargeIcon;
            listView_icon.FullRowSelect = true;

            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(24, 24);
            listView_icon.LargeImageList = imageList;

            for (int i = 0; i < this.form1.list_icon.Count; i++)
            {
                {
                    imageList.Images.Add(this.form1.list_icon[i]);
                    ListViewItem item = new ListViewItem(imageList.Images.Count.ToString());
                    item.ImageIndex = imageList.Images.Count - 1;
                    listView_icon.Items.Add(item);
                }
            }
            listView_icon.ItemActivate += listView_ico_ItemActivate;
        }

        private void listView_icon_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView_ico_ItemActivate(object sender, EventArgs e)
        {
            if (listView_icon.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView_icon.SelectedItems[0];
                this.frm_add.Set_Icon_view_by_index(selectedItem.Index);
                this.Hide();
            }
        }
    }
}
