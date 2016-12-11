namespace SMTP_SniffAPP
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lable_thead_text = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label_thead_count = new System.Windows.Forms.Label();
            this.label_time_text = new System.Windows.Forms.Label();
            this.label_time_value = new System.Windows.Forms.Label();
            this.label_email_text = new System.Windows.Forms.Label();
            this.label_email_value = new System.Windows.Forms.Label();
            this.label_password_text = new System.Windows.Forms.Label();
            this.label_password_value = new System.Windows.Forms.Label();
            this.label_connection_text = new System.Windows.Forms.Label();
            this.label_connection_value = new System.Windows.Forms.Label();
            this.label_connectionFail_text = new System.Windows.Forms.Label();
            this.label_connetionFail_value = new System.Windows.Forms.Label();
            this.label_cract_text = new System.Windows.Forms.Label();
            this.label_crack_value = new System.Windows.Forms.Label();
            this.lable_memory_text = new System.Windows.Forms.Label();
            this.label_memory_vlaue = new System.Windows.Forms.Label();
            this.lable_progress_text = new System.Windows.Forms.Label();
            this.label_grogress_value = new System.Windows.Forms.Label();
            this.label_cup_text = new System.Windows.Forms.Label();
            this.label_cup_value = new System.Windows.Forms.Label();
            this.label_networkFlow_text = new System.Windows.Forms.Label();
            this.label_networkFlow_value = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_networkFlow_value);
            this.groupBox1.Controls.Add(this.label_cup_value);
            this.groupBox1.Controls.Add(this.label_grogress_value);
            this.groupBox1.Controls.Add(this.label_memory_vlaue);
            this.groupBox1.Controls.Add(this.label_crack_value);
            this.groupBox1.Controls.Add(this.label_connetionFail_value);
            this.groupBox1.Controls.Add(this.label_connection_value);
            this.groupBox1.Controls.Add(this.label_password_value);
            this.groupBox1.Controls.Add(this.label_email_value);
            this.groupBox1.Controls.Add(this.label_time_value);
            this.groupBox1.Controls.Add(this.label_thead_count);
            this.groupBox1.Controls.Add(this.label_networkFlow_text);
            this.groupBox1.Controls.Add(this.label_cup_text);
            this.groupBox1.Controls.Add(this.lable_progress_text);
            this.groupBox1.Controls.Add(this.lable_memory_text);
            this.groupBox1.Controls.Add(this.label_cract_text);
            this.groupBox1.Controls.Add(this.label_connectionFail_text);
            this.groupBox1.Controls.Add(this.label_connection_text);
            this.groupBox1.Controls.Add(this.label_password_text);
            this.groupBox1.Controls.Add(this.label_email_text);
            this.groupBox1.Controls.Add(this.label_time_text);
            this.groupBox1.Controls.Add(this.lable_thead_text);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 353);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "运行信息";
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
            // label_thead_count
            // 
            this.label_thead_count.AutoSize = true;
            this.label_thead_count.Location = new System.Drawing.Point(95, 30);
            this.label_thead_count.Name = "label_thead_count";
            this.label_thead_count.Size = new System.Drawing.Size(11, 12);
            this.label_thead_count.TabIndex = 1;
            this.label_thead_count.Text = "0";
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
            // label_time_value
            // 
            this.label_time_value.AutoSize = true;
            this.label_time_value.Location = new System.Drawing.Point(95, 52);
            this.label_time_value.Name = "label_time_value";
            this.label_time_value.Size = new System.Drawing.Size(11, 12);
            this.label_time_value.TabIndex = 1;
            this.label_time_value.Text = "0";
            // 
            // label_email_text
            // 
            this.label_email_text.AutoSize = true;
            this.label_email_text.Location = new System.Drawing.Point(6, 77);
            this.label_email_text.Name = "label_email_text";
            this.label_email_text.Size = new System.Drawing.Size(89, 12);
            this.label_email_text.TabIndex = 0;
            this.label_email_text.Text = "导入的邮件数：";
            // 
            // label_email_value
            // 
            this.label_email_value.AutoSize = true;
            this.label_email_value.Location = new System.Drawing.Point(95, 77);
            this.label_email_value.Name = "label_email_value";
            this.label_email_value.Size = new System.Drawing.Size(11, 12);
            this.label_email_value.TabIndex = 1;
            this.label_email_value.Text = "0";
            // 
            // label_password_text
            // 
            this.label_password_text.AutoSize = true;
            this.label_password_text.Location = new System.Drawing.Point(6, 103);
            this.label_password_text.Name = "label_password_text";
            this.label_password_text.Size = new System.Drawing.Size(89, 12);
            this.label_password_text.TabIndex = 0;
            this.label_password_text.Text = "导入的密码数：";
            // 
            // label_password_value
            // 
            this.label_password_value.AutoSize = true;
            this.label_password_value.Location = new System.Drawing.Point(95, 103);
            this.label_password_value.Name = "label_password_value";
            this.label_password_value.Size = new System.Drawing.Size(11, 12);
            this.label_password_value.TabIndex = 1;
            this.label_password_value.Text = "0";
            // 
            // label_connection_text
            // 
            this.label_connection_text.AutoSize = true;
            this.label_connection_text.Location = new System.Drawing.Point(6, 131);
            this.label_connection_text.Name = "label_connection_text";
            this.label_connection_text.Size = new System.Drawing.Size(65, 12);
            this.label_connection_text.TabIndex = 0;
            this.label_connection_text.Text = "尝试链接：";
            // 
            // label_connection_value
            // 
            this.label_connection_value.AutoSize = true;
            this.label_connection_value.Location = new System.Drawing.Point(95, 131);
            this.label_connection_value.Name = "label_connection_value";
            this.label_connection_value.Size = new System.Drawing.Size(11, 12);
            this.label_connection_value.TabIndex = 1;
            this.label_connection_value.Text = "0";
            // 
            // label_connectionFail_text
            // 
            this.label_connectionFail_text.AutoSize = true;
            this.label_connectionFail_text.Location = new System.Drawing.Point(6, 165);
            this.label_connectionFail_text.Name = "label_connectionFail_text";
            this.label_connectionFail_text.Size = new System.Drawing.Size(77, 12);
            this.label_connectionFail_text.TabIndex = 0;
            this.label_connectionFail_text.Text = "链接失败数：";
            // 
            // label_connetionFail_value
            // 
            this.label_connetionFail_value.AutoSize = true;
            this.label_connetionFail_value.Location = new System.Drawing.Point(95, 165);
            this.label_connetionFail_value.Name = "label_connetionFail_value";
            this.label_connetionFail_value.Size = new System.Drawing.Size(11, 12);
            this.label_connetionFail_value.TabIndex = 1;
            this.label_connetionFail_value.Text = "0";
            // 
            // label_cract_text
            // 
            this.label_cract_text.AutoSize = true;
            this.label_cract_text.Location = new System.Drawing.Point(6, 190);
            this.label_cract_text.Name = "label_cract_text";
            this.label_cract_text.Size = new System.Drawing.Size(77, 12);
            this.label_cract_text.TabIndex = 0;
            this.label_cract_text.Text = "破解成功数：";
            // 
            // label_crack_value
            // 
            this.label_crack_value.AutoSize = true;
            this.label_crack_value.Location = new System.Drawing.Point(95, 190);
            this.label_crack_value.Name = "label_crack_value";
            this.label_crack_value.Size = new System.Drawing.Size(11, 12);
            this.label_crack_value.TabIndex = 1;
            this.label_crack_value.Text = "0";
            // 
            // lable_memory_text
            // 
            this.lable_memory_text.AutoSize = true;
            this.lable_memory_text.Location = new System.Drawing.Point(6, 218);
            this.lable_memory_text.Name = "lable_memory_text";
            this.lable_memory_text.Size = new System.Drawing.Size(65, 12);
            this.lable_memory_text.TabIndex = 0;
            this.lable_memory_text.Text = "内存负载：";
            // 
            // label_memory_vlaue
            // 
            this.label_memory_vlaue.AutoSize = true;
            this.label_memory_vlaue.Location = new System.Drawing.Point(95, 218);
            this.label_memory_vlaue.Name = "label_memory_vlaue";
            this.label_memory_vlaue.Size = new System.Drawing.Size(11, 12);
            this.label_memory_vlaue.TabIndex = 1;
            this.label_memory_vlaue.Text = "0";
            // 
            // lable_progress_text
            // 
            this.lable_progress_text.AutoSize = true;
            this.lable_progress_text.Location = new System.Drawing.Point(7, 292);
            this.lable_progress_text.Name = "lable_progress_text";
            this.lable_progress_text.Size = new System.Drawing.Size(59, 12);
            this.lable_progress_text.TabIndex = 0;
            this.lable_progress_text.Text = "总进度%：";
            // 
            // label_grogress_value
            // 
            this.label_grogress_value.AutoSize = true;
            this.label_grogress_value.Location = new System.Drawing.Point(95, 292);
            this.label_grogress_value.Name = "label_grogress_value";
            this.label_grogress_value.Size = new System.Drawing.Size(11, 12);
            this.label_grogress_value.TabIndex = 1;
            this.label_grogress_value.Text = "0";
            // 
            // label_cup_text
            // 
            this.label_cup_text.AutoSize = true;
            this.label_cup_text.Location = new System.Drawing.Point(7, 244);
            this.label_cup_text.Name = "label_cup_text";
            this.label_cup_text.Size = new System.Drawing.Size(59, 12);
            this.label_cup_text.TabIndex = 0;
            this.label_cup_text.Text = "CPU负载：";
            // 
            // label_cup_value
            // 
            this.label_cup_value.AutoSize = true;
            this.label_cup_value.Location = new System.Drawing.Point(95, 244);
            this.label_cup_value.Name = "label_cup_value";
            this.label_cup_value.Size = new System.Drawing.Size(11, 12);
            this.label_cup_value.TabIndex = 1;
            this.label_cup_value.Text = "0";
            // 
            // label_networkFlow_text
            // 
            this.label_networkFlow_text.AutoSize = true;
            this.label_networkFlow_text.Location = new System.Drawing.Point(7, 267);
            this.label_networkFlow_text.Name = "label_networkFlow_text";
            this.label_networkFlow_text.Size = new System.Drawing.Size(65, 12);
            this.label_networkFlow_text.TabIndex = 0;
            this.label_networkFlow_text.Text = "网络流量：";
            // 
            // label_networkFlow_value
            // 
            this.label_networkFlow_value.AutoSize = true;
            this.label_networkFlow_value.Location = new System.Drawing.Point(95, 267);
            this.label_networkFlow_value.Name = "label_networkFlow_value";
            this.label_networkFlow_value.Size = new System.Drawing.Size(11, 12);
            this.label_networkFlow_value.TabIndex = 1;
            this.label_networkFlow_value.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 366);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "SMTP-探嗅";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_networkFlow_value;
        private System.Windows.Forms.Label label_cup_value;
        private System.Windows.Forms.Label label_grogress_value;
        private System.Windows.Forms.Label label_memory_vlaue;
        private System.Windows.Forms.Label label_crack_value;
        private System.Windows.Forms.Label label_connetionFail_value;
        private System.Windows.Forms.Label label_connection_value;
        private System.Windows.Forms.Label label_password_value;
        private System.Windows.Forms.Label label_email_value;
        private System.Windows.Forms.Label label_time_value;
        private System.Windows.Forms.Label label_thead_count;
        private System.Windows.Forms.Label label_networkFlow_text;
        private System.Windows.Forms.Label label_cup_text;
        private System.Windows.Forms.Label lable_progress_text;
        private System.Windows.Forms.Label lable_memory_text;
        private System.Windows.Forms.Label label_cract_text;
        private System.Windows.Forms.Label label_connectionFail_text;
        private System.Windows.Forms.Label label_connection_text;
        private System.Windows.Forms.Label label_password_text;
        private System.Windows.Forms.Label label_email_text;
        private System.Windows.Forms.Label label_time_text;
        private System.Windows.Forms.Label lable_thead_text;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

