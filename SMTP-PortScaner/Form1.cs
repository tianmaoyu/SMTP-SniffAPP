using SMTP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMTP_PortScaner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox_mailFile_MouseDown(object sender, MouseEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.txt)|*.txt";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in fileDialog.FileNames)
                {
                    comboBox_mailFile.Text += file.ToString();
                }
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            FileProcess fileProcess = new FileProcess();
            fileProcess.EmailGroup2(this.comboBox_mailFile.Text);
        }
    }
}
