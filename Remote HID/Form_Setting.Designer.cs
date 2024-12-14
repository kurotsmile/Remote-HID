namespace Remote_HID
{
    partial class Form_Setting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Setting));
            btn_cancel = new Button();
            listView1 = new ListView();
            btn_delete = new Button();
            btn_add = new Button();
            SuspendLayout();
            // 
            // btn_cancel
            // 
            btn_cancel.Image = Properties.Resources.close;
            btn_cancel.ImageAlign = ContentAlignment.MiddleLeft;
            btn_cancel.Location = new Point(318, 426);
            btn_cancel.Name = "btn_cancel";
            btn_cancel.Size = new Size(73, 36);
            btn_cancel.TabIndex = 0;
            btn_cancel.Text = "Close";
            btn_cancel.TextAlign = ContentAlignment.MiddleRight;
            btn_cancel.UseVisualStyleBackColor = true;
            btn_cancel.Click += btn_cancel_Click;
            // 
            // listView1
            // 
            listView1.Location = new Point(12, 12);
            listView1.Name = "listView1";
            listView1.Size = new Size(379, 408);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // btn_delete
            // 
            btn_delete.Image = Properties.Resources.trash;
            btn_delete.ImageAlign = ContentAlignment.MiddleLeft;
            btn_delete.Location = new Point(239, 426);
            btn_delete.Name = "btn_delete";
            btn_delete.Size = new Size(73, 36);
            btn_delete.TabIndex = 2;
            btn_delete.Text = "Delete";
            btn_delete.TextAlign = ContentAlignment.MiddleRight;
            btn_delete.UseVisualStyleBackColor = true;
            btn_delete.Click += btn_delete_Click;
            // 
            // btn_add
            // 
            btn_add.Image = Properties.Resources.add;
            btn_add.ImageAlign = ContentAlignment.MiddleLeft;
            btn_add.Location = new Point(12, 426);
            btn_add.Name = "btn_add";
            btn_add.Size = new Size(91, 36);
            btn_add.TabIndex = 3;
            btn_add.Text = "Add Item";
            btn_add.TextAlign = ContentAlignment.MiddleRight;
            btn_add.UseVisualStyleBackColor = true;
            btn_add.Click += btn_add_Click;
            // 
            // Form_Setting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(403, 474);
            Controls.Add(btn_add);
            Controls.Add(btn_delete);
            Controls.Add(listView1);
            Controls.Add(btn_cancel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form_Setting";
            Text = "Settings";
            Load += Form_Setting_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btn_cancel;
        private ListView listView1;
        private Button btn_delete;
        private Button btn_add;
    }
}