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
        int Start = 0;
        int End = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start = Int32.Parse(this.textBox_start.Text);
            End = Int32.Parse(this.textBox_end.Text);
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
                            _password = ConverWithCoustomer(_password);
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email,host, _userName1, _password);
                            SaveData(str);
                            continue;
                        }
                        else if (password.Contains("#User#"))
                        {
                            var userName3 = _userName2.Substring(0, 1).ToUpper() + _userName2.Substring(1);
                            var _password = password.Replace("#User#", userName3);
                            _password = ConverWithCoustomer(_password);
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email, host, _userName1, _password);
                            SaveData(str);
                            continue;
                        }
                        else if (password.Contains("#USER#"))
                        {
                           
                            var _password = password.Replace("#USER#", _userName2.ToUpper());
                            _password = ConverWithCoustomer(_password);
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email, host, _userName1, _password);
                            SaveData(str);
                            continue;
                        }
                        else if (password.Contains("#domainleft#"))
                        {
                            var domain = email.Split('@')[1].Split('.')[0];
                            var _password = password.Replace("#domainleft#", domain);
                            _password = ConverWithCoustomer(_password);
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email, host, _userName1, _password);
                            SaveData(str);
                            continue;
                        }
                        else
                        {
                            var  _password = ConverWithCoustomer(password);
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email, host, _userName1, _password);
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
                            _password = ConverWithCoustomer(_password);
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email, host, _userName2, _password);
                            SaveData(str);
                        }
                        else if (password.Contains("#User#"))
                        {
                            var userName3 = _userName2.Substring(0, 1).ToUpper() + _userName2.Substring(1);
                            var _password = password.Replace("#User#", userName3);
                            _password = ConverWithCoustomer(_password);
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email, host, _userName1, _password);
                            SaveData(str);
                            continue;
                        }
                        else if (password.Contains("#USER#"))
                        {

                            var _password = password.Replace("#USER#", _userName2.ToUpper());
                            _password = ConverWithCoustomer(_password);
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email, host, _userName1, _password);
                            SaveData(str);
                            continue;
                        }
                        else if (password.Contains("#domainleft#"))
                        {
                            var domain = email.Split('@')[1].Split('.')[0];
                            var _password = password.Replace("#domainleft#", domain);
                            _password = ConverWithCoustomer(_password);
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email, host, _userName2, _password);
                            SaveData(str);
                        }
                        else
                        {
                            var _password = ConverWithCoustomer(password);
                            string str = string.Format("{0}|%|{1}|%|25|%|-1|%|{2}|%|{3}|%|0|%|0|%|-1|%|", email, host, _userName2, _password);
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

        private void btn_class_Click(object sender, EventArgs e)
        {
            var passwordFile = this.textBox_password_file.Text;
            var passwords = GetFile(passwordFile);
            foreach(string str in passwords)
            {
                if (IsPureNumber(str))
                {
                    SaveData(str, "纯数字.txt");
                    continue;
                }
                if (IsPureLetter(str))
                {
                    SaveData(str, "纯字母.txt");
                    continue;
                }
                SaveData(str, "混合.txt");
            }
        }
        //转换为大写
        public string ConverWithCoustomer(string str)
        {
            int end;
            if (str == ""||str==null) return "";
            string result;
            if (str.Length < End)
            {
                end = str.Length;
            }
            else
            {
                end = End;
            }

            if(Start > str.Length)
            {
                return str;
            }
          
            if (Start > 0 && end >= Start)
            {
                result = str.Substring(Start - 1, end - Start + 1).ToUpper() + str.Substring(end - Start + 1);
            }
            else
            {
                result = str;
            }
            return result;
        }
        //判断纯数字
        public bool IsPureNumber(string str)
        {
            for(int i=0;i< str.Length; i++)
            {
                byte b = Convert.ToByte(str[i]);
                if (b < 48 || b > 57)
                {
                    return false;
                }
            }
            return true;
        }
        //纯字母
        public bool IsPureLetter(string str)
        {
            string _str = str.ToUpper();
            for (int i = 0; i < _str.Length; i++)
            {
                byte b = Convert.ToByte(_str[i]);
                if (b < 65 || b > 90)
                {
                    return false;
                }
            }
            return true;
        }
        //文件保存
        public void SaveData(string str,string fileName)
        {
            using (StreamWriter sw = new StreamWriter(CheckFile(fileName), true, Encoding.Default))
            {
                sw.WriteLine(str);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fixedPassword = this.textBox_fixed_password.Text;
            var randomCount = Int32.Parse(this.textBox_rand_count.Text);
            var randCount = Int32.Parse(this.textBox_rand_count.Text);
            var numberCheck = this.checkBox_number.Checked;
            var letterCheck = this.checkBox_letter.Checked;
            var symboCheck = this.checkBox_Symbol.Checked;
            if (numberCheck)
            {
                int lenght = 1;
                for(int i= 0;i< randCount; i++)
                {
                    lenght *= 10;
                }
                for(int i=0;i< lenght; i++)
                {
                    string password = fixedPassword + i.ToString();
                    SaveData(password, "生成的密码.txt");
                }
            }
            if (letterCheck)
            {

            }
            if (symboCheck)
            {

            }
        }
        //生成数字
        public int GetNumber()
        {
            Random r = new Random();
            var result= r.Next(0,9);
            return result;
        }
    }
}
