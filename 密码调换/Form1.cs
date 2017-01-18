using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 密码调换
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var passwordFile = this.textBox_password.Text;
            var passwords = GetFile(passwordFile);
            StringBuilder sb = new StringBuilder();
            foreach (string password in passwords)
            {
                string []userPsssword= password.Replace("----", "{").Split('{');
                string _password = userPsssword[1];
                string _userName = userPsssword[0];
                if (this.checkBox_number.Checked)
                {
                    sb.AppendLine(RemoveNumber(_password, _userName));
                }
                if (this.checkBox_number.Checked)
                {
                    sb.AppendLine(RemoveLetter(_password, _userName));
                }
                if (this.checkBox_repalce.Checked)
                {
                    string str2 = null;
                    sb.AppendLine(Swap(_password, ref str2, _userName));
                    sb.AppendLine(str2);
                }
                if (sb.Length >100000)
                {
                    SaveData(sb);
                    sb.Clear();
                }
            }
            SaveData(sb);
        }
        //去数字
        public static string RemoveNumber( string str,string username)
        {
            Regex regex = new Regex(@"\d+");

            var result = regex.Replace(str,"");
            return username+"----"+ result;
        }
        //去数字1
        public static string  RemoveNumber(string str)
        {
            Regex regex = new Regex(@"\d+");

            var result = regex.Replace(str, "");
            return result;
        }
        //去字母
        public static string  RemoveLetter(string str, string username)
        {
            Regex regex = new Regex(@"[A-Za-z]+");
            var result = regex.Replace(str, "");
            return username + "----"+result;
        }
        //去字母1
        public static string RemoveLetter(string str)
        {
            Regex regex = new Regex(@"[A-Za-z]+");
            var result = regex.Replace(str, "");
            return  result;
        }
        //数字，字母调换
        public static string Swap(string str,ref string str2, string username)
        {
            //得到字母
            Regex regex = new Regex(@"[A-Za-z]+");
            var matches = regex.Matches(str);
            string letters = null;
            foreach(Match matche in matches)
            {
                letters += matche.Value;
            }
            //得到数字
            Regex regex2 = new Regex(@"\d+");
            var matches2 = regex2.Matches(str);
            string numbers = null;
            foreach (Match matche in matches2)
            {
                numbers += matche.Value;
            }
            var otherString = RemoveNumber(RemoveLetter(str));
            str2 = username + "----"+ numbers + letters+ otherString;
            string result = username + "----"+letters + numbers+ otherString;
            return result;
        }
        //文件保存
        public void SaveData(StringBuilder str)
        {

            using (StreamWriter sw = new StreamWriter(CheckFile("结果.txt"), true, Encoding.Default))
            {
                sw.WriteLine(str.ToString());
            }

        }
        public void SaveData2(StringBuilder sb)
        {

            using (StreamWriter sw = new StreamWriter(CheckFile("去重复的字.txt"), true, Encoding.Default))
            {
                sw.WriteLine(sb.ToString());
            }

        }
        public void SaveData3(StringBuilder sb)
        {

            using (StreamWriter sw = new StreamWriter(CheckFile("拼音名字.txt"), true, Encoding.Default))
            {
                sw.WriteLine(sb.ToString());
            }

        }
        //文件读取
        public List<String> GetFile(string filePath)
        {
            return File.ReadLines(filePath).Distinct().ToList();
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
