namespace 文本组装
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_email_file = new System.Windows.Forms.TextBox();
            this.label_email = new System.Windows.Forms.Label();
            this.textBox_password_file = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton_all = new System.Windows.Forms.RadioButton();
            this.radioButton_and_before = new System.Windows.Forms.RadioButton();
            this.textBox_host = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_start = new System.Windows.Forms.TextBox();
            this.textBox_end = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_class = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_email_file
            // 
            this.textBox_email_file.Location = new System.Drawing.Point(73, 18);
            this.textBox_email_file.Name = "textBox_email_file";
            this.textBox_email_file.Size = new System.Drawing.Size(199, 21);
            this.textBox_email_file.TabIndex = 0;
            this.textBox_email_file.Text = "email.txt";
            // 
            // label_email
            // 
            this.label_email.AutoSize = true;
            this.label_email.Location = new System.Drawing.Point(12, 21);
            this.label_email.Name = "label_email";
            this.label_email.Size = new System.Drawing.Size(53, 12);
            this.label_email.TabIndex = 1;
            this.label_email.Text = "邮件文件";
            // 
            // textBox_password_file
            // 
            this.textBox_password_file.Location = new System.Drawing.Point(73, 55);
            this.textBox_password_file.Name = "textBox_password_file";
            this.textBox_password_file.Size = new System.Drawing.Size(199, 21);
            this.textBox_password_file.TabIndex = 0;
            this.textBox_password_file.Text = "password.txt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "密码文件";
            // 
            // radioButton_all
            // 
            this.radioButton_all.AutoSize = true;
            this.radioButton_all.Location = new System.Drawing.Point(73, 186);
            this.radioButton_all.Name = "radioButton_all";
            this.radioButton_all.Size = new System.Drawing.Size(71, 16);
            this.radioButton_all.TabIndex = 2;
            this.radioButton_all.TabStop = true;
            this.radioButton_all.Text = "邮箱全部";
            this.radioButton_all.UseVisualStyleBackColor = true;
            // 
            // radioButton_and_before
            // 
            this.radioButton_and_before.AutoSize = true;
            this.radioButton_and_before.Location = new System.Drawing.Point(73, 164);
            this.radioButton_and_before.Name = "radioButton_and_before";
            this.radioButton_and_before.Size = new System.Drawing.Size(41, 16);
            this.radioButton_and_before.TabIndex = 2;
            this.radioButton_and_before.TabStop = true;
            this.radioButton_and_before.Text = "@前";
            this.radioButton_and_before.UseVisualStyleBackColor = true;
            // 
            // textBox_host
            // 
            this.textBox_host.Location = new System.Drawing.Point(73, 91);
            this.textBox_host.Name = "textBox_host";
            this.textBox_host.Size = new System.Drawing.Size(199, 21);
            this.textBox_host.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "服务器";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(162, 183);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "大写";
            // 
            // textBox_start
            // 
            this.textBox_start.Location = new System.Drawing.Point(74, 124);
            this.textBox_start.Name = "textBox_start";
            this.textBox_start.Size = new System.Drawing.Size(60, 21);
            this.textBox_start.TabIndex = 4;
            this.textBox_start.Text = "0";
            // 
            // textBox_end
            // 
            this.textBox_end.Location = new System.Drawing.Point(190, 125);
            this.textBox_end.Name = "textBox_end";
            this.textBox_end.Size = new System.Drawing.Size(67, 21);
            this.textBox_end.TabIndex = 4;
            this.textBox_end.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(138, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "-------";
            // 
            // btn_class
            // 
            this.btn_class.Location = new System.Drawing.Point(162, 153);
            this.btn_class.Name = "btn_class";
            this.btn_class.Size = new System.Drawing.Size(95, 23);
            this.btn_class.TabIndex = 5;
            this.btn_class.Text = "分类密码";
            this.btn_class.UseVisualStyleBackColor = true;
            this.btn_class.Click += new System.EventHandler(this.btn_class_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 226);
            this.Controls.Add(this.btn_class);
            this.Controls.Add(this.textBox_end);
            this.Controls.Add(this.textBox_start);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radioButton_and_before);
            this.Controls.Add(this.radioButton_all);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_email);
            this.Controls.Add(this.textBox_host);
            this.Controls.Add(this.textBox_password_file);
            this.Controls.Add(this.textBox_email_file);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_email_file;
        private System.Windows.Forms.Label label_email;
        private System.Windows.Forms.TextBox textBox_password_file;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton_all;
        private System.Windows.Forms.RadioButton radioButton_and_before;
        private System.Windows.Forms.TextBox textBox_host;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_start;
        private System.Windows.Forms.TextBox textBox_end;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_class;
    }
}

