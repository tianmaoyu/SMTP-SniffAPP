using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 查找注册与登录
{
    public delegate void DelegateHttpSearch(string str);
    class Program
    {
        static UserConfigInfo userConfigInfo = new UserConfigInfo();
        static volatile int Total = 0;
        static volatile int Progress = -1;
        static volatile int SuccusscCount = 0;
        static volatile int FailCount = 0;
        static Mutex mutex = new Mutex();
        static void Main(string[] args)
        {

            ConcurrentBag<string> domains = new ConcurrentBag<string>();
            #region 数据读取和去重复
            BlockingCollection<string> domians2 = new BlockingCollection<string>();
            Console.WriteLine("开始读取数据,和分类");
            userConfigInfo.Emails.AsParallel().ForAll(item =>
            {
                if (item.Contains("@"))
                {
                    var domian = item.Split('@')[1];
                    domians2.Add(domian);
                }
            });
            userConfigInfo.Emails.Clear();
             List<string>  urls = domians2.Distinct().Select(item => "http://www." + item).ToList();
            using (StreamWriter sw = new StreamWriter(CheckFile("删除重复.txt"), false, Encoding.Default))
            {
                foreach (string url in urls)
                {
                    sw.WriteLine(url);
                }
            }
            if (urls.Count > userConfigInfo.StartLine)
            {
                urls.RemoveRange(0, userConfigInfo.StartLine);
                Progress += userConfigInfo.StartLine;
            }
            Console.WriteLine("数据分类完成，开始查询");
            #endregion 读取去重复完成
            TimerCallback timerCallBack = new TimerCallback(PrintInfo);
            Timer timer = new Timer(timerCallBack, null, 5000, 2000);
            Total = urls.Count;
            int treadCount = userConfigInfo.ThreadCount;
            //SeachMangerWithHttp(userConfigInfo.ThreadCount, urls);
            userConfigInfo.Dispose();
            int timeOut = 3;
            if (userConfigInfo.TimeOut > 0 && userConfigInfo.TimeOut < 10)
            {
                timeOut = userConfigInfo.TimeOut;
            }
            for (int i = 0; i < urls.Count; i++)
            {
                if (urls.Count > userConfigInfo.ThreadCount)
                {
                    var _urls = urls.GetRange(0, userConfigInfo.ThreadCount);
                    urls.RemoveRange(0, userConfigInfo.ThreadCount);
                    ThreadManger(userConfigInfo.ThreadCount, _urls, timeOut*1000);
                }
                else
                {
                    ThreadManger(userConfigInfo.ThreadCount, urls, timeOut * 1000);
                    urls.Clear();
                }
            }
            Console.WriteLine("运行完成");
            Console.ReadKey();
        }


        //打印程序
        public static void PrintInfo(object obj)
        {
            Console.SetCursorPosition(0, 2);
            Console.Write("总数：{0}", Total);
            Console.SetCursorPosition(0, 3);
            Console.Write("成功：{0}", SuccusscCount);
            Console.SetCursorPosition(0, 4);
            Console.Write("失败：{0}", FailCount);
            Console.SetCursorPosition(0, 5);
            Console.Write("进度:{0}", Progress);
            Console.WriteLine();
        }
      

        public static void ThreadManger(int count, List<string> urls,int timeOut)
        {
            Thread thread = new Thread(() => MultiThread(count, urls));
            try
            {
                thread.IsBackground = true;
                thread.Start();
                thread.Join(timeOut);
                thread.Abort();
            }
            catch (Exception ex)
            {
                thread.Abort();
            }

        }
        public static void MultiThread(int count, List<string> urls)
        {
            for (int i = 0; i < count; i++)
            {
                var url = urls[i];
                var thread = new Thread(() => SeachHttp2(url));
                thread.IsBackground = true;
                thread.Start();
            }
        }
        public static void Thread1()
        {
            for (int i = 1; i < 1000; i++)
            {//每运行一个循环就写一个“1”

                Console.Write("1");
            }
        }
        public static void HttpSeacher(object obj)
        {

        }
        /// 多线程执行端口扫描管理器 HTTP
        public static void SeachMangerWithHttp(int count, List<string> urls, int timeOut)
        {
            if (urls.Count < 1) return;
            var waits = new List<EventWaitHandle>();
            for (int i = 0; i < count; i++)
            {
                Progress++;
                //邮件已经使用完，返回
                if (urls.Count < 1) break;
                var url = urls[0];
                urls.Remove(url);
                var handler = new ManualResetEvent(false);
                waits.Add(handler);
                new Thread(new ParameterizedThreadStart(SeachWithHttp))
                    .Start(new Tuple<string, EventWaitHandle>(url, handler));
            }
            if (waits.Count > 0)
            {
                WaitHandlePlus.WaitALL(waits, timeOut * 1000);
            }
            SeachMangerWithHttp(count, urls, timeOut);
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
                if (html != null)
                {
                    SuccusscCount++;
                    if (html.Contains("注册"))
                    {
                        SaveData(url, "注册.txt");
                    }
                    if (html.Contains("登录"))
                    {
                        SaveData(url, "登录.txt");
                    }
                    if (html.Contains("登录") && html.Contains("注册"))
                    {
                        SaveData(url, "登录注册.txt");
                    }
                }
                else
                {
                    FailCount++;
                    SaveData(url, "失败.txt");
                }
            }
            catch (Exception ex)
            {
                FailCount++;
                SaveData(url, "失败.txt");
            }
            finally
            {
                eventWaitHanld.Set();
            }
        }
        //如果没有这个文件或文件夹，则创建文件夹，文件
        static public string CheckFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                FileStream fileStream = File.Create(filePath);
                fileStream.Close();
            }
            return filePath;

        }
        static public void SaveData(string str, string fileName)
        {
            mutex.WaitOne();
            using (StreamWriter sw = new StreamWriter(CheckFile(fileName), true, Encoding.Default))
            {
                sw.WriteLine(str);
            }
            mutex.ReleaseMutex();
        }



        static public void SeachHttp2(string url)
        {
            try
            {
                Progress++;
                WebRequestWithTimeout webRequestWithTimeout = new WebRequestWithTimeout(url, 2000);
                var html = webRequestWithTimeout.Connect();
                if (html != null)
                {
                    SuccusscCount++;
                    if (html.Contains("注册"))
                    {
                        SaveData(url, "注册.txt");
                    }
                    if (html.Contains("登录"))
                    {
                        SaveData(url, "登录.txt");
                    }
                    if (html.Contains("登录") && html.Contains("注册"))
                    {
                        SaveData(url, "登录注册.txt");
                    }
                }
                else
                {
                    FailCount++;
                    SaveData(url, "失败.txt");
                }
            }
            catch (Exception ex)
            {
                FailCount++;
                SaveData(url, "失败.txt");
            }
            finally
            {

            }
        }

    }
}

