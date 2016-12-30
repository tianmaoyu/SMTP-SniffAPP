using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 删除邮件
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var lowNumber = Int32.Parse(this.textBox_low_number.Text);
            var upNumer =Int32.Parse( this.textBox_up_number.Text);
            var passwordFile = this.textBox_mail_file.Text;
            var passwords = GetFile(passwordFile);
            var newPasswords = passwords.Where(item => item.Length > lowNumber && item.Length < upNumer).ToList();
            foreach(string str in newPasswords)
            {
                SaveData(str);
            }
        }
        //文件保存
        public void SaveData(string str)
        {
            using (StreamWriter sw = new StreamWriter(CheckFile("剔除结果.txt"), true, Encoding.Default))
            {
                sw.WriteLine(str);
            }
        }
        //文件读取
        public List<string> GetFile(string filePath)
        {
            var list = File.ReadAllLines(CheckFile(filePath), Encoding.Default).ToList();
            return list;
        }
        public string CheckFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                FileStream fileStream = File.Create(filePath);
                fileStream.Close();
            }
            return filePath;

        }
    }
}
