﻿namespace SearchRepleace
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
            this.OldText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OldValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsCombination = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NewValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab_Mtype = new System.Windows.Forms.TabPage();
            this.dataGridView_Mtype = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.tab_Split = new System.Windows.Forms.TabPage();
            this.dataGridView_Split = new System.Windows.Forms.DataGridView();
            this.SplitId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SplitOldText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SplitValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tab_Family.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Fmaily)).BeginInit();
            this.tab_Mtype.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Mtype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.tab_Split.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Split)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(590, 393);
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
            this.button_Replece.Location = new System.Drawing.Point(408, 368);
            this.button_Replece.Name = "button_Replece";
            this.button_Replece.Size = new System.Drawing.Size(75, 23);
            this.button_Replece.TabIndex = 4;
            this.button_Replece.Text = "替换/拆分";
            this.button_Replece.UseVisualStyleBackColor = true;
            this.button_Replece.Click += new System.EventHandler(this.button_Replece_Click);
            // 
            // btn_Reacher
            // 
            this.btn_Reacher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Reacher.Location = new System.Drawing.Point(327, 367);
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
            this.text_fileNames.Size = new System.Drawing.Size(574, 75);
            this.text_fileNames.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tab_Family);
            this.tabControl.Controls.Add(this.tab_Split);
            this.tabControl.Controls.Add(this.tab_Mtype);
            this.tabControl.Location = new System.Drawing.Point(8, 104);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(582, 264);
            this.tabControl.TabIndex = 0;
            // 
            // tab_Family
            // 
            this.tab_Family.AutoScroll = true;
            this.tab_Family.Controls.Add(this.dataGridView_Fmaily);
            this.tab_Family.Location = new System.Drawing.Point(4, 22);
            this.tab_Family.Name = "tab_Family";
            this.tab_Family.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Family.Size = new System.Drawing.Size(574, 194);
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
            this.OldText,
            this.Id,
            this.OldValue,
            this.IsCombination,
            this.NewValue});
            this.dataGridView_Fmaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Fmaily.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_Fmaily.Name = "dataGridView_Fmaily";
            this.dataGridView_Fmaily.RowHeadersVisible = false;
            this.dataGridView_Fmaily.RowTemplate.Height = 23;
            this.dataGridView_Fmaily.Size = new System.Drawing.Size(568, 188);
            this.dataGridView_Fmaily.TabIndex = 0;
            // 
            // OldText
            // 
            this.OldText.HeaderText = "OldText";
            this.OldText.Name = "OldText";
            this.OldText.Visible = false;
            // 
            // Id
            // 
            this.Id.FillWeight = 40F;
            this.Id.HeaderText = "编号";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // OldValue
            // 
            this.OldValue.FillWeight = 110.6032F;
            this.OldValue.HeaderText = "老字体";
            this.OldValue.Name = "OldValue";
            this.OldValue.ReadOnly = true;
            // 
            // IsCombination
            // 
            this.IsCombination.FillWeight = 114.1617F;
            this.IsCombination.HeaderText = "是否添加组合字体";
            this.IsCombination.MinimumWidth = 7;
            this.IsCombination.Name = "IsCombination";
            this.IsCombination.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsCombination.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // NewValue
            // 
            this.NewValue.FillWeight = 110.6032F;
            this.NewValue.HeaderText = "新字体";
            this.NewValue.Name = "NewValue";
            // 
            // tab_Mtype
            // 
            this.tab_Mtype.AutoScroll = true;
            this.tab_Mtype.Controls.Add(this.dataGridView_Mtype);
            this.tab_Mtype.Location = new System.Drawing.Point(4, 22);
            this.tab_Mtype.Name = "tab_Mtype";
            this.tab_Mtype.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Mtype.Size = new System.Drawing.Size(574, 194);
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
            this.dataGridView_Mtype.Size = new System.Drawing.Size(568, 188);
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
            // tab_Split
            // 
            this.tab_Split.Controls.Add(this.dataGridView_Split);
            this.tab_Split.Location = new System.Drawing.Point(4, 22);
            this.tab_Split.Name = "tab_Split";
            this.tab_Split.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Split.Size = new System.Drawing.Size(574, 238);
            this.tab_Split.TabIndex = 2;
            this.tab_Split.Text = "拆分";
            this.tab_Split.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Split
            // 
            this.dataGridView_Split.AllowUserToAddRows = false;
            this.dataGridView_Split.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Split.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Split.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SplitId,
            this.SplitOldText,
            this.SplitValue});
            this.dataGridView_Split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Split.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_Split.Name = "dataGridView_Split";
            this.dataGridView_Split.RowHeadersVisible = false;
            this.dataGridView_Split.RowTemplate.Height = 23;
            this.dataGridView_Split.Size = new System.Drawing.Size(568, 232);
            this.dataGridView_Split.TabIndex = 0;
            // 
            // SplitId
            // 
            this.SplitId.FillWeight = 15F;
            this.SplitId.HeaderText = "序号";
            this.SplitId.Name = "SplitId";
            this.SplitId.ReadOnly = true;
            // 
            // SplitOldText
            // 
            this.SplitOldText.HeaderText = "Column1";
            this.SplitOldText.Name = "SplitOldText";
            this.SplitOldText.ReadOnly = true;
            this.SplitOldText.Visible = false;
            // 
            // SplitValue
            // 
            this.SplitValue.HeaderText = "读音内容";
            this.SplitValue.Name = "SplitValue";
            this.SplitValue.ReadOnly = true;
            // 
            // demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(590, 393);
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
            this.tab_Split.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Split)).EndInit();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn OldText;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn OldValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsCombination;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewValue;
        private System.Windows.Forms.TabPage tab_Split;
        private System.Windows.Forms.DataGridView dataGridView_Split;
        private System.Windows.Forms.DataGridViewTextBoxColumn SplitId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SplitOldText;
        private System.Windows.Forms.DataGridViewTextBoxColumn SplitValue;
    }
}

