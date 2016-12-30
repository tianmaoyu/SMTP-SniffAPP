using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
      static  ConcurrentBag<string> domains = new ConcurrentBag<string>();
        static void Main(string[] args)
        {

          
            ConcurrentQueue<string> urls2 = new ConcurrentQueue<string>();
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
            //foreach(string str in urls)
            //{
            //    domains.Add(str);
            //}
            Total = urls.Count;
            //urls.Clear();
           
         
            Console.WriteLine("数据分类完成，开始查询");
            #endregion 读取去重复完成
            //TimerCallback timerCallBack = new TimerCallback(PrintInfo);
            //Timer timer = new Timer(timerCallBack, null, 1000, 2000);
            int treadCount = userConfigInfo.ThreadCount;
            int threadCount = userConfigInfo.ThreadCount;
            int timeOut = 3;
            if (userConfigInfo.TimeOut > 0 && userConfigInfo.TimeOut < 10)
            {
                timeOut = userConfigInfo.TimeOut;
            }
            int runCount = 0;
            //RunTask(threadCount, timeOut * 1000);
            while (true)
            {
                if (urls.Count < 1) break;
                List<string> _urls = new List<string>();
                if (urls.Count> treadCount)
                {
                    runCount += treadCount;
                    _urls = urls.GetRange(0, treadCount);
                    urls.RemoveRange(0, treadCount);
                    Run1(treadCount, timeOut, _urls);
                }else
                {
                    Run1(urls.Count, timeOut, _urls);
                    urls.Clear();
                }

                GC.Collect();

            }

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
      
        public static void thread(string url)
        {
            Thread thread = new Thread(() => SeachHttp2(url));
            thread.Start();
            thread.Join(2000);
            try
            {
                thread.Abort();
            }catch(Exception ex)
            {

            }
           
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize( System.Diagnostics.Process.GetCurrentProcess().Handle,- 1, -1);
            }
        }
        public static void Run1(int threadCount, int timeOut,List<string> urls)
        {
            List<Thread> threads = new List<Thread>();
            for(int i = 0; i < urls.Count; i++)
            {
                string url = urls[i];
                Thread thread = new Thread(SeachHttp4);
                thread.Start(url);
                threads.Add(thread);
            }
            Thread.Sleep(timeOut * 1000);
            foreach(Thread thread in threads)
            {
                thread.DisableComObjectEagerCleanup();
                thread.Abort();
            }
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

        async static Task RunTask(int threadCount,int timeOut)
        {
             await Task.Run(() =>
             {
                 while (true)
                 {
                     if (domains.Count > threadCount)
                     {
                         List<string> _urls = new List<string>();
                         for (int j = 0; j < threadCount; j++)
                         {
                             string _str;
                             var result = domains.TryTake(out _str);
                             if (result)
                             {
                                 _urls.Add(_str);
                             }
                           SeachMangerWithHttp(threadCount, _urls, timeOut);
                         }
                     }
                     Console.WriteLine("运行完成");
                 }
             });
        }

        public static void ThreadManger(int count, ConcurrentBag<string> urls,int timeOut)
        {
            Thread thread = new Thread(() => MultiThread(count, urls));
            try
            {
                thread.IsBackground = true;
                thread.Start();
                thread.Join(2000);
                thread.Abort();
            }
            catch (Exception ex)
            {
                thread.Abort();
            }

        }
        //单线程
        public static void threda(string url)
        {
            var thread = new Thread(() => SeachHttp2(url));
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
        }
        public static void MultiThread(int count, ConcurrentBag<string> urls)
        {
            for (int i = 0; i < count; i++)
            {
                string url;
                var result = urls.TryTake(out url);
                if (result)
                {
                    var thread = new Thread(() => SeachHttp2(url));
                    thread.IsBackground = true;
                    thread.Start();
                }
             
            }
        }
      
        /// 多线程执行端口扫描管理器 HTTP
        async public static Task SeachMangerWithHttp(int count, List<string> urls, int timeOut)
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
           await SeachMangerWithHttp(count, urls, timeOut);
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
                    
                    if (html.Contains("注册"))
                    {
                        SuccusscCount++;
                        SaveData(url, "注册.txt");
                    }
                    if (html.Contains("登录"))
                    {
                        SuccusscCount++;
                        SaveData(url, "登录.txt");
                    }
                    if (html.Contains("登录") && html.Contains("注册"))
                    {
                        SuccusscCount++;
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

        static public void SeachHttp3(object obj)
        {
            string url = (string)obj;
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
        static public void SeachHttp4(object obj)
        {
            string url = obj as string;
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
        }
    }
}

