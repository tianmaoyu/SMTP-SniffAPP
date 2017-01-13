using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 网页登录_网易
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> userNames = GetFile(this.textBox_user.Text);
            List<string> passwords = GetFile(this.textBox_password.Text);


        }
        //打开网页
        public void OpenUrl(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                WebRequest webReq = WebRequest.Create(uri);
                webReq.Timeout = 3000;
                WebResponse webRes = webReq.GetResponse();
                HttpWebRequest myReq = (HttpWebRequest)webReq;
                myReq.UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705";
                myReq.Accept = "*/*";
                myReq.KeepAlive = true;
                myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                var result = (HttpWebResponse)myReq.GetResponse();
                Stream receviceStream = result.GetResponseStream();
                StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("utf-8"));
                var html = readerOfStream.ReadToEnd();
                
            }
            catch (Exception ex)
            {
            }
        }
        //得到图片
        public Bitmap GetImage(string url, CookieContainer cookies)
        {
            Bitmap result = default(Bitmap);
            try
            {
                Uri uri = new Uri(url);
                WebRequest webReq = WebRequest.Create(uri);
                webReq.Timeout = 3000;
                WebResponse webRes = webReq.GetResponse();
                HttpWebRequest myReq = (HttpWebRequest)webReq;
                myReq.UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705";
                myReq.Accept = "*/*";
                myReq.Host = "service.netease.com";
                myReq.KeepAlive = true;
                myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                myReq.CookieContainer = new CookieContainer();
               
                HttpWebResponse httpWebResponse = (HttpWebResponse)webReq.GetResponse();
                Bitmap bitmap = new Bitmap(httpWebResponse.GetResponseStream());
                httpWebResponse.Close();
                result = bitmap;
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
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
