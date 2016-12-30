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

namespace 文本组装
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //得到邮件
            var emailFile = this.textBox_email_file.Text;
            var emails= GetFile(emailFile);
            var passwordFile = this.textBox_password_file.Text;
            var passwords= GetFile(passwordFile);
            var host = this.textBox_host.Text;
            var isAll = this.radioButton_all.Checked;
            #region 邮箱全部
            if (isAll)//全部
            {
                //循环邮箱
                foreach (string email in emails)
                {
                    if (!email.Contains("@")) continue;
                    //循环密码
                    var _userName1 = email;
                    var _userName2 = email.Split('@')[0];
                    foreach (string password in passwords)
                    {
                        if (password.Contains("#user#"))
                        {
                            var _password = password.Replace("#user#", _userName2);
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email,host, _userName1, _password);
                            SaveData(str);
                        }
                        else if (password.Contains("#domainleft#"))
                        {
                            var domain = email.Split('@')[1].Split('.')[0];
                            var _password = password.Replace("#domainleft#", domain);
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email, host, _userName1, _password);
                            SaveData(str);
                        }
                        else
                        {
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email, host, _userName1, password);
                            SaveData(str);
                        }
                    }
                }
            }
            #endregion
            #region @之前
            else
            {
                //循环邮箱
                foreach (string email in emails)
                {
                    if (!email.Contains("@")) continue;
                    //循环密码
                    var _userName1 = email;
                    var _userName2 = email.Split('@')[0];
                    foreach (string password in passwords)
                    {
                        if (password.Contains("#user#"))
                        {
                            var _password = password.Replace("#user#", _userName2);
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email, host, _userName2, _password);
                            SaveData(str);
                        }
                        else if (password.Contains("#domainleft#"))
                        {
                            var domain = email.Split('@')[1].Split('.')[0];
                            var _password = password.Replace("#domainleft#", domain);
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email, host, _userName2, _password);
                            SaveData(str);
                        }
                        else
                        {
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email, host, _userName2, password);
                            SaveData(str);
                        }
                    }
                }
            }
            #endregion

        }
        //文件保存
        public void SaveData(string str)
        {
            using (StreamWriter sw = new StreamWriter(CheckFile("结果.txt"), true, Encoding.Default))
            {
                sw.WriteLine(str);
            }
        }
        //文件读取
        public  List<string> GetFile(string filePath)
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
