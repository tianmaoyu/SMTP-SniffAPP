namespace 名字生成器
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_surname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_word = new System.Windows.Forms.TextBox();
            this.checkBox_two = new System.Windows.Forms.CheckBox();
            this.checkBox_three = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_reaplec = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "姓";
            // 
            // textBox_surname
            // 
            this.textBox_surname.Location = new System.Drawing.Point(93, 31);
            this.textBox_surname.Name = "textBox_surname";
            this.textBox_surname.Size = new System.Drawing.Size(152, 21);
            this.textBox_surname.TabIndex = 1;
            this.textBox_surname.Text = "姓.txt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "字";
            // 
            // textBox_word
            // 
            this.textBox_word.Location = new System.Drawing.Point(93, 66);
            this.textBox_word.Name = "textBox_word";
            this.textBox_word.Size = new System.Drawing.Size(152, 21);
            this.textBox_word.TabIndex = 1;
            this.textBox_word.Text = "字.txt";
            // 
            // checkBox_two
            // 
            this.checkBox_two.AutoSize = true;
            this.checkBox_two.Location = new System.Drawing.Point(93, 127);
            this.checkBox_two.Name = "checkBox_two";
            this.checkBox_two.Size = new System.Drawing.Size(48, 16);
            this.checkBox_two.TabIndex = 2;
            this.checkBox_two.Text = "两个";
            this.checkBox_two.UseVisualStyleBackColor = true;
            this.checkBox_two.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox_three
            // 
            this.checkBox_three.AutoSize = true;
            this.checkBox_three.Location = new System.Drawing.Point(144, 127);
            this.checkBox_three.Name = "checkBox_three";
            this.checkBox_three.Size = new System.Drawing.Size(48, 16);
            this.checkBox_three.TabIndex = 2;
            this.checkBox_three.Text = "三个";
            this.checkBox_three.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(169, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "生成";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_reaplec
            // 
            this.textBox_reaplec.Location = new System.Drawing.Point(65, 20);
            this.textBox_reaplec.Name = "textBox_reaplec";
            this.textBox_reaplec.Size = new System.Drawing.Size(151, 21);
            this.textBox_reaplec.TabIndex = 4;
            this.textBox_reaplec.Text = "字.txt";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.textBox_reaplec);
            this.groupBox1.Location = new System.Drawing.Point(23, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 94);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "去重复";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(139, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "确定";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 319);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox_three);
            this.Controls.Add(this.checkBox_two);
            this.Controls.Add(this.textBox_word);
            this.Controls.Add(this.textBox_surname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_surname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_word;
        private System.Windows.Forms.CheckBox checkBox_two;
        private System.Windows.Forms.CheckBox checkBox_three;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_reaplec;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
    }
}

