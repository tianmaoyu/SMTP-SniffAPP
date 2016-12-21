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
        #region 公共数据------------------------------------
        ConcurrentQueue<Tuple<string, string>> SreachTasks = new ConcurrentQueue<Tuple<string, string>>();
        ConcurrentQueue<Tuple<string, string>> ScanerTasks = new ConcurrentQueue<Tuple<string, string>>();
        ConcurrentDictionary<string, string> DomainHost = new ConcurrentDictionary<string, string>();
        ConcurrentDictionary<string, Tuple<string, string>> DomainHostPort = new ConcurrentDictionary<string, Tuple<string, string>>();
        List<string> Email = new List<string>();
        #endregion

        static void Main(string[] args)
        {
            TimerCallback timerCallback = new TimerCallback(PrintInfo);
            Timer timer = new Timer(timerCallback, null, 200, 2000);
            
        }

        //打印程序
        public static void PrintInfo(object obj)
        {

        }

        #region 异步执行的任务-----------------------------------
        //异步运行查询:MX任务
        public static async Task RunSreachWithMXTask()
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
        //异步运行查询:HTTP任务
        public static async Task RunSreachWithHTTPTask()
        {
            await Task.Run(async() =>
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

        #region 多选线程端口扫描 MX方式-----------------------------
        /// 多线程执行端口扫描管理器 MX
        public static async Task ScanPortMangerWithMX(int count,List<string> emails)
        {
            if (emails.Count < 1) return;
            var waits = new List<EventWaitHandle>();
            for (int i = 0; i < count; i++)
            {
                //邮件已经使用完，返回
                if (emails.Count < 1) break;
                var email = emails[0];
                emails.Remove(email);
                emails.Remove(email);
                //检查邮件是否查询过
                if (false)
                {
                    //
                    continue;
                }
                var handler = new ManualResetEvent(false);
                waits.Add(handler);
                new Thread(new ParameterizedThreadStart(ScanPortWithMX))
                    .Start(new Tuple<string, int, EventWaitHandle>(email, 666, handler));
            }
            if (waits.Count > 0)
            {
                WaitHandlePlus.WaitALL(waits);
            }
            await ScanPortMangerWithMX(count, emails);
        }
        /// 端口扫描 MX
        public static void ScanPortWithMX(object obj)
        {
            var param = (Tuple<string, int, EventWaitHandle>)obj;
            var host = param.Item1;
            var port = param.Item2;
            var eventWaitHanld = param.Item3;
            try
            {

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

        #region 多线程执行端口扫描 HTTP方式------------------------
        /// 多线程执行端口扫描管理器 HTTP
        public static async Task ScanPortMangerWithHttp(int count, List<string> emails)
        {
            if (emails.Count < 1) return;
            var waits = new List<EventWaitHandle>();
            for (int i = 0; i < count; i++)
            {
                //邮件已经使用完，返回
                if (emails.Count < 1) break;
                var email = emails[0];
                emails.Remove(email);
                emails.Remove(email);
                //检查邮件是否查询过
                if (false)
                {
                    //
                    continue;
                }
                var handler = new ManualResetEvent(false);
                waits.Add(handler);
                new Thread(new ParameterizedThreadStart(ScanPortWithHttp))
                    .Start(new Tuple<string, int, EventWaitHandle>(email, 666, handler));
            }
            if (waits.Count > 0)
            {
                WaitHandlePlus.WaitALL(waits);
            }
            await ScanPortMangerWithHttp(count, emails);
        }
        /// 端口扫描 HTTP
        public static void ScanPortWithHttp(object obj)
        {
            var param = (Tuple<string, int, EventWaitHandle>)obj;
            var host = param.Item1;
            var port = param.Item2;
            var eventWaitHanld = param.Item3;
            try
            {

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

        #region 多线程查询服务器----------------------------------
        //多线程服务器查询管理器
        public static async Task SreachHostManger(int count, List<string> emails)
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
                    .Start(new Tuple<string, EventWaitHandle>(email, handler));
            }
            if (waits.Count > 0)
            {
                WaitHandlePlus.WaitALL(waits);
            }
            await SreachHostManger(count, emails);
        }
        //查询服务器
        public static void SreachHost(object obj)
        {
            var param = (Tuple<string, EventWaitHandle>)obj;
            var email = param.Item1;
            var handle = param.Item2;
            try
            {
                //var host = GetMailServer(email);
                var domain = email.Split('@')[1];
                if (false)
                {
                    //线程执行
                   // SaveHostSuccess(domain, host);

                }
                else
                {
                    //SaveHostFail(domain);
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

    }
}
