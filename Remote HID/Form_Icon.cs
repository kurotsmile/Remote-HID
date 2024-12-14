using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Remote_HID
{
    public partial class Form_Icon : Form
    {
        public Form_Icon()
        {
            InitializeComponent();
        }

        private void Form_Icon_Load(object sender, EventArgs e)
        {
            // Thiết lập ListView
            listView1.View = View.LargeIcon; // Hiển thị theo dạng biểu tượng lớn
            listView1.FullRowSelect = true;

            // Tạo ImageList
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(64, 64); // Kích thước hình ảnh
            listView1.LargeImageList = imageList;

            // Duyệt qua các hình ảnh trong Resources
            foreach (System.Drawing.Bitmap image in Properties.Resources.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, true))
            {
                // Thêm hình ảnh vào ImageList
                imageList.Images.Add(image);

                // Thêm mục vào ListView (dựa trên tên hình ảnh)
                ListViewItem item = new ListViewItem(imageList.Images.Count.ToString());
                item.ImageIndex = imageList.Images.Count - 1; // Gán chỉ số ảnh
                listView1.Items.Add(item);
            }
        }
    }
}
