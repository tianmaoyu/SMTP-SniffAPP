namespace SMTP_PortScaner
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_stop = new System.Windows.Forms.Button();
            this.button_start = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label_port_defalt = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_saveFile = new System.Windows.Forms.TextBox();
            this.textBox_thead_value = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox_mailFile = new System.Windows.Forms.ComboBox();
            this.label_inputUser_text = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_grogress_value = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label_networkFlow_value = new System.Windows.Forms.Label();
            this.label_cup_value = new System.Windows.Forms.Label();
            this.label_memory_vlaue = new System.Windows.Forms.Label();
            this.label_scanFail_value = new System.Windows.Forms.Label();
            this.label_connetionFail_value = new System.Windows.Forms.Label();
            this.label_scanCount_value = new System.Windows.Forms.Label();
            this.label_time_value = new System.Windows.Forms.Label();
            this.label_thead_count = new System.Windows.Forms.Label();
            this.label_networkFlow_text = new System.Windows.Forms.Label();
            this.label_cup_text = new System.Windows.Forms.Label();
            this.lable_progress_text = new System.Windows.Forms.Label();
            this.lable_memory_text = new System.Windows.Forms.Label();
            this.label_scanFail_text = new System.Windows.Forms.Label();
            this.label_scanSuccess_text = new System.Windows.Forms.Label();
            this.label_scanCount_text = new System.Windows.Forms.Label();
            this.label_time_text = new System.Windows.Forms.Label();
            this.lable_thead_text = new System.Windows.Forms.Label();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.button_stop);
            this.groupBox5.Controls.Add(this.button_start);
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Controls.Add(this.checkBox1);
            this.groupBox5.Location = new System.Drawing.Point(11, 319);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(455, 87);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "操作";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(114, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "已经授权";
            // 
            // button_stop
            // 
            this.button_stop.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button_stop.Location = new System.Drawing.Point(363, 29);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(76, 32);
            this.button_stop.TabIndex = 0;
            this.button_stop.Text = "暂停";
            this.button_stop.UseVisualStyleBackColor = true;
            // 
            // button_start
            // 
            this.button_start.ForeColor = System.Drawing.Color.Red;
            this.button_start.Location = new System.Drawing.Point(256, 30);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(78, 32);
            this.button_start.TabIndex = 0;
            this.button_start.Text = "启动";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(8, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "拨号配置ADSL";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "授权配置";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(116, 53);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "使用存档";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label_port_defalt
            // 
            this.label_port_defalt.AutoSize = true;
            this.label_port_defalt.Location = new System.Drawing.Point(11, 63);
            this.label_port_defalt.Name = "label_port_defalt";
            this.label_port_defalt.Size = new System.Drawing.Size(149, 12);
            this.label_port_defalt.TabIndex = 3;
            this.label_port_defalt.Text = "设置常用端口，以逗号隔开";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "线程设置(1-200)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(263, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "成功的保存到文件【自动加入启动时间前缀】txt";
            // 
            // textBox_saveFile
            // 
            this.textBox_saveFile.Location = new System.Drawing.Point(14, 179);
            this.textBox_saveFile.Name = "textBox_saveFile";
            this.textBox_saveFile.Size = new System.Drawing.Size(262, 21);
            this.textBox_saveFile.TabIndex = 1;
            this.textBox_saveFile.Text = "2016-12-12：12-8-25@success-mail.txt";
            // 
            // textBox_thead_value
            // 
            this.textBox_thead_value.Location = new System.Drawing.Point(13, 127);
            this.textBox_thead_value.Name = "textBox_thead_value";
            this.textBox_thead_value.Size = new System.Drawing.Size(263, 21);
            this.textBox_thead_value.TabIndex = 1;
            this.textBox_thead_value.Text = "10";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 83);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(263, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "25，587，465";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox_mailFile);
            this.groupBox2.Controls.Add(this.label_inputUser_text);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label_port_defalt);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.textBox_saveFile);
            this.groupBox2.Controls.Add(this.textBox_thead_value);
            this.groupBox2.Location = new System.Drawing.Point(175, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 299);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "导入数据";
            // 
            // comboBox_mailFile
            // 
            this.comboBox_mailFile.FormattingEnabled = true;
            this.comboBox_mailFile.Location = new System.Drawing.Point(11, 36);
            this.comboBox_mailFile.Name = "comboBox_mailFile";
            this.comboBox_mailFile.Size = new System.Drawing.Size(265, 20);
            this.comboBox_mailFile.TabIndex = 1;
            this.comboBox_mailFile.MouseDown += new System.Windows.Forms.MouseEventHandler(this.comboBox_mailFile_MouseDown);
            // 
            // label_inputUser_text
            // 
            this.label_inputUser_text.AutoSize = true;
            this.label_inputUser_text.Location = new System.Drawing.Point(9, 21);
            this.label_inputUser_text.Name = "label_inputUser_text";
            this.label_inputUser_text.Size = new System.Drawing.Size(77, 12);
            this.label_inputUser_text.TabIndex = 0;
            this.label_inputUser_text.Text = "选择邮件文件";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "失败的保存到文件【自动加入启动时间前缀】txt";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(14, 239);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(262, 21);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "2016-12-12：12-8-25@Fail-mail.txt";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_grogress_value);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.label_networkFlow_value);
            this.groupBox1.Controls.Add(this.label_cup_value);
            this.groupBox1.Controls.Add(this.label_memory_vlaue);
            this.groupBox1.Controls.Add(this.label_scanFail_value);
            this.groupBox1.Controls.Add(this.label_connetionFail_value);
            this.groupBox1.Controls.Add(this.label_scanCount_value);
            this.groupBox1.Controls.Add(this.label_time_value);
            this.groupBox1.Controls.Add(this.label_thead_count);
            this.groupBox1.Controls.Add(this.label_networkFlow_text);
            this.groupBox1.Controls.Add(this.label_cup_text);
            this.groupBox1.Controls.Add(this.lable_progress_text);
            this.groupBox1.Controls.Add(this.lable_memory_text);
            this.groupBox1.Controls.Add(this.label_scanFail_text);
            this.groupBox1.Controls.Add(this.label_scanSuccess_text);
            this.groupBox1.Controls.Add(this.label_scanCount_text);
            this.groupBox1.Controls.Add(this.label_time_text);
            this.groupBox1.Controls.Add(this.lable_thead_text);
            this.groupBox1.Location = new System.Drawing.Point(10, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 300);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "运行信息";
            // 
            // label_grogress_value
            // 
            this.label_grogress_value.AutoSize = true;
            this.label_grogress_value.Location = new System.Drawing.Point(70, 249);
            this.label_grogress_value.Name = "label_grogress_value";
            this.label_grogress_value.Size = new System.Drawing.Size(17, 12);
            this.label_grogress_value.TabIndex = 3;
            this.label_grogress_value.Text = "0%";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 243);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(141, 23);
            this.progressBar1.TabIndex = 1;
            this.progressBar1.Value = 2;
            // 
            // label_networkFlow_value
            // 
            this.label_networkFlow_value.AutoSize = true;
            this.label_networkFlow_value.Location = new System.Drawing.Point(95, 203);
            this.label_networkFlow_value.Name = "label_networkFlow_value";
            this.label_networkFlow_value.Size = new System.Drawing.Size(11, 12);
            this.label_networkFlow_value.TabIndex = 1;
            this.label_networkFlow_value.Text = "0";
            // 
            // label_cup_value
            // 
            this.label_cup_value.AutoSize = true;
            this.label_cup_value.Location = new System.Drawing.Point(95, 180);
            this.label_cup_value.Name = "label_cup_value";
            this.label_cup_value.Size = new System.Drawing.Size(11, 12);
            this.label_cup_value.TabIndex = 1;
            this.label_cup_value.Text = "0";
            // 
            // label_memory_vlaue
            // 
            this.label_memory_vlaue.AutoSize = true;
            this.label_memory_vlaue.Location = new System.Drawing.Point(95, 154);
            this.label_memory_vlaue.Name = "label_memory_vlaue";
            this.label_memory_vlaue.Size = new System.Drawing.Size(11, 12);
            this.label_memory_vlaue.TabIndex = 1;
            this.label_memory_vlaue.Text = "0";
            // 
            // label_scanFail_value
            // 
            this.label_scanFail_value.AutoSize = true;
            this.label_scanFail_value.Location = new System.Drawing.Point(95, 126);
            this.label_scanFail_value.Name = "label_scanFail_value";
            this.label_scanFail_value.Size = new System.Drawing.Size(11, 12);
            this.label_scanFail_value.TabIndex = 1;
            this.label_scanFail_value.Text = "0";
            // 
            // label_connetionFail_value
            // 
            this.label_connetionFail_value.AutoSize = true;
            this.label_connetionFail_value.Location = new System.Drawing.Point(95, 101);
            this.label_connetionFail_value.Name = "label_connetionFail_value";
            this.label_connetionFail_value.Size = new System.Drawing.Size(11, 12);
            this.label_connetionFail_value.TabIndex = 1;
            this.label_connetionFail_value.Text = "0";
            // 
            // label_scanCount_value
            // 
            this.label_scanCount_value.AutoSize = true;
            this.label_scanCount_value.Location = new System.Drawing.Point(95, 76);
            this.label_scanCount_value.Name = "label_scanCount_value";
            this.label_scanCount_value.Size = new System.Drawing.Size(11, 12);
            this.label_scanCount_value.TabIndex = 1;
            this.label_scanCount_value.Text = "0";
            // 
            // label_time_value
            // 
            this.label_time_value.AutoSize = true;
            this.label_time_value.Location = new System.Drawing.Point(95, 52);
            this.label_time_value.Name = "label_time_value";
            this.label_time_value.Size = new System.Drawing.Size(11, 12);
            this.label_time_value.TabIndex = 1;
            this.label_time_value.Text = "0";
            // 
            // label_thead_count
            // 
            this.label_thead_count.AutoSize = true;
            this.label_thead_count.Location = new System.Drawing.Point(95, 30);
            this.label_thead_count.Name = "label_thead_count";
            this.label_thead_count.Size = new System.Drawing.Size(11, 12);
            this.label_thead_count.TabIndex = 1;
            this.label_thead_count.Text = "0";
            // 
            // label_networkFlow_text
            // 
            this.label_networkFlow_text.AutoSize = true;
            this.label_networkFlow_text.Location = new System.Drawing.Point(7, 203);
            this.label_networkFlow_text.Name = "label_networkFlow_text";
            this.label_networkFlow_text.Size = new System.Drawing.Size(83, 12);
            this.label_networkFlow_text.TabIndex = 0;
            this.label_networkFlow_text.Text = "网络流量(M)：";
            // 
            // label_cup_text
            // 
            this.label_cup_text.AutoSize = true;
            this.label_cup_text.Location = new System.Drawing.Point(7, 180);
            this.label_cup_text.Name = "label_cup_text";
            this.label_cup_text.Size = new System.Drawing.Size(59, 12);
            this.label_cup_text.TabIndex = 0;
            this.label_cup_text.Text = "CPU负载：";
            // 
            // lable_progress_text
            // 
            this.lable_progress_text.AutoSize = true;
            this.lable_progress_text.Location = new System.Drawing.Point(7, 228);
            this.lable_progress_text.Name = "lable_progress_text";
            this.lable_progress_text.Size = new System.Drawing.Size(53, 12);
            this.lable_progress_text.TabIndex = 0;
            this.lable_progress_text.Text = "总进度：";
            // 
            // lable_memory_text
            // 
            this.lable_memory_text.AutoSize = true;
            this.lable_memory_text.Location = new System.Drawing.Point(6, 154);
            this.lable_memory_text.Name = "lable_memory_text";
            this.lable_memory_text.Size = new System.Drawing.Size(65, 12);
            this.lable_memory_text.TabIndex = 0;
            this.lable_memory_text.Text = "内存负载：";
            // 
            // label_scanFail_text
            // 
            this.label_scanFail_text.AutoSize = true;
            this.label_scanFail_text.Location = new System.Drawing.Point(6, 126);
            this.label_scanFail_text.Name = "label_scanFail_text";
            this.label_scanFail_text.Size = new System.Drawing.Size(77, 12);
            this.label_scanFail_text.TabIndex = 0;
            this.label_scanFail_text.Text = "扫描失败数：";
            // 
            // label_scanSuccess_text
            // 
            this.label_scanSuccess_text.AutoSize = true;
            this.label_scanSuccess_text.Location = new System.Drawing.Point(6, 101);
            this.label_scanSuccess_text.Name = "label_scanSuccess_text";
            this.label_scanSuccess_text.Size = new System.Drawing.Size(77, 12);
            this.label_scanSuccess_text.TabIndex = 0;
            this.label_scanSuccess_text.Text = "扫描成功数：";
            // 
            // label_scanCount_text
            // 
            this.label_scanCount_text.AutoSize = true;
            this.label_scanCount_text.Location = new System.Drawing.Point(6, 76);
            this.label_scanCount_text.Name = "label_scanCount_text";
            this.label_scanCount_text.Size = new System.Drawing.Size(65, 12);
            this.label_scanCount_text.TabIndex = 0;
            this.label_scanCount_text.Text = "扫描总数：";
            // 
            // label_time_text
            // 
            this.label_time_text.AutoSize = true;
            this.label_time_text.Location = new System.Drawing.Point(6, 52);
            this.label_time_text.Name = "label_time_text";
            this.label_time_text.Size = new System.Drawing.Size(65, 12);
            this.label_time_text.TabIndex = 0;
            this.label_time_text.Text = "运行时间：";
            // 
            // lable_thead_text
            // 
            this.lable_thead_text.AutoSize = true;
            this.lable_thead_text.Location = new System.Drawing.Point(6, 30);
            this.lable_thead_text.Name = "lable_thead_text";
            this.lable_thead_text.Size = new System.Drawing.Size(53, 12);
            this.lable_thead_text.TabIndex = 0;
            this.lable_thead_text.Text = "线程数：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 423);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "SMTP-端口扫描";
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label_port_defalt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_saveFile;
        private System.Windows.Forms.TextBox textBox_thead_value;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBox_mailFile;
        private System.Windows.Forms.Label label_inputUser_text;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_grogress_value;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label_networkFlow_value;
        private System.Windows.Forms.Label label_cup_value;
        private System.Windows.Forms.Label label_memory_vlaue;
        private System.Windows.Forms.Label label_scanFail_value;
        private System.Windows.Forms.Label label_connetionFail_value;
        private System.Windows.Forms.Label label_scanCount_value;
        private System.Windows.Forms.Label label_time_value;
        private System.Windows.Forms.Label label_thead_count;
        private System.Windows.Forms.Label label_networkFlow_text;
        private System.Windows.Forms.Label label_cup_text;
        private System.Windows.Forms.Label lable_progress_text;
        private System.Windows.Forms.Label lable_memory_text;
        private System.Windows.Forms.Label label_scanFail_text;
        private System.Windows.Forms.Label label_scanSuccess_text;
        private System.Windows.Forms.Label label_scanCount_text;
        private System.Windows.Forms.Label label_time_text;
        private System.Windows.Forms.Label lable_thead_text;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
    }
}

