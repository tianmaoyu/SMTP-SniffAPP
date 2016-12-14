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
using System.Threading.Tasks;

/// <summary>
/// 验证正确的smtp
/// </summary>
namespace validateSMTP
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = GetData();
            var infos=  GetSMTPInfo(data);
            foreach(var info in infos)
            {
                SMTPConnet(info);
            }
        }
        //读取原始数据
        public static List<string> GetData()
        {
            List<string> result = new List<string>();
            result = File.ReadAllLines("smtp-user-password-data.txt").ToList();
            return result;
        }
        //从数据中分析出 ，邮件服务器，用户名，端口，密码
        public static List<Tuple<string, int,string,string, string>> GetSMTPInfo(List<string> list)
        {
            var result = new List<Tuple<string, int, string, string, string>>();
            foreach (string str in list)
            {
                var _str = str.Replace("|%|","$");
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
                if (!Int32.TryParse(_port,out port))
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
        public static string SMTPConnet(Tuple<string, int, string, string, string> info)
        {
            var host = info.Item1;
            var port = info.Item2;
            var userName = info.Item3;
            var password = info.Item4;
            var email = info.Item5;
            //配置服务器端口
            SmtpClient client = new SmtpClient(host, port);
            client.EnableSsl =false;
            client.UseDefaultCredentials = false;
           
            MailAddress from = new MailAddress(email, userName, System.Text.Encoding.UTF8);
            // Set destinations for the e-mail message.
            MailAddress to = new MailAddress("tianmaoyu@foxmail.com");
            // Specify the message content.
            MailMessage message = new MailMessage(from, to);

            client.Credentials = new NetworkCredential(userName, password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            message.Body += Environment.NewLine+"你好";
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "发送测试";
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            try
            {
                //根据验证过程，远程证书无效
                ServicePointManager.ServerCertificateValidationCallback = delegate (Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };

                client.Send(message);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送出现错误");
                return ex.ToString();
            }
            Console.WriteLine("成功发送");
            return "";
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
