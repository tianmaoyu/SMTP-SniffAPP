using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 服务器获取端口扫描
{
    class Program
    {
        static ConcurrentQueue<Tuple<string, string>> SeachTasksWithHttp = new ConcurrentQueue<Tuple<string, string>>();
        #region 公共数据------------------------------------
        static ConcurrentQueue<string> SeachTasksSecondWithMX = new ConcurrentQueue<string>();
        static ConcurrentQueue<Tuple<string, string>> ScanTasks = new ConcurrentQueue<Tuple<string, string>>();
        static  ConcurrentDictionary<string, string> DomainHost = new ConcurrentDictionary<string, string>();
        static ConcurrentDictionary<string, Tuple<string, string>> DomainHostPort = new ConcurrentDictionary<string, Tuple<string, string>>();
        List<string> Email = new List<string>();
        static ConcurrentBag<int> Ports = new ConcurrentBag<int> { 25, 587, 465 };
        static DataSave dataSave = new DataSave();
        #endregion
        static UserConfigInfo userInfo = new UserConfigInfo();
     

        static void Main(string[] args)
        {
           TimerCallback timerCallback = new TimerCallback(PrintInfo);
            Timer timer = new Timer(timerCallback, null, 200, 2000);
            int threadCount = userInfo.ThreadCount;
            RunSreachWithMXTask(userInfo);
            RunSreachWithMxTaskSecond(threadCount);
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
            Console.WriteLine("MX成功数量为：{0}", ScanTasks.Count);
            Console.WriteLine("MX失败数量为：{0}", SeachTasksSecondWithMX.Count);
            Console.WriteLine("HTTP查询等待数量：{0}", SeachTasksWithHttp.Count);
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
                    Console.WriteLine("第二次开始运行");
                    List<string> emails = new List<string>();
                    for (int i = 0; i <= 300; i++)
                    {
                        if (SeachTasksSecondWithMX.Count > 1)
                        {
                            string str = null;
                            var _str = SeachTasksSecondWithMX.TryDequeue(out str);
                            if (str != null)
                            {
                                emails.Add(str);
                            }
                            //emails.Add("123@qq.com");
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
        public static async Task RunSreachWithHTTPTask()
        {
            await Task.Run(async() =>
            {
               //是否暂停，退出
                while (true)
                {
                    #region 要进行第二查询得到 emails
                    Console.WriteLine("HTTP开始运行");
                    List<string> emails = new List<string>();
                    for (int i = 0; i <= 300; i++)
                    {
                        if (SeachTasksSecondWithMX.Count > 1)
                        {
                            string str = null;
                            var _str = SeachTasksSecondWithMX.TryDequeue(out str);
                            if (_str != null)
                            {
                                emails.Add(str);
                            }
                            //emails.Add("123@qq.com");
                        }
                        else
                        {
                            break;
                        }

                    }
                    #endregion
                    //队列中是否有任务
                    if (true)
                    {

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
        public static async Task RunScanPortTask()
        {
            await Task.Run(async () =>
            {
                //是否暂停，退出
                while (true)
                {
                    //队列中是否有任务
                    if (true)
                    {

                    }
                    else
                    {
                        //等到一段时间
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
                    ScanTasks.Enqueue(new Tuple<string, string>(domain, host));
                }
                else
                {
                    SeachTasksSecondWithMX.Enqueue(domain);
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
                    DataSave.SaveHostSuccess(domain, host);
                    //加入扫描任务
                    ScanTasks.Enqueue(new Tuple<string, string>(domain, host));
                }
                else
                {
                    SeachTasksWithHttp.Enqueue(new Tuple<string, string>(domain,"smtp"));
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
        public static async Task SeachMangerWithHttp(int count, List<string> urls)
        {
            if (urls.Count < 1) return;
            var waits = new List<EventWaitHandle>();
            for (int i = 0; i < count; i++)
            {
                //邮件已经使用完，返回
                if (urls.Count < 1) break;
                var url = urls[0];
                urls.Remove(url);
                //检查邮件是否查询过
                if (false)
                {
                    //
                    continue;
                }
                var handler = new ManualResetEvent(false);
                waits.Add(handler);
                new Thread(new ParameterizedThreadStart(SeachWithHttp))
                    .Start(new Tuple<string, EventWaitHandle>(url, handler));
            }
            if (waits.Count > 0)
            {
                WaitHandlePlus.WaitALL(waits);
            }
            await SeachMangerWithHttp(count, urls);
        }
        /// 端口扫描 HTTP
        public static void SeachWithHttp(object obj)
        {
            var param = (Tuple<string, EventWaitHandle>)obj;
            var url = param.Item1;
            var eventWaitHanld = param.Item2;
            try
            {
                WebRequestWithTimeout webRequestWithTimeout = new WebRequestWithTimeout(url, 2000);
                var html = webRequestWithTimeout.Connect();
                Console.WriteLine(html);
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

        #region 多线程端口扫描----------------------------------
        //多线程端口扫描
        public static async Task ScanPortManger(int count, List<string> emails)
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
                if (false)
                {
                    //
                    continue;
                }
                var handler = new ManualResetEvent(false);
                waits.Add(handler);
                new Thread(new ParameterizedThreadStart(SreachHost))
                    .Start(new Tuple<string,int, EventWaitHandle>(email,44, handler));
            }
            if (waits.Count > 0)
            {
                WaitHandlePlus.WaitALL(waits);
            }
            await ScanPortManger(count, emails);
        }
        //端口扫描
        public static void SreachHost(object obj)
        {
            var param = (Tuple<string,int, EventWaitHandle>)obj;
            var email = param.Item1;
            var port = param.Item2;
            var handle = param.Item3;
            try
            {
                var host = Seacher.GetHostWithMX(email);
                var domain = email.Split('@')[1];
                if (host != null)
                {
                    //(Tuple<string, string>{ domain,host });
                    //保存
                    DataSave.SaveHostSuccess(domain, host);
                    ScanTasks.Enqueue(new Tuple<string, string>(domain, host));
                }
                else
                {
                    SeachTasksSecondWithMX.Enqueue(domain);
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
