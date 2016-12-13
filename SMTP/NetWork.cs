using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SMTP
{
    public class Network
    {

        //得到用户输入的常用端口
        public List<int> ports = new List<int>();
      
        //public Network(string portStr)
        //{
        //    Regex regex = new Regex(@"\d+");
        //    var matches = regex.Matches(portStr);
        //    foreach(Match match in matches)
        //    {
        //        ports.Add(int.Parse(match.Value));
        //    }
        //}



        /// <summary>
        /// 通过邮件名称得到邮件服务器
        /// </summary>
        /// <param name="strEmail"></param>
        /// <returns></returns>
        public string GetMailServer(string strEmail)
        {
            string strDomain = strEmail.Split('@')[1];
            ProcessStartInfo info = new ProcessStartInfo();
            info.UseShellExecute = false;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.FileName = "nslookup";
            info.CreateNoWindow = true;
            info.Arguments = "-type=mx " + strDomain;
            Process ns = Process.Start(info);
            StreamReader sout = ns.StandardOutput;
            Regex reg = new Regex("mail exchanger = (?<mailServer>[^\\s].*)");
            string strResponse = "";
            while ((strResponse = sout.ReadLine()) != null)
            {
                Match amatch = reg.Match(strResponse);
                if (reg.Match(strResponse).Success) return amatch.Groups["mailServer"].Value;
            }
            return "";
        }

        //使用telent 链接的远程 的方式
        public string IsPortOpened(string server,int port)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.UseShellExecute = false;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            //info.FileName = "telnet";
            info.CreateNoWindow = true;
            info.Arguments = "telnet " + server+ " " + port;
            Process ns = Process.Start(info);
            StreamReader sout = ns.StandardOutput;
            Regex reg = new Regex("220");
            string strResponse = "";
            while ((strResponse = sout.ReadLine()) != null)
            {
                Match amatch = reg.Match(strResponse);
                if (reg.Match(strResponse).Success) return "成功:"+ port;
            }
            return "";
        }


        /// <summary>
        /// 检查邮件是否可以用
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public int CheckEmail(string mailAddress, int port, out string errorInfo)
        {
            Regex reg = new Regex("^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\\.[a-zA-Z0-9_-]+)+$");
            if (!reg.IsMatch(mailAddress))
            {
                errorInfo = "Email Format error!";
                return 405;

            }
            string mailServer = GetMailServer(mailAddress);
            if (mailServer == null)
            {
                errorInfo = "Email Server error!";
                return 404;
            }
            TcpClient tcpc = new TcpClient();
            tcpc.NoDelay = true;
            tcpc.ReceiveTimeout = 2000;
            tcpc.SendTimeout = 2000;
            try
            {
                tcpc.Connect(mailServer, port);
                NetworkStream ns = tcpc.GetStream();
                StreamReader sr = new StreamReader(ns, Encoding.Default);
                StreamWriter sw = new StreamWriter(ns, Encoding.Default);
                string strResponse = "";
                string strTestFrom = mailAddress;
                sw.WriteLine("helo " + mailServer);
                sw.WriteLine("mail from:<" + mailAddress + ">");
                sw.WriteLine("rcpt to:<" + strTestFrom + ">");
                strResponse = sr.ReadLine();
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
         
        /// <summary>
        /// 监测远程服务器的端口是否打开
        /// </summary>
        /// <param name="m_port"></param>
        /// <param name="m_host"></param>
        /// <returns></returns>
        public  bool CheckPortOpened(string m_host,int m_port)
        {
            string portStr = m_port.ToString();
            if (!string.IsNullOrEmpty(portStr))
            {
                TcpClient tc = new TcpClient();
                //tc.NoDelay = true;
                //延迟2秒
                tc.ReceiveTimeout = 3000;
                try
                {
                    tc.Connect(m_host, m_port);
                    if (tc.Connected)
                    {
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    return false;
                }
                finally
                {
                    tc.Close();
                    tc = null;
                }
            }
            return false;
        }


        public bool CheckPortOpened2(string m_host, int m_port)
        {
            string portStr = m_port.ToString();
            string str = "";
            if (!string.IsNullOrEmpty(portStr))
            {
                TcpClient tc = new TcpClient(m_host, m_port);
                NetworkStream ns= tc.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                //tc.NoDelay = true;
                //延迟2秒
                //tc.ReceiveTimeout = 3000;
                try
                {
                    ///延迟2秒
                    ns.ReadTimeout = 2000;
                    while ((str = sr.ReadLine())!= null)
                    {
                        
                        if (str.Contains("220"))
                        {
                            return true; ;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    tc.Close();
                    tc = null;
                }
            }
            return false;
        }
        //使用SMTP链接
        public string SMTPConnet(string server,int port, string emailAddress,string password)
        {
            //配置服务器端口
            SmtpClient client = new SmtpClient(server, port);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            //配置邮件名，密码
            client.Credentials = new NetworkCredential(emailAddress, password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send("你好","","","");
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
            return "";
        }

    }
    
}

