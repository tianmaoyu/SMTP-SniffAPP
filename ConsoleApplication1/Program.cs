using SMTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static private Network network;
        static List<Tuple<string, string>> listEmail = new List<Tuple<string, string>>();
        //常用端口
        static List<int> commonPorts = new List<int> { 25, 587, 465 };
        //
        static List<int> fixedPorts = new List<int> { 80, };
        static void Main(string[] args)
        {
            //network = new Network();
            Console.WriteLine("开始运行");
            //List<string> emials = GetEmail();
            //foreach (string email in emials)
            //{
            //    GetEamilAddress(email);
            //}
            //Console.ReadKey();

            //得到 邮件，和相应的邮件服务器
            List<string> emailAndServers = GetEmailAndServer();

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


                    Parallel.Invoke(
                        () =>
                        {
                            CheckPortOpened2(_servr, commonPorts[0]);
                        },
                        () =>
                        {
                            CheckPortOpened2(_servr, commonPorts[1]);
                        },
                        () =>
                        {
                            CheckPortOpened2(_servr, commonPorts[2]);
                        }
                        );

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

        }

        //自定定义的链接
        static public bool CheckPortOpened2(string m_host, int m_port)
        {
            try
            {
                TcpClient connection = new TcpClientWithTimeout(m_host, m_port, 2000).Connect();
                //NetworkStream stream = connection.GetStream();
                //StreamReader sr = new StreamReader(stream);
                //StreamWriter sw = new StreamWriter(stream);
                //string str = "";
                //tc.NoDelay = true;
                //延迟2秒
                //tc.ReceiveTimeout = 3000;
                try
                {
                    ///延迟2秒
                    //stream.ReadTimeout = 3000;
                    if (connection.Connected)
                    {
                        Console.WriteLine("good");
                        connection.Close();
                        return true;

                    }
                }
                catch (Exception ex)
                {
                    connection.Close();
                    //sr.Close();
                    //sw.Close();
                    connection = null;
                }
                return false;
            }
            catch (Exception ex)
            {

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
