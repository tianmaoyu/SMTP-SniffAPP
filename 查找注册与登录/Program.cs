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
            var domains3 = domians2.Distinct().ToList();
            var urls = domains3.Select(item => "http://www." + item).ToList();
            using (StreamWriter sw = new StreamWriter(CheckFile("删除重复.txt"), false, Encoding.Default))
            {
                foreach (string url in urls)
                {
                    sw.WriteLine(url);
                }
            }
            Console.WriteLine("数据分类完成，开始查询");
            #endregion 读取去重复完成
            TimerCallback timerCallBack = new TimerCallback(PrintInfo);
            Timer timer = new Timer(timerCallBack, null, 5000, 2000);
            Total = urls.Count;
            SeachMangerWithHttp(userConfigInfo.ThreadCount, urls);
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
        /// 多线程执行端口扫描管理器 HTTP
        public static void SeachMangerWithHttp(int count, List<string> urls)
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
                WaitHandlePlus.WaitALL(waits, 6000);
            }
            SeachMangerWithHttp(count, urls);
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
    }
}
