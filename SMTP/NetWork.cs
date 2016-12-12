using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
            return null;
        }

        /// <summary>
        /// 检查邮件是否可以用
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public int CheckEmail(string mailAddress, out string errorInfo)
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
            tcpc.ReceiveTimeout = 3000;
            tcpc.SendTimeout = 3000;
            try
            {
                tcpc.ConnectAsync(mailServer, 25);
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
                    return 403;
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
        public bool CheckPortOpened(int m_port, string m_host)
        {
            string portStr = m_port.ToString();
            if (!string.IsNullOrEmpty(portStr))
            {
                TcpClient tc = new TcpClient();
                tc.SendTimeout = tc.ReceiveTimeout = 2000;
                try
                {
                    tc.ConnectAsync(m_host, m_port);
                    if (tc.Connected)
                    {
                        return true;
                    }
                }
                catch
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



    }
    
}
