namespace SearchRepleace
{
    partial class demo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(demo));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button_Replece = new System.Windows.Forms.Button();
            this.btn_Reacher = new System.Windows.Forms.Button();
            this.btn_SelectFile = new System.Windows.Forms.Button();
            this.text_fileNames = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tab_Family = new System.Windows.Forms.TabPage();
            this.dataGridView_Fmaily = new System.Windows.Forms.DataGridView();
            this.Original = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IScOM = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NewFamily = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab_Mtype = new System.Windows.Forms.TabPage();
            this.dataGridView_Mtype = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.panel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tab_Family.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Fmaily)).BeginInit();
            this.tab_Mtype.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Mtype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button_Replece);
            this.panel1.Controls.Add(this.btn_Reacher);
            this.panel1.Controls.Add(this.btn_SelectFile);
            this.panel1.Controls.Add(this.text_fileNames);
            this.panel1.Controls.Add(this.tabControl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 400);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(13, 379);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "查找统计";
            // 
            // button_Replece
            // 
            this.button_Replece.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Replece.Location = new System.Drawing.Point(347, 374);
            this.button_Replece.Name = "button_Replece";
            this.button_Replece.Size = new System.Drawing.Size(75, 23);
            this.button_Replece.TabIndex = 4;
            this.button_Replece.Text = "替换";
            this.button_Replece.UseVisualStyleBackColor = true;
            this.button_Replece.Click += new System.EventHandler(this.button_Replece_Click);
            // 
            // btn_Reacher
            // 
            this.btn_Reacher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Reacher.Location = new System.Drawing.Point(266, 374);
            this.btn_Reacher.Name = "btn_Reacher";
            this.btn_Reacher.Size = new System.Drawing.Size(75, 23);
            this.btn_Reacher.TabIndex = 3;
            this.btn_Reacher.Text = "查找";
            this.btn_Reacher.UseVisualStyleBackColor = true;
            this.btn_Reacher.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btn_SelectFile
            // 
            this.btn_SelectFile.Location = new System.Drawing.Point(9, 3);
            this.btn_SelectFile.Name = "btn_SelectFile";
            this.btn_SelectFile.Size = new System.Drawing.Size(75, 23);
            this.btn_SelectFile.TabIndex = 2;
            this.btn_SelectFile.Text = "选择文件";
            this.btn_SelectFile.UseVisualStyleBackColor = true;
            this.btn_SelectFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // text_fileNames
            // 
            this.text_fileNames.Location = new System.Drawing.Point(9, 23);
            this.text_fileNames.Multiline = true;
            this.text_fileNames.Name = "text_fileNames";
            this.text_fileNames.Size = new System.Drawing.Size(517, 119);
            this.text_fileNames.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tab_Family);
            this.tabControl.Controls.Add(this.tab_Mtype);
            this.tabControl.Location = new System.Drawing.Point(8, 148);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(521, 220);
            this.tabControl.TabIndex = 0;
            // 
            // tab_Family
            // 
            this.tab_Family.AutoScroll = true;
            this.tab_Family.Controls.Add(this.dataGridView_Fmaily);
            this.tab_Family.Location = new System.Drawing.Point(4, 22);
            this.tab_Family.Name = "tab_Family";
            this.tab_Family.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Family.Size = new System.Drawing.Size(513, 194);
            this.tab_Family.TabIndex = 0;
            this.tab_Family.Text = "文字";
            this.tab_Family.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Fmaily
            // 
            this.dataGridView_Fmaily.AllowUserToAddRows = false;
            this.dataGridView_Fmaily.AllowUserToOrderColumns = true;
            this.dataGridView_Fmaily.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Fmaily.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Fmaily.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Original,
            this.Count,
            this.IScOM,
            this.NewFamily});
            this.dataGridView_Fmaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Fmaily.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_Fmaily.Name = "dataGridView_Fmaily";
            this.dataGridView_Fmaily.RowTemplate.Height = 23;
            this.dataGridView_Fmaily.Size = new System.Drawing.Size(507, 188);
            this.dataGridView_Fmaily.TabIndex = 0;
            // 
            // Original
            // 
            this.Original.FillWeight = 105.8027F;
            this.Original.HeaderText = "原始字体";
            this.Original.Name = "Original";
            // 
            // Count
            // 
            this.Count.FillWeight = 79.18781F;
            this.Count.HeaderText = "是否异常";
            this.Count.Name = "Count";
            // 
            // IScOM
            // 
            this.IScOM.FillWeight = 109.2068F;
            this.IScOM.HeaderText = "添加组合字体";
            this.IScOM.MinimumWidth = 7;
            this.IScOM.Name = "IScOM";
            this.IScOM.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IScOM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // NewFamily
            // 
            this.NewFamily.FillWeight = 105.8027F;
            this.NewFamily.HeaderText = "替换后字体";
            this.NewFamily.Name = "NewFamily";
            // 
            // tab_Mtype
            // 
            this.tab_Mtype.AutoScroll = true;
            this.tab_Mtype.Controls.Add(this.dataGridView_Mtype);
            this.tab_Mtype.Location = new System.Drawing.Point(4, 22);
            this.tab_Mtype.Name = "tab_Mtype";
            this.tab_Mtype.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Mtype.Size = new System.Drawing.Size(513, 194);
            this.tab_Mtype.TabIndex = 1;
            this.tab_Mtype.Text = "注音";
            this.tab_Mtype.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Mtype
            // 
            this.dataGridView_Mtype.AllowUserToAddRows = false;
            this.dataGridView_Mtype.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Mtype.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Mtype.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView_Mtype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Mtype.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_Mtype.Name = "dataGridView_Mtype";
            this.dataGridView_Mtype.RowTemplate.Height = 23;
            this.dataGridView_Mtype.Size = new System.Drawing.Size(507, 188);
            this.dataGridView_Mtype.TabIndex = 0;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "原始读音";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "目标读音";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "替换后读音";
            this.Column4.Name = "Column4";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(529, 400);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "demo";
            this.Text = "demo";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tab_Family.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Fmaily)).EndInit();
            this.tab_Mtype.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Mtype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tab_Family;
        private System.Windows.Forms.TabPage tab_Mtype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_SelectFile;
        private System.Windows.Forms.TextBox text_fileNames;
        private System.Windows.Forms.DataGridView dataGridView_Fmaily;
        private System.Windows.Forms.DataGridView dataGridView_Mtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button btn_Reacher;
        private System.Windows.Forms.Button button_Replece;
        private System.Windows.Forms.DataGridViewTextBoxColumn Original;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IScOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewFamily;
    }
}

