namespace Remote_HID
{
    partial class Form_Icon
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
            listView_icon = new ListView();
            SuspendLayout();
            // 
            // listView_icon
            // 
            listView_icon.Location = new Point(12, 12);
            listView_icon.Name = "listView_icon";
            listView_icon.Size = new Size(438, 355);
            listView_icon.TabIndex = 0;
            listView_icon.UseCompatibleStateImageBehavior = false;
            listView_icon.SelectedIndexChanged += listView_icon_SelectedIndexChanged;
            // 
            // Form_Icon
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(462, 379);
            Controls.Add(listView_icon);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form_Icon";
            Text = "Icon";
            Load += Form_Icon_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListView listView_icon;
    }
}