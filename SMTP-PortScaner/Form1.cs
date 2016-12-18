using SMTP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMTP_PortScaner
{
    public delegate void CallBackDelegate(string message);
    delegate void AsynUpdateUI(int step);
    public partial class Form1 : Form
    {
        //所有的邮件
        List<string> emailData = new List<string>();
        private Network network;

        private List<Tuple<string, string>> EmailAndHost = new List<Tuple<string, string>>();
        //所有邮件服务器地址，邮件
        private Dictionary<string, string> EmailAndHostAll = new Dictionary<string, string>();
        //件服务器地址，邮件成功的
        private Dictionary<string, string> EmailAndHostSuccess = new Dictionary<string, string>();
        //件服务器地址，邮件失败的的
        private Dictionary<string, string> EmailAndHostFail = new Dictionary<string, string>();
        //检查过的域名域名列表
        List<string> Domains = new List<string>();

        //所有的务器地，端口
        private Dictionary<string, string> HostAndPort = new Dictionary<string, string>();
        //服务器，端口 成功的
        private Dictionary<string, string> HostAndPortSuccess = new Dictionary<string, string>();
        //服务器，端口 失败的
        private Dictionary<string, string> HostAndPortFail = new Dictionary<string, string>();

        //常用的端口
        static List<int> commonPorts = new List<int>();
        private static Mutex mutex;
        public Form1()
        {
            InitializeComponent();
            network = new Network();
            mutex = new Mutex();
        }
        #region 文件选择
        private void comboBox_mailFile_MouseDown(object sender, MouseEventArgs e)
        {
            //OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Multiselect = false;
            //fileDialog.Title = "请选择文件";
            //fileDialog.Filter = "所有文件(*.txt)|*.txt";
            //if (fileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    foreach (string file in fileDialog.FileNames)
            //    {
            //        comboBox_mailFile.Text = "";
            //        comboBox_mailFile.Text += file.ToString();
            //    }
            //}
        }
        private void comboBox_Host_MouseDown(object sender, MouseEventArgs e)
        {
            //OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Multiselect = false;
            //fileDialog.Title = "请选择文件";
            //fileDialog.Filter = "所有文件(*.txt)|*.txt";
            //if (fileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    foreach (string file in fileDialog.FileNames)
            //    {
            //        this.comboBox_host_file.Text = "";
            //        this.comboBox_host_file.Text += file.ToString();
            //    }
            //}
        }
        private void comboBox_Port_MouseDown(object sender, MouseEventArgs e)
        {
            //OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Multiselect = false;
            //fileDialog.Title = "请选择文件";
            //fileDialog.Filter = "所有文件(*.txt)|*.txt";
            //if (fileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    foreach (string file in fileDialog.FileNames)
            //    {
            //        this.comboBox_port_file.Text = "";
            //        this.comboBox_port_file.Text += file.ToString();
            //    }
            //}
        }
        #endregion

        private async void button_start_Click(object sender, EventArgs e)
        {
            //得到文件里面的邮箱
            //string filePath = this.comboBox_mailFile.Text;
            string filePath = textBox2.Text;
            if (!File.Exists(filePath))
            {
                MessageBox.Show("邮件文件不存在：{0}", filePath);
                return;
            }
            ScanerHelper scanerHelper = new ScanerHelper();
       
            emailData = scanerHelper.GetEmail(filePath);
            //的到邮件的个数
            label_scanCount_value.Text = emailData.Count.ToString();
            //得到用户自定义开始读取行
            int startLine = Int32.Parse(textBox_StartLine.Text);
            //得到指定线程数
            int threadCount = Int32.Parse(textBox_thread_value.Text);
            GetEamilAddress(threadCount, emailData);

         
            //根据邮件，得到邮件服务器的地址
            //异步操作
            //foreach (string mail in emailData)
            //{
            //    if (!EmailAndHostAll.Keys.Contains(mail))
            //    {
            //        scanerHelper.GetEamilAddress(mail);
            //    }
            //}


        }
        //初始化一些数据

        //异步
        //根据邮件读取SMTP服务器地址
        public async Task<Tuple<string, string>> GetEamilAddress(string eamil)
        {
            return await Task.Run(() =>
            {
                string server = network.GetMailServer(eamil);
                EmailAndHostAll.Add(server, eamil);
                if (!string.IsNullOrEmpty(server))
                {
                    EmailAndHostSuccess.Add(server, eamil);
                    //成功的-保存到文件去
                    using (StreamWriter sw = new StreamWriter(CheckFile("server-success.txt"), true, Encoding.Default))
                    {
                        sw.WriteLine(string.Format("{0}&{1}", eamil, server));
                    }
                }
                else
                {
                    //失败的
                    EmailAndHostFail.Add(server, eamil);
                    using (StreamWriter sw = new StreamWriter(CheckFile("server-fail.txt"), true, Encoding.Default))
                    {
                        sw.WriteLine(string.Format("{0}", eamil));
                    }
                }
                return new Tuple<string, string>(eamil, server);
            });
        }
       /// <summary>
       /// 多线程的方式取的服务器地址
       /// </summary>
       /// <param name="count"></param>
       /// <param name="emails"></param>
        public void GetEamilAddress(int count, List<string> emails)
        {
            if (count <= 0 || emails.Count < 1) return;
            var waits = new List<EventWaitHandle>();
            List<string> _emails = emails;
            for (int i = 0; i < count; i++)
            {
                if (_emails.Count < 1) break;//邮件已经使用完，返回
                //取得第一个邮件
                string email = _emails[0];
                //删掉第一个
                _emails.Remove(_emails[0]);
                //监测这个域名是不是监测过
                if (!IsCheck(email))
                {
                    var oldValue = this.label_host_success.Text;
                    this.label_host_success.Text = (Int32.Parse(oldValue) + 1).ToString();
                    continue;
                }
                //如果没有监测过则进行
                CallBackDelegate callBack = CallBack;
                var handler = new ManualResetEvent(false);
                waits.Add(handler);
                new Thread(new ParameterizedThreadStart(GetEmailHost))
                {
                    //线程的名字
                    Name = "线程" + i.ToString()

                }.Start(new Tuple<string, EventWaitHandle, CallBackDelegate>(email, handler, callBack));
                //把已经运行的删掉
             

            }
            //等待所有的线程都结束后继续执行
            if (waits.Count > 0)
            {
                WaitHandlePlus.WaitALL(waits);
            }
            GetEamilAddress(count, emails);
        }
         //判断域名是否已经监测过
        public bool IsCheck(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    var domain = email.Split('@')[1];
                    if (Domains.Contains(domain))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        public void GetEmailHost(object obj)
        {
            var param = (Tuple<string, EventWaitHandle, CallBackDelegate>)obj;
            var email = param.Item1;
            var handle = param.Item2;
            var callBack = param.Item3;
            try
            {
               
                var host = GetMailServer(email);
                var domain = email.Split('@')[1];
                if (host != null)
                {
                    //线程执行
                    SaveHostSuccess(domain, host);
                    //this.Invoke(d, "goodd");
                    //跟新界面
                    //this.Invoke((Action)delegate
                    //{
                    //    var oldValue = this.label_host_success.Text;
                    //    this.label_host_success.Text = (Int32.Parse(oldValue) + 1).ToString();
                    //});
                    //callBack("good");
                }
                else
                {
                    SaveHostFail(domain);
                    //this.Invoke(d, "fail");
                    //跟新界面
                    //UpdataHostFail(0);
                    //this.Invoke((Action)delegate
                    //{
                    //    var oldValue = this.label_host_fail.Text;
                    //    this.label_host_fail.Text = (Int32.Parse(oldValue) + 1).ToString();
                    //});
                }
            }catch(Exception ex)
            {
                
            }
            finally
            {
                handle.Set();
            }
        }



        public string GetMailServer(string strEmail)
        {
            string strDomain = strEmail.Split('@')[1];
            ProcessStartInfo info = new ProcessStartInfo();
            info.UseShellExecute = false;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.FileName = "nslookup";
            info.CreateNoWindow = true;
            info.Arguments = "-type=mx " + strDomain;
            Process ns = Process.Start(info);
            StreamReader sout = ns.StandardOutput;
            Regex reg = new Regex("mail exchanger = (?<mailServer>[^\\s].*)");
            string strResponse = "";
            while ((strResponse = sout.ReadLine()) != null)
            {
                Match amatch = reg.Match(strResponse);
                if (reg.Match(strResponse).Success) return amatch.Groups["mailServer"].Value;
            }
            return null;
        }



        //如果没有这个文件或文件夹，则创建文件夹，文件
        public string CheckFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                FileStream fileStream = File.Create(filePath);
                fileStream.Close();
            }
            return filePath;
        }

        //用多线程的方式得到服务器地址


        //根据服务器地址扫描端口
        public void ScanPortWithThread(int count, List<string> emailAndServers)
        {
            //最大192个
            var waits = new List<EventWaitHandle>();
            List<string> _emailAndServers = emailAndServers;
            for (int i = 0; i < count; i++)
            {
                if (_emailAndServers.Count < 1) break;//邮件已经使用完，返回

                string[] _emailAndServr = _emailAndServers[0].Split('&');
                if (_emailAndServr.Length <= 1)
                {
                    _emailAndServers.Remove(_emailAndServers[0]);//删掉错的
                    continue;
                }
                string _emial = _emailAndServr[0];
                string emailServer = _emailAndServr[1];

                //循环监测 端口
                foreach (int _port in commonPorts)
                {
                    CallBackDelegate callBack = CallBack;
                    var handler = new ManualResetEvent(false);
                    waits.Add(handler);
                    new Thread(new ParameterizedThreadStart(CheckPortOpenedForThread))
                    {
                        //线程的名字
                        Name = "线程" + i.ToString()

                    }.Start(new Tuple<string, int, EventWaitHandle, CallBackDelegate>(emailServer, _port, handler, callBack));
                }

                //把已经运行的删掉
                _emailAndServers.Remove(_emailAndServers[0]);

            }
            //等待所有的线程都结束后继续执行
            WaitHandlePlus.WaitALL(waits);
            ScanPortWithThread(count, _emailAndServers);
        }



        //多线程执行的函数
        //自定定义的链接
        static public void CheckPortOpenedForThread(object obj)
        {
            var param = (Tuple<string, int, EventWaitHandle, CallBackDelegate>)obj;
            var host = param.Item1;
            var port = param.Item2;
            var eventWaitHanld = param.Item3;
            CallBackDelegate callBack = param.Item4 as CallBackDelegate;

            try
            {
                //2秒超时则为失败
                TcpClient connection = new TcpClientWithTimeout(host, port, 2000).Connect();
                try
                {
                    ///延迟2秒
                    //stream.ReadTimeout = 3000;
                    if (connection.Connected)
                    {
                        SaveServerAndPortSuccess(host, port.ToString());
                        connection.Close();

                    }
                }
                catch (Exception ex)
                {
                    SaveServerAndPortFail(host, port.ToString());
                    connection.Close();
                    connection = null;
                }

            }
            catch (Exception ex)
            {
                SaveServerAndPortFail(host, port.ToString());
            }
            finally
            {
                //信号
                eventWaitHanld.Set();
            }

        }
        //线程后调函数
         public void CallBack(string str)
        {
           
        }

        //报存得到的服务器地址--成功的
        private static void SaveHostSuccess(string domain, string host)
        {
            mutex.WaitOne(); //当一个线程正在使用该方法的时候，锁定该方法，使其他线程处于等待状态
            using (StreamWriter sw = new StreamWriter("domain-host-success.txt", true, Encoding.Default))
            {
                sw.WriteLine(string.Format("{0}&{1}", domain, host));
            }
            mutex.ReleaseMutex(); //使用完了，释放锁，让其他线程继续使用
        }
        //报存得到的服务器地址 -失败的
        private static void SaveHostFail(string domain)
        {
            mutex.WaitOne(); //当一个线程正在使用该方法的时候，锁定该方法，使其他线程处于等待状态
            using (StreamWriter sw = new StreamWriter("domain-host-fail.txt", true, Encoding.Default))
            {
                sw.WriteLine(string.Format("{0}", domain));
            }
            mutex.ReleaseMutex(); //使用完了，释放锁，让其他线程继续使用
        }



        //报存扫描到打开的端口
        private static void SaveServerAndPortSuccess(string server, string port)
        {
            mutex.WaitOne(); //当一个线程正在使用该方法的时候，锁定该方法，使其他线程处于等待状态
            using (StreamWriter sw = new StreamWriter("host-port-success.txt", true, Encoding.Default))
            {
                sw.WriteLine(string.Format("{0}&{1}", server, port));
            }
            mutex.ReleaseMutex(); //使用完了，释放锁，让其他线程继续使用
        }
        //报存扫描到打开的端口
        private static void SaveServerAndPortFail(string server, string port)
        {
            mutex.WaitOne(); //当一个线程正在使用该方法的时候，锁定该方法，使其他线程处于等待状态
            using (StreamWriter sw = new StreamWriter("host-port-fail.txt", true, Encoding.Default))
            {
                sw.WriteLine(string.Format("{0}&{1}", server, port));
            }
            mutex.ReleaseMutex(); //使用完了，释放锁，让其他线程继续使用
        }

        //更新UI
        private void UpdataHostFail(string step)
        {
            this.label_host_fail.Text = step;
            //if (this.label_host_fail.InvokeRequired)
            //{
            //       this.label_host_fail.Text="fffffffff";
            //     //this.label_host_fail.Text = (Int32.Parse(oldValue) + 1).ToString();
            //}
            //else
            //{
            //    var oldValue = this.label_host_fail.Text;
            //    this.label_host_fail.Text = (Int32.Parse(oldValue) + 1).ToString();
            //}
        }

    }
}
