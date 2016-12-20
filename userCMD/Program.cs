using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace userCMD
{
    public delegate void CallBackDelegate(string message);
    delegate void AsynUpdateUI(int step);
    class Program
    {
        //所有的邮件
        List<string> emailData = new List<string>();
        private static Network network;

        private List<Tuple<string, string>> EmailAndHost = new List<Tuple<string, string>>();
        //所有邮件服务器地址，邮件
        private Dictionary<string, string> EmailAndHostAll = new Dictionary<string, string>();
        //件服务器地址，邮件成功的
        private Dictionary<string, string> EmailAndHostSuccess = new Dictionary<string, string>();
        //件服务器地址，邮件失败的的
       static private Dictionary<string, string> EmailAndHostFail = new Dictionary<string, string>();
        //检查过的域名域名列表
        static List<string> Domains = new List<string>();

        //所有的务器地，端口
        private Dictionary<string, string> HostAndPort = new Dictionary<string, string>();
        //服务器，端口 成功的
        private Dictionary<string, string> HostAndPortSuccess = new Dictionary<string, string>();
        //服务器，端口 失败的
        private Dictionary<string, string> HostAndPortFail = new Dictionary<string, string>();

        //常用的端口
        static List<int> commonPorts = new List<int>{ 25};
        private static Mutex mutex;

        //配置信息
        public static int EmailTotal = 0;
        public static int HostTotal = 0;
        static CofingInfo info;

    
        
        static void Main(string[] args)
        {
            network = new Network();
            mutex = new Mutex();
           
             info = ReadConfig();
            if (info == null)
            {
                //直接退出
                goto quit999;
            }
            var isEXcuteComple = ExecuteSearchHost(info);
            if (isEXcuteComple)
            {
                //直接退出
                goto quit999;
            }
            //光标已经在15行了
            var isScanComple = ExecuteScanPort(info);
            if (isScanComple)
            {
                //直接退出
                goto quit999;
            }

            quit999:
            Console.ReadKey();

        }
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
        public static void GetEamilAddress(int count, List<string> emails)
        {
            if (Console.KeyAvailable && System.Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                Console.SetCursorPosition(0, 15);
                Console.WriteLine("你已经暂停---请按回车继续查询");
                Console.ReadKey();
            }

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
                    //
                    continue;
                }
                //如果没有监测过则进行
            
                var handler = new ManualResetEvent(false);
                waits.Add(handler);
                new Thread(new ParameterizedThreadStart(GetEmailHost))
                {
                    //线程的名字
                    Name = "线程" + i.ToString()

                }.Start(new Tuple<string, EventWaitHandle>(email, handler));

            }
            //等待所有的线程都结束后继续执行
            if (waits.Count > 0)
            {
                WaitHandlePlus.WaitALL(waits);
            }
            //提示
            double dd = (EmailTotal - emails.Count) * 100 / EmailTotal;
            Console.SetCursorPosition(0, 13);
            Console.Write("已经查找：{0}", EmailTotal - emails.Count);
            Console.SetCursorPosition(0, 14);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("进度为：{0}%", dd);

            //提示
            GetEamilAddress(count, emails);
        }
        //判断域名是否已经监测过
        public static bool IsCheck(string email)
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
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        public static void GetEmailHost(object obj)
        {
            var param = (Tuple<string, EventWaitHandle>)obj;
            var email = param.Item1;
            var handle = param.Item2;
       
            try
            {
                var host = GetMailServer(email);
                var domain = email.Split('@')[1];
                if (host != null)
                {
                    //线程执行
                    SaveHostSuccess(domain, host);

                }
                else
                {
                    SaveHostFail(domain);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                handle.Set();
            }
        }



        public static string GetMailServer(string strEmail)
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
        public static void ScanPortWithThread(int count, List<string> emailAndServers)
        {
            if (emailAndServers.Count <= 0) return;
            if (Console.KeyAvailable && System.Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                Console.SetCursorPosition(0, 15);
                Console.WriteLine("你已经暂停---请按回车继续查询");
                Console.ReadKey();
            }
            //最大192个
            var waits = new List<EventWaitHandle>();
            List<string> _emailAndServers = emailAndServers;
            for (int i = 0; i < count; i++)
            {
                if (_emailAndServers.Count < 1) break;//邮件已经使用完，返回

                string[] _emailAndServr = _emailAndServers[0].Split('$');
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
                    //CallBackDelegate callBack = CallBack;
                    var handler = new ManualResetEvent(false);
                    waits.Add(handler);
                    new Thread(new ParameterizedThreadStart(CheckPortOpenedForThread))
                    {
                        //线程的名字
                        Name = "线程" + i.ToString()

                    }.Start(new Tuple<string, int, EventWaitHandle/*, CallBackDelegate*/>(emailServer, _port, handler/*, callBack*/));
                }

                //把已经运行的删掉
                _emailAndServers.Remove(_emailAndServers[0]);

            }
            //等待所有的线程都结束后继续执行
            WaitHandlePlus.WaitALL(waits);
            //提示
         
            double dd = (HostTotal - _emailAndServers.Count) * 100 / HostTotal;
            Console.SetCursorPosition(0, 19);
            Console.Write("已经查找：{0}", HostTotal - _emailAndServers.Count);
            Console.SetCursorPosition(0, 20);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("进度为：{0}%", dd);

            ScanPortWithThread(count, _emailAndServers);
        }



        //多线程执行的函数
        //自定定义的链接
        static public void CheckPortOpenedForThread(object obj)
        {
            var param = (Tuple<string, int, EventWaitHandle /*CallBackDelegate*/>)obj;
            var host = param.Item1;
            var port = param.Item2;
            var eventWaitHanld = param.Item3;
            //CallBackDelegate callBack = param.Item4 as CallBackDelegate;

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
                sw.WriteLine(string.Format("{0}${1}", domain, host));
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
                sw.WriteLine(string.Format("{0}${1}", server, port));
            }
            mutex.ReleaseMutex(); //使用完了，释放锁，让其他线程继续使用
        }
        //报存扫描到打开的端口
        private static void SaveServerAndPortFail(string server, string port)
        {
            mutex.WaitOne(); //当一个线程正在使用该方法的时候，锁定该方法，使其他线程处于等待状态
            using (StreamWriter sw = new StreamWriter("host-port-fail.txt", true, Encoding.Default))
            {
                sw.WriteLine(string.Format("{0}${1}", server, port));
            }
            mutex.ReleaseMutex(); //使用完了，释放锁，让其他线程继续使用
        }

        public static CofingInfo ReadConfig()
        {
            if (!File.Exists("配置文件.txt"))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("当前没有配置文件，请先配置");
                Console.ReadKey();
            }
            List<string> configInf = File.ReadAllLines("配置文件.txt", Encoding.Default).ToList();
            CofingInfo info = new CofingInfo();
            info.EmailFile = configInf[0].Split('$')[1];
            info.DomainHostSuccess = configInf[1].Split('$')[1];
            info.DomainHostFail = configInf[2].Split('$')[1];
            info.HostPortSuccess = configInf[3].Split('$')[1];
            info.HostPortFail = configInf[4].Split('$')[1];
            info.ThreadCount = Int32.Parse(configInf[5].Split('$')[1]);
            info.StartLine = Int32.Parse(configInf[6].Split('$')[1]);
            info.CheckCount = Int32.Parse(configInf[7].Split('$')[1]);
            info.IsNoCheckHost = configInf[8].Split('$')[1];
            Console.WriteLine("邮件文件：---------------{0}", info.EmailFile);
            Console.WriteLine("域名-服务器-成功：-------{0}", info.DomainHostSuccess);
            Console.WriteLine("域名-服务器-失败：-------{0}", info.DomainHostFail);
            Console.WriteLine("服务器-端口-成功：-------{0}", info.HostPortSuccess);
            Console.WriteLine("服务器-端口-失败：-------{0}", info.HostPortFail);
            Console.WriteLine("线程数量：---------------{0}", info.ThreadCount);
            Console.WriteLine("开始启动的行数：---------{0}", info.StartLine);
            Console.WriteLine("查询服务器次数：---------{0}", info.CheckCount);
            Console.WriteLine("直接开始端口扫描：-------{0}", info.IsNoCheckHost);

            Console.WriteLine("请检查是否正确，回车键--继续执行");
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter)
            {
                return info;
            }
            else
            {
                return null;
            }
        }


        public static bool ExecuteSearchHost(CofingInfo info)
        {
            bool isBreak = false;
            ConsoleColor colorBack = Console.BackgroundColor;
            ConsoleColor colorFore = Console.ForegroundColor;
            //第一行信息            
            Console.WriteLine("****** 开始检测服务器地址...******");

            //第二行绘制进度条背景            
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; ++i <= 25;)
            {
                Console.Write(" ");
            }
            Console.WriteLine(" ");
            Console.BackgroundColor = colorBack;
            //第三行输出进度            
            Console.WriteLine("0%");
            ScanerHelper scanerHelper = new ScanerHelper();
            var emailData = scanerHelper.GetEmail(info.EmailFile);
            EmailTotal = emailData.Count;
            Console.SetCursorPosition(0, 12);
            Console.Write("扫描的总数为：{0}", EmailTotal);
            
            GetEamilAddress(info.ThreadCount, emailData);
            Console.SetCursorPosition(0, 13);
            Console.Write("服务器地址检测完成");
            quit999:
            return false;
        }

        public static bool ExecuteScanPort(CofingInfo info)
        {
             bool isBreak = false;
            ConsoleColor colorBack = Console.BackgroundColor;
            ConsoleColor colorFore = Console.ForegroundColor;
            //第一行信息            
            Console.WriteLine("****** 端口扫描开始...******");

            //第二行绘制进度条背景            
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; ++i <= 25;)
            {
                Console.Write(" ");
            }
            Console.WriteLine(" ");
            Console.BackgroundColor = colorBack;
            //第三行输出进度            
            Console.WriteLine("0%");//15
            ScanerHelper scanerHelper = new ScanerHelper();
            var hostData = scanerHelper.GetHost(info.DomainHostSuccess);
            HostTotal = hostData.Count;
            Console.Write("扫描的总数为：{0}", HostTotal);
            ScanPortWithThread(info.ThreadCount, hostData);
            Console.WriteLine("****** 端口扫描结束...******");
            quit999:
            return false;

        }

        /// <summary>
        /// mx 方式的host
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        async  Task<Tuple<string,string>> GetHostWithMX(string email)
        {
            return await Task.Run(() =>
            {
                string host = network.GetMailServer(email);
                EmailAndHostAll.Add(host, email);
                var domain = email.Split('@')?[1];
                if (!string.IsNullOrEmpty(host))
                {
                    EmailAndHostSuccess.Add(host, email);
                    //成功的-保存到文件去
                    SaveHostSuccess(domain, host);
                }
                else
                {
                    //失败的
                    EmailAndHostFail.Add(host, email);
                    SaveHostFail(domain);
                }
                return new Tuple<string, string>(email, host);
            });
        }
        async Task<Tuple<string, string>> GetHostWithHTTP(string email,string str)
        {
            return await Task.Run( async() =>
            {
                var domain = email.Split('@')?[1];
                if (domain == null)
                {
                    return new Tuple<string, string>(email, null);
                }
                string host = str + "." + email;
                 bool result = await network.HttpBrowse(host);
                if (result)
                {
                    SaveHostSuccess(domain, host);
                    return new Tuple<string, string>(email, host);
                }
                else
                {
                    SaveHostFail(domain);
                    return new Tuple<string, string>(email, null);
                }
            });
        }


    }
    public class CofingInfo
    {
        public string EmailFile { set; get; }
        public string DomainHostFail { set; get; }
        public string DomainHostSuccess { set; get; }
        public string HostPortSuccess { set; get; }
        public string HostPortFail { set; get; }
        public int ThreadCount { set; get; }
        public int StartLine { set; get; }
        public int CheckCount { set; get; }
        public string IsNoCheckHost { set; get; }

    }
 
   

}
