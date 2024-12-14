namespace Remote_HID
{
    partial class Form_Setting_Tabel
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
            button_done = new Button();
            label1 = new Label();
            label2 = new Label();
            txt_row = new TextBox();
            txt_col = new TextBox();
            SuspendLayout();
            // 
            // button_done
            // 
            button_done.Location = new Point(12, 148);
            button_done.Name = "button_done";
            button_done.Size = new Size(354, 44);
            button_done.TabIndex = 0;
            button_done.Text = "Done";
            button_done.UseVisualStyleBackColor = true;
            button_done.Click += button_done_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 1;
            label1.Text = "Row";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 74);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 2;
            label2.Text = "Column";
            // 
            // txt_row
            // 
            txt_row.Location = new Point(14, 30);
            txt_row.Name = "txt_row";
            txt_row.Size = new Size(352, 23);
            txt_row.TabIndex = 3;
            // 
            // txt_col
            // 
            txt_col.Location = new Point(14, 92);
            txt_col.Name = "txt_col";
            txt_col.Size = new Size(352, 23);
            txt_col.TabIndex = 4;
            // 
            // Form_Setting_Tabel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(378, 214);
            Controls.Add(txt_col);
            Controls.Add(txt_row);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button_done);
            Name = "Form_Setting_Tabel";
            Text = "Table Setting";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_done;
        private Label label1;
        private Label label2;
        private TextBox txt_row;
        private TextBox txt_col;
    }
}