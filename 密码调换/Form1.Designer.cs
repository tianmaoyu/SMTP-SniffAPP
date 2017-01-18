namespace 密码调换
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
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_number = new System.Windows.Forms.CheckBox();
            this.checkBox_letter = new System.Windows.Forms.CheckBox();
            this.checkBox_repalce = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_replace = new System.Windows.Forms.TextBox();
            this.textBox_start = new System.Windows.Forms.TextBox();
            this.textBox_ned = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(89, 23);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(154, 21);
            this.textBox_password.TabIndex = 0;
            this.textBox_password.Text = "password.txt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "密码文本";
            // 
            // checkBox_number
            // 
            this.checkBox_number.AutoSize = true;
            this.checkBox_number.Location = new System.Drawing.Point(88, 69);
            this.checkBox_number.Name = "checkBox_number";
            this.checkBox_number.Size = new System.Drawing.Size(60, 16);
            this.checkBox_number.TabIndex = 2;
            this.checkBox_number.Text = "去数字";
            this.checkBox_number.UseVisualStyleBackColor = true;
            // 
            // checkBox_letter
            // 
            this.checkBox_letter.AutoSize = true;
            this.checkBox_letter.Location = new System.Drawing.Point(155, 69);
            this.checkBox_letter.Name = "checkBox_letter";
            this.checkBox_letter.Size = new System.Drawing.Size(60, 16);
            this.checkBox_letter.TabIndex = 3;
            this.checkBox_letter.Text = "去字母";
            this.checkBox_letter.UseVisualStyleBackColor = true;
            // 
            // checkBox_repalce
            // 
            this.checkBox_repalce.AutoSize = true;
            this.checkBox_repalce.Location = new System.Drawing.Point(88, 105);
            this.checkBox_repalce.Name = "checkBox_repalce";
            this.checkBox_repalce.Size = new System.Drawing.Size(96, 16);
            this.checkBox_repalce.TabIndex = 4;
            this.checkBox_repalce.Text = "数字字母互换";
            this.checkBox_repalce.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(89, 189);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_replace
            // 
            this.textBox_replace.Location = new System.Drawing.Point(141, 138);
            this.textBox_replace.Name = "textBox_replace";
            this.textBox_replace.Size = new System.Drawing.Size(106, 21);
            this.textBox_replace.TabIndex = 6;
            // 
            // textBox_start
            // 
            this.textBox_start.Location = new System.Drawing.Point(12, 138);
            this.textBox_start.Name = "textBox_start";
            this.textBox_start.Size = new System.Drawing.Size(35, 21);
            this.textBox_start.TabIndex = 6;
            // 
            // textBox_ned
            // 
            this.textBox_ned.Location = new System.Drawing.Point(72, 138);
            this.textBox_ned.Name = "textBox_ned";
            this.textBox_ned.Size = new System.Drawing.Size(35, 21);
            this.textBox_ned.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "换";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "---";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 232);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_ned);
            this.Controls.Add(this.textBox_start);
            this.Controls.Add(this.textBox_replace);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox_repalce);
            this.Controls.Add(this.checkBox_letter);
            this.Controls.Add(this.checkBox_number);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_password);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_number;
        private System.Windows.Forms.CheckBox checkBox_letter;
        private System.Windows.Forms.CheckBox checkBox_repalce;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_replace;
        private System.Windows.Forms.TextBox textBox_start;
        private System.Windows.Forms.TextBox textBox_ned;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

