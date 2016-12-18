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
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label_port_defalt = new System.Windows.Forms.Label();
            this.label_Sheach_Host = new System.Windows.Forms.Label();
            this.textBox_Sheach_Host_value = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label_thread_text = new System.Windows.Forms.Label();
            this.textBox_thread_value = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_select_hostfile = new System.Windows.Forms.Label();
            this.textBox_StartLine = new System.Windows.Forms.TextBox();
            this.label_inputUser_text = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_grogress_value = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label_scanFail_value = new System.Windows.Forms.Label();
            this.label_connetionFail_value = new System.Windows.Forms.Label();
            this.label_host_fail = new System.Windows.Forms.Label();
            this.label_host_success = new System.Windows.Forms.Label();
            this.label_scanCount_value = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_time_value = new System.Windows.Forms.Label();
            this.label_thead_count = new System.Windows.Forms.Label();
            this.lable_progress_text = new System.Windows.Forms.Label();
            this.label_scanFail_text = new System.Windows.Forms.Label();
            this.label_scanSuccess_text = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_scanCount_text = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_time_text = new System.Windows.Forms.Label();
            this.lable_thead_text = new System.Windows.Forms.Label();
            this.textBox_result = new System.Windows.Forms.TextBox();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.button_stop);
            this.groupBox5.Controls.Add(this.button_start);
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
            this.checkBox1.Location = new System.Drawing.Point(13, 53);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "使用存档";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label_port_defalt
            // 
            this.label_port_defalt.AutoSize = true;
            this.label_port_defalt.Location = new System.Drawing.Point(11, 187);
            this.label_port_defalt.Name = "label_port_defalt";
            this.label_port_defalt.Size = new System.Drawing.Size(149, 12);
            this.label_port_defalt.TabIndex = 3;
            this.label_port_defalt.Text = "设置常用端口，以逗号隔开";
            // 
            // label_Sheach_Host
            // 
            this.label_Sheach_Host.AutoSize = true;
            this.label_Sheach_Host.Location = new System.Drawing.Point(11, 160);
            this.label_Sheach_Host.Name = "label_Sheach_Host";
            this.label_Sheach_Host.Size = new System.Drawing.Size(113, 12);
            this.label_Sheach_Host.TabIndex = 3;
            this.label_Sheach_Host.Text = "循环查询服务器次数";
            // 
            // textBox_Sheach_Host_value
            // 
            this.textBox_Sheach_Host_value.Location = new System.Drawing.Point(130, 156);
            this.textBox_Sheach_Host_value.Name = "textBox_Sheach_Host_value";
            this.textBox_Sheach_Host_value.Size = new System.Drawing.Size(145, 21);
            this.textBox_Sheach_Host_value.TabIndex = 1;
            this.textBox_Sheach_Host_value.Text = "3";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 208);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(263, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "25，587，465，30-65535";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.label_thread_text);
            this.groupBox2.Controls.Add(this.textBox_thread_value);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label_select_hostfile);
            this.groupBox2.Controls.Add(this.textBox_StartLine);
            this.groupBox2.Controls.Add(this.label_inputUser_text);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label_Sheach_Host);
            this.groupBox2.Controls.Add(this.label_port_defalt);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.textBox_Sheach_Host_value);
            this.groupBox2.Location = new System.Drawing.Point(175, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 299);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "导入数据";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(14, 37);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(263, 21);
            this.textBox2.TabIndex = 7;
            // 
            // label_thread_text
            // 
            this.label_thread_text.AutoSize = true;
            this.label_thread_text.Location = new System.Drawing.Point(11, 241);
            this.label_thread_text.Name = "label_thread_text";
            this.label_thread_text.Size = new System.Drawing.Size(53, 12);
            this.label_thread_text.TabIndex = 6;
            this.label_thread_text.Text = "设置线程";
            // 
            // textBox_thread_value
            // 
            this.textBox_thread_value.Location = new System.Drawing.Point(80, 238);
            this.textBox_thread_value.Name = "textBox_thread_value";
            this.textBox_thread_value.Size = new System.Drawing.Size(195, 21);
            this.textBox_thread_value.TabIndex = 5;
            this.textBox_thread_value.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择端口文件[可选]";
            // 
            // label_select_hostfile
            // 
            this.label_select_hostfile.AutoSize = true;
            this.label_select_hostfile.Location = new System.Drawing.Point(11, 62);
            this.label_select_hostfile.Name = "label_select_hostfile";
            this.label_select_hostfile.Size = new System.Drawing.Size(125, 12);
            this.label_select_hostfile.TabIndex = 0;
            this.label_select_hostfile.Text = "选择服务器文件[可选]";
            // 
            // textBox_StartLine
            // 
            this.textBox_StartLine.Location = new System.Drawing.Point(79, 271);
            this.textBox_StartLine.Name = "textBox_StartLine";
            this.textBox_StartLine.Size = new System.Drawing.Size(196, 21);
            this.textBox_StartLine.TabIndex = 5;
            this.textBox_StartLine.Text = "0";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 274);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "设置启动行";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_grogress_value);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.label_scanFail_value);
            this.groupBox1.Controls.Add(this.label_connetionFail_value);
            this.groupBox1.Controls.Add(this.label_host_fail);
            this.groupBox1.Controls.Add(this.label_host_success);
            this.groupBox1.Controls.Add(this.label_scanCount_value);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label_time_value);
            this.groupBox1.Controls.Add(this.label_thead_count);
            this.groupBox1.Controls.Add(this.lable_progress_text);
            this.groupBox1.Controls.Add(this.label_scanFail_text);
            this.groupBox1.Controls.Add(this.label_scanSuccess_text);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label_scanCount_text);
            this.groupBox1.Controls.Add(this.label2);
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
            this.label_grogress_value.Location = new System.Drawing.Point(69, 242);
            this.label_grogress_value.Name = "label_grogress_value";
            this.label_grogress_value.Size = new System.Drawing.Size(17, 12);
            this.label_grogress_value.TabIndex = 3;
            this.label_grogress_value.Text = "0%";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 238);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(141, 23);
            this.progressBar1.TabIndex = 1;
            this.progressBar1.Value = 2;
            // 
            // label_scanFail_value
            // 
            this.label_scanFail_value.AutoSize = true;
            this.label_scanFail_value.Location = new System.Drawing.Point(95, 194);
            this.label_scanFail_value.Name = "label_scanFail_value";
            this.label_scanFail_value.Size = new System.Drawing.Size(11, 12);
            this.label_scanFail_value.TabIndex = 1;
            this.label_scanFail_value.Text = "0";
            // 
            // label_connetionFail_value
            // 
            this.label_connetionFail_value.AutoSize = true;
            this.label_connetionFail_value.Location = new System.Drawing.Point(95, 169);
            this.label_connetionFail_value.Name = "label_connetionFail_value";
            this.label_connetionFail_value.Size = new System.Drawing.Size(11, 12);
            this.label_connetionFail_value.TabIndex = 1;
            this.label_connetionFail_value.Text = "0";
            // 
            // label_host_fail
            // 
            this.label_host_fail.AutoSize = true;
            this.label_host_fail.Location = new System.Drawing.Point(95, 112);
            this.label_host_fail.Name = "label_host_fail";
            this.label_host_fail.Size = new System.Drawing.Size(11, 12);
            this.label_host_fail.TabIndex = 1;
            this.label_host_fail.Text = "0";
            // 
            // label_host_success
            // 
            this.label_host_success.AutoSize = true;
            this.label_host_success.Location = new System.Drawing.Point(95, 88);
            this.label_host_success.Name = "label_host_success";
            this.label_host_success.Size = new System.Drawing.Size(11, 12);
            this.label_host_success.TabIndex = 1;
            this.label_host_success.Text = "0";
            // 
            // label_scanCount_value
            // 
            this.label_scanCount_value.AutoSize = true;
            this.label_scanCount_value.Location = new System.Drawing.Point(95, 43);
            this.label_scanCount_value.Name = "label_scanCount_value";
            this.label_scanCount_value.Size = new System.Drawing.Size(11, 12);
            this.label_scanCount_value.TabIndex = 1;
            this.label_scanCount_value.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(95, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "0";
            // 
            // label_time_value
            // 
            this.label_time_value.AutoSize = true;
            this.label_time_value.Location = new System.Drawing.Point(95, 21);
            this.label_time_value.Name = "label_time_value";
            this.label_time_value.Size = new System.Drawing.Size(11, 12);
            this.label_time_value.TabIndex = 1;
            this.label_time_value.Text = "0";
            // 
            // label_thead_count
            // 
            this.label_thead_count.AutoSize = true;
            this.label_thead_count.Location = new System.Drawing.Point(95, 148);
            this.label_thead_count.Name = "label_thead_count";
            this.label_thead_count.Size = new System.Drawing.Size(11, 12);
            this.label_thead_count.TabIndex = 1;
            this.label_thead_count.Text = "0";
            // 
            // lable_progress_text
            // 
            this.lable_progress_text.AutoSize = true;
            this.lable_progress_text.Location = new System.Drawing.Point(7, 218);
            this.lable_progress_text.Name = "lable_progress_text";
            this.lable_progress_text.Size = new System.Drawing.Size(53, 12);
            this.lable_progress_text.TabIndex = 0;
            this.lable_progress_text.Text = "总进度：";
            // 
            // label_scanFail_text
            // 
            this.label_scanFail_text.AutoSize = true;
            this.label_scanFail_text.Location = new System.Drawing.Point(6, 194);
            this.label_scanFail_text.Name = "label_scanFail_text";
            this.label_scanFail_text.Size = new System.Drawing.Size(77, 12);
            this.label_scanFail_text.TabIndex = 0;
            this.label_scanFail_text.Text = "端口失败数：";
            // 
            // label_scanSuccess_text
            // 
            this.label_scanSuccess_text.AutoSize = true;
            this.label_scanSuccess_text.Location = new System.Drawing.Point(6, 169);
            this.label_scanSuccess_text.Name = "label_scanSuccess_text";
            this.label_scanSuccess_text.Size = new System.Drawing.Size(77, 12);
            this.label_scanSuccess_text.TabIndex = 0;
            this.label_scanSuccess_text.Text = "端口成功数：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "服务器失败：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "服务器成功：";
            // 
            // label_scanCount_text
            // 
            this.label_scanCount_text.AutoSize = true;
            this.label_scanCount_text.Location = new System.Drawing.Point(6, 43);
            this.label_scanCount_text.Name = "label_scanCount_text";
            this.label_scanCount_text.Size = new System.Drawing.Size(65, 12);
            this.label_scanCount_text.TabIndex = 0;
            this.label_scanCount_text.Text = "扫描总数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "服务器总是：";
            // 
            // label_time_text
            // 
            this.label_time_text.AutoSize = true;
            this.label_time_text.Location = new System.Drawing.Point(6, 21);
            this.label_time_text.Name = "label_time_text";
            this.label_time_text.Size = new System.Drawing.Size(65, 12);
            this.label_time_text.TabIndex = 0;
            this.label_time_text.Text = "运行时间：";
            // 
            // lable_thead_text
            // 
            this.lable_thead_text.AutoSize = true;
            this.lable_thead_text.Location = new System.Drawing.Point(6, 148);
            this.lable_thead_text.Name = "lable_thead_text";
            this.lable_thead_text.Size = new System.Drawing.Size(53, 12);
            this.lable_thead_text.TabIndex = 0;
            this.lable_thead_text.Text = "线程数：";
            // 
            // textBox_result
            // 
            this.textBox_result.Location = new System.Drawing.Point(495, 27);
            this.textBox_result.Multiline = true;
            this.textBox_result.Name = "textBox_result";
            this.textBox_result.Size = new System.Drawing.Size(194, 377);
            this.textBox_result.TabIndex = 7;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(12, 77);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(263, 21);
            this.textBox3.TabIndex = 7;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(11, 119);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(263, 21);
            this.textBox4.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 409);
            this.Controls.Add(this.textBox_result);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "SMTP-服务器查找，端口扫描";
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label_port_defalt;
        private System.Windows.Forms.Label label_Sheach_Host;
        private System.Windows.Forms.TextBox textBox_Sheach_Host_value;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label_inputUser_text;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_grogress_value;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label_scanFail_value;
        private System.Windows.Forms.Label label_connetionFail_value;
        private System.Windows.Forms.Label label_scanCount_value;
        private System.Windows.Forms.Label label_time_value;
        private System.Windows.Forms.Label label_thead_count;
        private System.Windows.Forms.Label lable_progress_text;
        private System.Windows.Forms.Label label_scanFail_text;
        private System.Windows.Forms.Label label_scanSuccess_text;
        private System.Windows.Forms.Label label_scanCount_text;
        private System.Windows.Forms.Label label_time_text;
        private System.Windows.Forms.Label lable_thead_text;
        private System.Windows.Forms.TextBox textBox_StartLine;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_thread_text;
        private System.Windows.Forms.TextBox textBox_thread_value;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_select_hostfile;
        private System.Windows.Forms.Label label_host_fail;
        private System.Windows.Forms.Label label_host_success;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_result;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
    }
}

