namespace SMTP_PortScaner
{
    partial class Loading
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
            this.label_message = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_message
            // 
            this.label_message.AutoSize = true;
            this.label_message.Location = new System.Drawing.Point(80, 67);
            this.label_message.Name = "label_message";
            this.label_message.Size = new System.Drawing.Size(113, 12);
            this.label_message.TabIndex = 0;
            this.label_message.Text = "请等待有操作在进行";
            // 
            // Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 146);
            this.Controls.Add(this.label_message);
            this.Name = "Loading";
            this.Text = "Loading";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_message;
    }
}