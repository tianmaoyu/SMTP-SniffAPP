using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 服务器获取端口扫描
{
    class Program
    {
        #region 公共数据------------------------------------
        static ConcurrentQueue<Tuple<string, string>> SeachTasksWithHttp = new ConcurrentQueue<Tuple<string, string>>();
        static ConcurrentQueue<string> SeachTasksSecondWithMX = new ConcurrentQueue<string>();
        static ConcurrentQueue<Tuple<string, string>> ScanTasks = new ConcurrentQueue<Tuple<string, string>>();
        static  ConcurrentDictionary<string, string> DomainHost = new ConcurrentDictionary<string, string>();
        static ConcurrentDictionary<string, Tuple<string, string>> DomainHostPort = new ConcurrentDictionary<string, Tuple<string, string>>();
        List<string> Email = new List<string>();
        static ConcurrentBag<int> Ports = new ConcurrentBag<int> { 25, 587, 465 };
        static DataSave dataSave = new DataSave();
        #endregion
        //成功的列表
        static volatile Dictionary<String, String> SuccessDomainHost = new Dictionary<string, string>();
        static volatile Dictionary<string, Tuple<string, int>> SuccessDomainHostPort = new Dictionary<string, Tuple<string, int>>();
        //任务队列
        static volatile Queue<String> MXTaksQueue = new Queue<string>();
        static volatile Queue<Tuple<string, string,string>> HttpTasksQueue = new Queue<Tuple<string, string, string>>();
        static volatile Queue<Tuple<string,string,string,int>> ScanPortTasksQueue = new Queue<Tuple<string,string,string, int>>();
        static volatile List<int> PortList = new List<int> { 25, 587, 465 };
        static UserConfigInfo userInfo = new UserConfigInfo();
        //邮件进度
        static volatile int EmailProgess=0;
        static volatile int EmailTotal = 0;
        //记录
        static int _count = 0;

        static void Main(string[] args)
        {
            EmailTotal = userInfo.Emails.Count;
           TimerCallback timerCallback = new TimerCallback(PrintInfo);
            Timer timer = new Timer(timerCallback, null, 200, 2000);
            int threadCount = userInfo.ThreadCount;
            RunSreachWithMXTask(userInfo);
            RunSreachWithMxTaskSecond(threadCount);
            RunSreachWithHTTPTask(threadCount);
            RunScanPortTask(threadCount);
            Console.ReadKey();
        }

        //打印程序
        public static void PrintInfo(object obj)
        {
            //邮件总是
            //查找到第几行（邮件个数）
            //已经查询的次数
            //查询陈功数量
            //端口扫描次数
            //端口成功的个数 
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("总数：{0}", EmailTotal);
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("进度:{0}", EmailProgess);
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("服务器查询成功：{0}", SuccessDomainHost.Count);
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("端口扫描成功：{0}", SuccessDomainHostPort.Count);
           
        }
        //保存成果
        public static void SaveResult(object obj)
        {

        }

        #region 异步执行的任务-----------------------------------
        //异步运行查询:MX任务
        public static async Task RunSreachWithMXTask(UserConfigInfo userConfigInfo)
        {
            List<string> emails = userConfigInfo.Emails;
            int threadCount = userConfigInfo.ThreadCount;
            await Task.Run(async () =>
            {
                while (true)
                {
                    List<string> _emails;
                    int count = 0;
                    if (emails.Count < 1) break;//运行完成
                    if (emails.Count < 300)
                    {
                        count = emails.Count;
                        _emails = emails.Where((item, index) => index < emails.Count).ToList();
                    }
                    else
                    {
                        count = 300;
                        _emails = emails.Where((item, index) => index < 300).ToList();
                    }
                    //是否暂停
                    if (true)
                    {
                        //
                        await SeachMangerWithMX(threadCount, _emails);
                        emails.RemoveRange(0, count);
                    }
                    else
                    {
                      await  Task.Delay(1000);
                    }
                }
            });
        }

        public static async Task RunSreachWithMxTaskSecond(int threadCount)
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    #region 要进行第二查询得到 emails
                    List<string> emails = new List<string>();
                    for (int i = 0; i <= 300; i++)
                    {
                        if (MXTaksQueue.Count >0)
                        {
                          
                            var str = MXTaksQueue.Dequeue();
                            if (str != null)
                            {
                                emails.Add(str);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion
                    //是否暂停
                    if (true && emails.Count>0)
                    {
                        //开始执行一直到完成为
                        await SeachMangerWithMXSecond(threadCount, emails);
                    }
                    else
                    { 
                       await Task.Delay(2000);
                    }
                }
            });
        }
        //异步运行查询:HTTP任务
        public static async Task RunSreachWithHTTPTask(int countThread)
        {
            await Task.Run(async() =>
            {
               //是否暂停，退出
                while (true)
                {
                    #region 要进行第二查询得到 emails
                    List<Tuple<string,string> > urlMails = new List<Tuple<string,string>>();
                    for (int i = 0; i <= 300; i++)
                    {
                        if (HttpTasksQueue.Count > 0)
                        {
                            //Tuple<string,string> tuple = null;
                            var tuple = HttpTasksQueue.Dequeue();
                            if (tuple != null)
                            {
                                var domain = tuple.Item1;
                                var name = tuple.Item2;
                                var email = tuple.Item3;
                                string url = "http://" + name + "." + domain;
                                urlMails.Add(new Tuple<string, string>(url, email));
                            }
                            
                        }
                        else
                        {
                            break;
                        }

                    }
                    #endregion
                    //队列中是否有任务
                    if (true&& urlMails.Count>1)
                    {
                        await SeachMangerWithHttp(countThread, urlMails);
                    }
                    else
                    {
                        //等到一段时间
                        await Task.Delay(1000);
                    }
                }
            });
        }
        //异步端口扫描任务
        public static async Task RunScanPortTask(int countThread)
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    //队列中是否有任务
                    List<Tuple<string,string,string, int>> hostPort = new List<Tuple<string,string,string, int>>();
                    for(int i=0;i< 300; i++)
                    {
                        if (ScanPortTasksQueue.Count > 0)
                        {
                            var _hostPort =ScanPortTasksQueue.Dequeue();
                            hostPort.Add(_hostPort);
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (hostPort.Count>0)
                    {
                        await ScanPortManger(countThread, hostPort);
                    }
                    else
                    {
                        await Task.Delay(1000);
                    }
                }
            });
        }
        #endregion 任务结束

        #region 多选线程端服务器查询MX方式-----------------------------
        /// 多线程执行服务器查询 MX
        public static async Task SeachMangerWithMX(int count,List<string> emails)
        {
            if (emails.Count < 1) return;
            var waits = new List<EventWaitHandle>();
            for (int i = 0; i < count; i++)
            {
                EmailProgess++;
                //邮件已经使用完，返回
                if (emails.Count < 1) break;
                var email = emails[0];
                emails.Remove(email);
                //检查邮件是否查询过
                var isChecked= IsCheckDomainHost(email);
                if (isChecked)
                {
                    continue;
                }
                var handler = new ManualResetEvent(false);
                waits.Add(handler);
                new Thread(new ParameterizedThreadStart(SeachWithMX))
                    .Start(new Tuple<string, EventWaitHandle>(email, handler));
            }
            if (waits.Count > 0)
            {
                WaitHandlePlus.WaitALL(waits);
            }
            await SeachMangerWithMX(count, emails);
        }
        /// 服务器查询 MX
        public static void SeachWithMX(object obj)
        {
            var param = (Tuple<string, EventWaitHandle>)obj;
            var email = param.Item1;
            var eventWaitHanld = param.Item2;
            try
            {
                var host = Seacher.GetHostWithMX(email);
                var domain = email.Split('@')[1];
                if (host != null)
                {
                    DataSave.SaveHostSuccess(domain, host);
                    //加入扫描任务
                    //ScanTasks.Enqueue(new Tuple<string, string>(domain, host));
                    ScanPortTasksQueue.Enqueue(new Tuple<string,string,string,int>(domain,host,email, 25));
                    if (!SuccessDomainHost.Keys.Contains(domain))
                    {
                        SuccessDomainHost.Add(domain, host);
                    }
                }
                else
                {
                    //SeachTasksSecondWithMX.Enqueue(domain);
                    MXTaksQueue.Enqueue(email);
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                eventWaitHanld.Set();
            }
        }
        #endregion

        #region 多线程查询服务器第二次 MX方式
        /// 多线程执行服务器查询 MX
        public static async Task SeachMangerWithMXSecond(int count, List<string> emails)
        {
            if (emails.Count < 1) return;
            var waits = new List<EventWaitHandle>();
            for (int i = 0; i < count; i++)
            {
                //邮件已经使用完，返回
                if (emails.Count < 1) break;
                var email = emails[0];
                emails.Remove(email);
                //检查邮件是否查询过
                var isChecked = IsCheckDomainHost(email);
                if (isChecked)
                {
                    continue;
                }
                var handler = new ManualResetEvent(false);
                waits.Add(handler);
                new Thread(new ParameterizedThreadStart(SeachWithMXSecond))
                    .Start(new Tuple<string, EventWaitHandle>(email, handler));
            }
            if (waits.Count > 0)
            {
                WaitHandlePlus.WaitALL(waits);
            }
            await SeachMangerWithMXSecond(count, emails);
        }
        /// 服务器查询 MX
        public static void SeachWithMXSecond(object obj)
        {
            var param = (Tuple<string, EventWaitHandle>)obj;
            var email = param.Item1;
            var eventWaitHanld = param.Item2;
          
            try
            {
                var host = Seacher.GetHostWithMX(email);
                var domain = email.Split('@')[1];
                if (host != null)
                {
                    //保存和，加入扫描任务
                    DataSave.SaveHostSuccess(domain, host);
                    ScanTasks.Enqueue(new Tuple<string, string>(domain, host));
                    ScanPortTasksQueue.Enqueue(new Tuple<string,string,string, int>(domain,host, email,25));
                    if (!SuccessDomainHost.Keys.Contains(domain))
                    {
                        SuccessDomainHost.Add(domain, host);
                    }
                }
                else
                {
                    //SeachTasksWithHttp.Enqueue(new Tuple<string, string>(domain,"smtp"));
                    HttpTasksQueue.Enqueue(new Tuple<string,string,string>(domain, "smtp", email));
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                eventWaitHanld.Set();
            }
        }
        #endregion

        #region 多线程执行服务器查询 HTTP方式------------------------
        /// 多线程执行端口扫描管理器 HTTP
        public static async Task SeachMangerWithHttp(int count, List<Tuple<string,string>> urlMails)
        {
            if (urlMails.Count < 1) return;
            var waits = new List<EventWaitHandle>();
            for (int i = 0; i < count; i++)
            {
                //邮件已经使用完，返回
                if (urlMails.Count < 1) break;
                var urlMail = urlMails[0];
                urlMails.Remove(urlMail);
                var handler = new ManualResetEvent(false);
                waits.Add(handler);
                new Thread(new ParameterizedThreadStart(SeachWithHttp))
                    .Start(new Tuple<string,string, EventWaitHandle>(urlMail.Item1,urlMail.Item2, handler));
            }
            if (waits.Count > 0)
            {
                WaitHandlePlus.WaitALL(waits,6000);
            }
            await SeachMangerWithHttp(count, urlMails);
        }
        /// 端口扫描 HTTP
        public static void SeachWithHttp(object obj)
        {
            var param = (Tuple<string,string, EventWaitHandle>)obj;
            var url = param.Item1;
            var email = param.Item2;
            var eventWaitHanld = param.Item3;
            try
            {
                WebRequestWithTimeout webRequestWithTimeout = new WebRequestWithTimeout(url, 2000);
                var html = webRequestWithTimeout.Connect();
                if (html != null)
                {
                    var domain = url.Substring(12);
                    var host = url.Substring(7);
                    DataSave.SaveHostSuccess(domain, host);
                    SuccessDomainHost.Add(domain, host);
                    ScanPortTasksQueue.Enqueue(new Tuple<string,string,string,int>(domain,host, email, 25));
                }
                else
                {
                    if (url.Contains("smtp"))
                    {
                        var domain = url.Substring(12);
                        HttpTasksQueue.Enqueue(new Tuple<string, string,string>(domain, "mail", email));
                    }
                }
           
            }
            catch (Exception ex)
            {
                if (url.Contains("smtp"))
                {
                    var domain = url.Substring(12);
                    HttpTasksQueue.Enqueue(new Tuple<string, string,string>(domain, "mail",email));
                }
            }
            finally
            {
                eventWaitHanld.Set();
            }
        }
        #endregion

        #region 多线程端口扫描----------------------------------
        //多线程端口扫描
        public static async Task ScanPortManger(int count, List<Tuple<string,string,string,int>> hostPorts)
        {
            if (hostPorts.Count < 1) return;
            var waits = new List<EventWaitHandle>();
            for (int i = 0; i < count; i++)
            {
                //邮件已经使用完，返回
                if (hostPorts.Count < 1) break;
                var hostPort = hostPorts[0];
                hostPorts.Remove(hostPort);
                var domian = hostPort.Item1;
                var host = hostPort.Item2;
                var email = hostPort.Item3;
                var port = hostPort.Item4;
                var handler = new ManualResetEvent(false);
                waits.Add(handler);
                new Thread(new ParameterizedThreadStart(ScanPort))
                    .Start(new Tuple<string,string,string,int, EventWaitHandle>(domian,host,email, port, handler));
            }
            if (waits.Count > 0)
            {
                WaitHandlePlus.WaitALL(waits);
            }
            await ScanPortManger(count, hostPorts);
        }
        //端口扫描
        public static void ScanPort(object obj)
        {
            var param = (Tuple<string,string,string,int, EventWaitHandle>)obj;
            var domain = param.Item1;
            var host = param.Item2;
            var email = param.Item3;
            var port = param.Item4;
            var handle = param.Item5;
            try
            {
                TcpClient connection = new TcpClientWithTimeout(host, port, 2000).Connect();
                try
                {
                    if (connection.Connected)
                    {
                        SuccessDomainHostPort.Add(domain, new Tuple<string, int>(host, port));
                        DataSave.SaveServerAndPortSuccess(host, port.ToString(),email);
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                   var index= PortList.IndexOf(port)+1;
                    if(PortList.Count> index + 1)
                    {
                        var _port = PortList[index];
                        ScanPortTasksQueue.Enqueue(new Tuple<string, string,string, int>(domain, host, email, _port));
                    }
                    else
                    {
                        DataSave.SaveServerAndPortFail(host, port.ToString(),email);
                    }
                    connection.Close();
                    connection = null;
                }
            }
            catch (Exception ex)
            {
                var index = PortList.IndexOf(port) + 1;
                if (PortList.Count > index + 1)
                {
                    var _port = PortList[index];
                    ScanPortTasksQueue.Enqueue(new Tuple<string, string,string, int>(domain, host, email, _port));
                }
                else
                {
                    DataSave.SaveServerAndPortFail(host, port.ToString(),email);
                }
            }
            finally
            {
                handle.Set();
            }
        }
        #endregion

        #region 判断监测过 服务器，域名，端口---------------------
        //判断域名是否已经监测过
        public static bool IsCheckDomainHost(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    var domain = email.Split('@')[1];
                    if (DomainHost.ContainsKey(domain))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }
        //判断域名是否已经监测过
        public bool IsCheckDomainHostPort(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                try
                {
                    var domain = email.Split('@')[1];
                    if (DomainHostPort.ContainsKey(domain))
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
        #endregion
    }
}
