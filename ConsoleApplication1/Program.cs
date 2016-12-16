using SMTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public delegate void CallBackDelegate(string message);

    class Program
    {
        private static Mutex mutex;
        static private Network network;
        static List<Tuple<string, string>> listEmail = new List<Tuple<string, string>>();
        //常用端口
        static List<int> commonPorts = new List<int> { 25, 587, 465 };
        //
        static List<int> fixedPorts = new List<int> { 80, };
        static void Main(string[] args)
        {
            mutex = new Mutex();
            //network = new Network();
            Console.WriteLine("开始运行");
            List<string> emials = GetEmail();
            foreach (string email in emials)
            {
                GetEamilAddress(email);
            }
            Console.ReadKey();

            //得到 邮件，和相应的邮件服务器
            List<string> emailAndServers = GetEmailAndServer();

            //多线程执行 30个线程
            ScanPortWithThread(50, emailAndServers);
            Console.WriteLine("运行结束");
            Console.ReadKey();

            foreach (string emailAndServr in emailAndServers)
            {
                string[] _emailAndServr = emailAndServr.Split('&');
                if (_emailAndServr.Length <= 1) continue;
                string _emial = _emailAndServr[0];
                string _servr = _emailAndServr[1];
                if (_emailAndServr.Length > 1)
                {

                    //  Task.Factory.StartNew(
                    //() =>
                    //{
                    //    CheckPortOpened2(_servr, commonPorts[0]);
                    //});

                //CUP 并行执行
                    //Parallel.Invoke(
                    //    () =>
                    //    {
                    //        CheckPortOpened2(_servr, commonPorts[0]);
                    //    },
                    //    () =>
                    //    {
                    //        CheckPortOpened2(_servr, commonPorts[1]);
                    //    },
                    //    () =>
                    //    {
                    //        CheckPortOpened2(_servr, commonPorts[2]);
                    //    }
                    //    );

                    //循环验证 常用的端口
                    //var _25port = IsPortOpened(_servr, commonPorts[0]);

                    //var _587port = IsPortOpened(_servr, commonPorts[1]);
                    //var _465port = IsPortOpened(_servr, commonPorts[2]);
                    //CheckPortOpened(_servr, commonPorts[0]);
                    //CheckPortOpened(_servr, commonPorts[1]);
                    //CheckPortOpened(_servr, commonPorts[2]);
                }
            }
            //string portStr = m_port.ToString();
            //string str = "";
            //{
            //    TcpClient tc = new TcpClient("mail.biz-email.net", 25);
            //    NetworkStream ns = tc.GetStream();
            //    StreamReader sr = new StreamReader(ns);
            //    StreamWriter sw = new StreamWriter(ns);
            //    //tc.NoDelay = true;
            //    //延迟2秒
            //    //tc.ReceiveTimeout = 3000;
            //    try
            //    {
            //        ///延迟2秒
            //        ns.ReadTimeout = 2000;
            //        while ((str = sr.ReadLine()) != null)
            //        {

            //            if (str.Contains("220"))
            //            {
            //                Console.WriteLine("good");
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //    finally
            //    {
            //        tc.Close();
            //        tc = null;
            //    }
            //    Console.ReadKey();
            //}
            Console.ReadKey();
        }
        //开启多线程扫描

         public static void ScanPortWithThread(int count,List<string> emailAndServers)
        {
            //最大192个
            var waitsList = new List<List<EventWaitHandle>>();
            var waits = new List<EventWaitHandle>();
            var waits2 = new List<EventWaitHandle>();
            var waits3 = new List<EventWaitHandle>();
            //重邮件文件中取得 count 个邮件放入 运行
            List<string> _emailAndServers = emailAndServers;

            //启动的线程为 count * 3 
            for(int i = 0; i < count; i++)
            {
                if (_emailAndServers.Count < 1) break;//邮件已经使用完，返回

                string[] _emailAndServr = _emailAndServers[0].Split('&');
                if (_emailAndServr.Length <= 1) {
                    _emailAndServers.Remove(_emailAndServers[0]);//删掉错的
                    continue;
                }
                string _emial = _emailAndServr[0];
                string emailServer = _emailAndServr[1];

                //循环监测 端口
                foreach(int _port in commonPorts)
                {
                    CallBackDelegate callBack = CallBack;
                    var handler = new ManualResetEvent(false);
                    if (waits.Count < 64)
                    {
                        waits.Add(handler);
                    }
                    var falge2 = waits.Count == 64 && waits2.Count < 64;
                    if (falge2)
                    {
                        waits2.Add(handler);
                    }
                    var flage3 = waits2.Count == 64 && waits3.Count < 64;
                    if (flage3)
                    {
                        waits3.Add(handler);
                    }


                    new Thread(new ParameterizedThreadStart(CheckPortOpenedForThread))
                    {
                        //线程的名字
                        Name = "线程" + i.ToString()

                    }.Start(new Tuple<string, int,EventWaitHandle, CallBackDelegate>(emailServer, _port, handler, callBack));
                }

                //把已经运行的删掉
               _emailAndServers.Remove(_emailAndServers[0]);
              
            }
            //等待30个线程都结束后继续执行
            if (waits.Count < 1)
            {
                return;
            }
            WaitHandle.WaitAll(waits.ToArray());
            if (waits2.Count > 1)
            {
                WaitHandle.WaitAll(waits2.ToArray());
            }
            if (waits3.Count > 1)
            {
                WaitHandle.WaitAll(waits3.ToArray());
            }
            ScanPortWithThread( count, _emailAndServers);
        }
      


        //多线程执行的函数
        //自定定义的链接
        static public void CheckPortOpenedForThread(object param)
        {
            var _param= (Tuple<string, int, EventWaitHandle, CallBackDelegate >)param;
            var _host = _param.Item1;
            var _port = _param.Item2;
            var _eventWaitHanld = _param.Item3;
            CallBackDelegate _callBack = _param.Item4 as CallBackDelegate;

            try
            {
                TcpClient connection = new TcpClientWithTimeout(_host, _port, 2000).Connect();
                try
                {
                    ///延迟2秒
                    //stream.ReadTimeout = 3000;
                    if (connection.Connected)
                    {
                        Console.WriteLine(string.Format("{0}--端口：{1}---成功---执行的线程为{2}", _host, _port,Thread.CurrentThread.Name));
                        SaveServerAndPortSuccess(_host, _port.ToString());
                        connection.Close();

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("{0}--端口：{1}---未知错误---执行的线程为{2}", _host, _port, Thread.CurrentThread.Name));
                    SaveServerAndPortFail(_host, _port.ToString());
                    connection.Close();
                    connection = null;
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0}--端口：{1}---链接超时---执行的线程为{2}", _host, _port, Thread.CurrentThread.Name));
                SaveServerAndPortFail(_host, _port.ToString());
            }
            finally
            {
                //信号
                _eventWaitHanld.Set();
            }
          
        }
        //线程后调函数
         static public void CallBack(string str)
        {

        }

        //报存扫描到打开的端口
        private static void SaveServerAndPortSuccess(string server,string port)
        {
            mutex.WaitOne(); //当一个线程正在使用该方法的时候，锁定该方法，使其他线程处于等待状态
            using (StreamWriter sw = new StreamWriter("server-port-success.txt", true, Encoding.Default))
            {
                sw.WriteLine(string.Format("{0}&{1}", server, port));
            }
            mutex.ReleaseMutex(); //使用完了，释放锁，让其他线程继续使用
        }
        //报存扫描到打开的端口
        private static void SaveServerAndPortFail(string server, string port)
        {
            mutex.WaitOne(); //当一个线程正在使用该方法的时候，锁定该方法，使其他线程处于等待状态
            using (StreamWriter sw = new StreamWriter("server-port-fail.txt", true, Encoding.Default))
            {
                sw.WriteLine(string.Format("{0}&{1}", server, port));
            }
            mutex.ReleaseMutex(); //使用完了，释放锁，让其他线程继续使用
        }

        //自定定义的链接
        static public bool CheckPortOpened2(string m_host, int m_port)
        {
            try
            {
                TcpClient connection = new TcpClientWithTimeout(m_host, m_port, 2000).Connect();
                try
                {
                    ///延迟2秒
                    //stream.ReadTimeout = 3000;
                    if (connection.Connected)
                    {
                        Console.WriteLine(string.Format("{0}--端口：{1}---成功",m_host, m_port));
                        SaveServerAndPortSuccess(m_host, m_port.ToString());
                        connection.Close();
                        return true;

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("{0}--端口：{1}---未知错误", m_host, m_port));
                    SaveServerAndPortFail(m_host, m_port.ToString());
                    connection.Close();
                    connection = null;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0}--端口：{1}---链接超时", m_host, m_port));
                SaveServerAndPortFail(m_host, m_port.ToString());
            }
            return false;
        }


        static public bool CheckPortOpened(string m_host, int m_port)
        {

            string portStr = m_port.ToString();
            if (!string.IsNullOrEmpty(portStr))
            {
                TcpClient tc = new TcpClient();
                //延迟2秒
                try
                {
                    tc.NoDelay = true;
                    tc.SendTimeout = 2000;
                    tc.ReceiveTimeout = 2000;
                    tc.Connect(m_host, m_port);
                    if (tc.Connected)
                    {
                        Console.WriteLine("good");
                        tc.Close();
                        return true;

                    }
                    tc.EndConnect(Task.Run(() =>
                    {
                        Console.WriteLine("结束链接");
                    }));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("报错、或者链接超时了");
                    tc.Close();
                    tc = null;
                    return false;
                }
            }
            return false;


        }

        //异步验证端口是是否打开
        static async Task<bool> IsPortOpened(string server, int poit)
        {
            return await Task.Run(() =>
            {
                bool result = CheckPortOpened(server, poit);
                //bool result = network.CheckPortOpened2(server, poit);
                if (result)
                {
                    Console.WriteLine("服务器{0}---端口{1} ::{2}", server, poit, "成功");
                    return true;
                }
                Console.WriteLine("服务器{0}---端口{1} ::{2}", server, poit, "");
                return true;
            });
        }
        //异步验证端口是不是smtp 服务器




        //读取邮件，和对应的服务器地址
        //读取邮件文件
        static public List<string> GetEmailAndServer(string filePath = "")
        {
            List<string> result = new List<string>();
            result = File.ReadAllLines("server-success.txt").ToList();
            return result;
        }

        //把取得的邮件服务器地址写入文件
        public void SaveRsult(Tuple<string, string> tuple)
        {
            //成功的写入
            string emailString = tuple.Item1;
            string emailSever = tuple.Item2;
            if (!string.IsNullOrEmpty(emailSever))
            {
                using (StreamWriter sw = new StreamWriter("server-success.txt", true, Encoding.Default))
                {
                    sw.WriteLine(string.Format("{0}&{1}", emailString, emailSever));
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter("server-fail.txt", true, Encoding.Default))
                {
                    sw.WriteLine(string.Format("{0}", emailString));
                }
            }

            //失败的写入
        }



        //读取邮件文件
        static public List<string> GetEmail()
        {
            List<string> result = new List<string>();
            result = File.ReadAllLines("1000.txt").ToList();
            return result;
        }
        ////全部邮件一次性放入查找
        //public async List<Tuple<string, string>> GetALLAsync(List<string> emails)
        //{
        //    foreach (string email in emails)
        //    {
        //       await  GetEamilAddress(email);
        //    }

        //}




        //启用异步方式运行
        /// <summary>
        /// 得到，邮件得服务期
        /// </summary>
        /// <param name="eamil"></param>
        /// <returns> 邮件名，和邮件的服务地址</returns>
        static async Task<Tuple<string, string>> GetEamilAddress(string eamil)
        {

            return await Task.Run(() =>
             {
                 string server = network.GetMailServer(eamil);
                 listEmail.Add(new Tuple<string, string>(eamil, server));
                 if (!string.IsNullOrEmpty(server))
                 {
                     //成功的-保存到文件去
                     using (StreamWriter sw = new StreamWriter("server-success.txt", true, Encoding.Default))
                     {
                         sw.WriteLine(string.Format("{0}&{1}", eamil, server));
                     }
                 }
                 else
                 {
                     //失败的
                     using (StreamWriter sw = new StreamWriter("server-fail.txt", true, Encoding.Default))
                     {
                         sw.WriteLine(string.Format("{0}", eamil));
                     }
                 }

                 Console.WriteLine("邮件为：{0}----服务器地址为：{1}", eamil, server);
                 return new Tuple<string, string>(eamil, server);
             });
        }

        static async Task<string> MyAsync(int i)
        {
            Console.WriteLine("{0}-异步开始", i);
            var reshult = await MyMethod(i);
            Console.WriteLine("{0}-异步完成", i);
            return reshult;
        }
        static async Task<String> MyMethod(int step)
        {
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("{0}-异步执行步骤：{1}", step, i);
                await Task.Delay(5000);
            }
            return "ss";
        }
    }
}
