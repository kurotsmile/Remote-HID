namespace Remote_HID
{
    partial class Form_Add_Item
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
            btn_done = new Button();
            label_name = new Label();
            txt_name = new TextBox();
            Label_func = new Label();
            comboBox_func = new ComboBox();
            txt_val = new TextBox();
            label1 = new Label();
            openFileDialog1 = new OpenFileDialog();
            label2 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            label3 = new Label();
            pictureBox1 = new PictureBox();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btn_done
            // 
            btn_done.Location = new Point(12, 348);
            btn_done.Name = "btn_done";
            btn_done.Size = new Size(368, 45);
            btn_done.TabIndex = 0;
            btn_done.Text = "Done";
            btn_done.UseVisualStyleBackColor = true;
            btn_done.Click += btn_done_Click;
            // 
            // label_name
            // 
            label_name.AutoSize = true;
            label_name.Location = new Point(12, 19);
            label_name.Name = "label_name";
            label_name.Size = new Size(39, 15);
            label_name.TabIndex = 1;
            label_name.Text = "Name";
            // 
            // txt_name
            // 
            txt_name.Location = new Point(12, 37);
            txt_name.Name = "txt_name";
            txt_name.Size = new Size(368, 23);
            txt_name.TabIndex = 2;
            // 
            // Label_func
            // 
            Label_func.AutoSize = true;
            Label_func.Location = new Point(12, 72);
            Label_func.Name = "Label_func";
            Label_func.Size = new Size(54, 15);
            Label_func.TabIndex = 3;
            Label_func.Text = "Function";
            // 
            // comboBox_func
            // 
            comboBox_func.FormattingEnabled = true;
            comboBox_func.Items.AddRange(new object[] { "Open Program and url", "sendkey", "OpenTaskView", "OpenProjectMenu", "OpenActionCenter", "NextMedia", "PreviousMedia", "FastForwardMedia", "FastRewindMedia", "PlayPauseMedia", "Change_status_voice_command" });
            comboBox_func.Location = new Point(12, 90);
            comboBox_func.Name = "comboBox_func";
            comboBox_func.Size = new Size(368, 23);
            comboBox_func.TabIndex = 4;
            comboBox_func.Text = "Open Program and Url";
            // 
            // txt_val
            // 
            txt_val.Location = new Point(12, 148);
            txt_val.Name = "txt_val";
            txt_val.Size = new Size(368, 23);
            txt_val.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 130);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 6;
            label1.Text = "Value";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 191);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 7;
            label2.Text = "Icon Path";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 209);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(299, 23);
            textBox1.TabIndex = 8;
            // 
            // button1
            // 
            button1.Location = new Point(317, 209);
            button1.Name = "button1";
            button1.Size = new Size(63, 23);
            button1.TabIndex = 9;
            button1.Text = "Done";
            button1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 252);
            label3.Name = "label3";
            label3.Size = new Size(87, 15);
            label3.TabIndex = 10;
            label3.Text = "Icon Collection";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(16, 272);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(70, 70);
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // button2
            // 
            button2.Location = new Point(317, 272);
            button2.Name = "button2";
            button2.Size = new Size(63, 70);
            button2.TabIndex = 12;
            button2.Text = "Select Icon";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form_Add_Item
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(392, 405);
            Controls.Add(button2);
            Controls.Add(pictureBox1);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txt_val);
            Controls.Add(comboBox_func);
            Controls.Add(Label_func);
            Controls.Add(txt_name);
            Controls.Add(label_name);
            Controls.Add(btn_done);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form_Add_Item";
            Text = "Add Menu Item";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_done;
        private Label label_name;
        private TextBox txt_name;
        private Label Label_func;
        private ComboBox comboBox_func;
        private TextBox txt_val;
        private Label label1;
        private OpenFileDialog openFileDialog1;
        private Label label2;
        private TextBox textBox1;
        private Button button1;
        private Label label3;
        private PictureBox pictureBox1;
        private Button button2;
    }
}