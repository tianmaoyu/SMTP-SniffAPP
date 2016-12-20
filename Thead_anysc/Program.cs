using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thead_anysc
{
    class Program
    {
        static ConcurrentBag<string> CCCCCC = new ConcurrentBag<string> { "a", "a", "a", "a", "a", "f", "g", "a", "b", "d", "d", "e", "a", "g" };
        static void Main(string[] args)
        {
          
            ConcurrentBag<int> Ports = new ConcurrentBag<int>();
            //来一个定时器：
            TimerCallback timBC = new TimerCallback(PrintCount);
            Timer t = new Timer(timBC, CCCCCC, 300, 2000);
            RunThread();
            //GetHost("ddd", Ports);
            //GetPort("", Ports);
             dd:
            Console.SetCursorPosition(0, 3);
            var input = Console.ReadLine();

            if (!input.Equals("quit")) ;
            {
                goto dd;
            }

        }
        public static async Task RunThread()
        {
            List<string> lsit = new List<string> { "a", "b", "d", "d", "e", "f", "g", "a", "b", "d", "d", "e", "f", "g" };
             await Task.Run( async () => {
                 while (true)
                 {
                     if (lsit.Count > 1)
                     {
                         await Task.Delay(1000);
                         Console.WriteLine("删除完成");
                         Console.WriteLine("开始执行");
                         await ScanPortWithThread(2, lsit);
                         Console.WriteLine("结束执行");
                     }
                     else
                     {
                         lsit.Remove(lsit[0]);
                         await Task.Delay(1000);
                        
                     }
                 }
              
               
            });
        }

        public static async Task<Tuple<string, string>> GetHost(string eamil, ConcurrentBag<int> Ports)
        {
           
            return await Task.Run(async () => {
                int i = 0;
                do
                {
                    i++;
                    Task T = AsynchronyWhithTPL();
                    Ports.Add(i);
                    Console.SetCursorPosition(0, 0);
                    Console.Write("获取host-----{0}", i);
                    Thread.Sleep(250);
                    await T;
                }
                while (true);
                return new Tuple<string, string>("dd", "ddd");
            });
        }
        public static async Task<Tuple<string, string>> GetPort(string eamil, ConcurrentBag<int> Ports)
        {
            return await Task.Run(() => {
                int i = 0;
                do
                {
                    i++;
                    Ports.Add(i);
                    Console.SetCursorPosition(0, 1);
                    Console.Write("{0}", i);
                    Thread.Sleep(250);
                }
                while (true);
                return new Tuple<string, string>("dd", "ddd");
            });
        }
        static  public void PrintCount(object Ports)
        {
            var port = (ConcurrentBag<string>)Ports;
            //Console.SetCursorPosition(0, 2);
            Console.WriteLine("port个数为：{0}",port.Count);
        }

        static Task AsynchronyWhithTPL()
        {
            var container = new Task(() =>
             {
                 Task<string> t = GetInfoForAsync("TPL 1");
                 t.ContinueWith(DD=> {
                     //Console.WriteLine(t.Result);
                     Task<string> T2 = GetInfoForAsync("TPL 2");
                     T2.ContinueWith(dd => { Console.WriteLine(T2.Result); });
                 });
             });
            container.Start();
            return container;


        }



        public async static Task<string> GetInfoForAsync(string name)
        {
            CCCCCC.Add(name);
            //Console.WriteLine("Task{0} started", name);
            await Task.Delay(2000);
           
            Console.WriteLine("ccccc 的个数为：{0}",CCCCCC.Count);
           return string.Format("Task{0} is running on a thread id{1}", Thread.CurrentThread.ManagedThreadId);
        }

        public static async Task ScanPortWithThread(int count, List<string> emailAndServers)
        {
            if (emailAndServers.Count <= 0) return;
            var  email = emailAndServers[0];
            var waits = new List<EventWaitHandle>();
            for (int i = 0; i < count; i++)
            {
                var handler = new ManualResetEvent(false);
                waits.Add(handler);
                new Thread(new ParameterizedThreadStart(CheckPortOpenedForThread))
                {
                    //线程的名字
                    Name = "线程" + i.ToString()

                }.Start(new Tuple<string, int, EventWaitHandle>(email, 666, handler/*, callBack*/));
            }
            emailAndServers.Remove(emailAndServers[0]);
          
            WaitHandle.WaitAll(waits.ToArray());
            Console.WriteLine("多线程循环了一遍");
           await ScanPortWithThread(count, emailAndServers);
        }
        public static void CheckPortOpenedForThread(object obj)
        {
            var param = (Tuple<string, int, EventWaitHandle /*CallBackDelegate*/>)obj;
            var host = param.Item1;
            var port = param.Item2;
            var eventWaitHanld = param.Item3;
            //CallBackDelegate callBack = param.Item4 as CallBackDelegate;
            CCCCCC.Add(Thread.CurrentThread.ManagedThreadId.ToString());
            try
            {
                Thread.Sleep(2000);
                Console.WriteLine("多线程id==={0}", Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception ex)
            {
              
            }
            finally
            {
                //信号
                eventWaitHanld.Set();
            }

        }

    }
}
