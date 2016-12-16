using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using ConsoleApplication1;

namespace testTelnet
{
    enum Verbs
    {
        WILL = 251,
        WONT = 252,
        DO = 253,
        DONT = 254,
        IAC = 255
    }

    enum Options
    {
        SGA = 3
    }


   public class SMTPConnection
    {
        TcpClient tcpSocket;
        int TimeOutMs = 200;
        public SMTPConnection(String Hostname, int Port)
        {
            /*TcpClient*/ tcpSocket = new TcpClientWithTimeout(Hostname, Port, 2000).Connect();
            //tcpSocket = new TcpClient(Hostname, Port);
        }

        public bool Login(string userName, string password, int LoginTimeOutMs)
        {
            try {
                if (!tcpSocket.Connected)
                {
                    return false;
                }
                TimeOutMs = LoginTimeOutMs;
                //链接后第一次等待读取2秒
                string str = ReadFirst(2000);
                if (string.IsNullOrEmpty(str))
                {
                    //无法链接
                    DisConnect();
                    return false;
                }
                WriteLine("EHLO dsfsdfsd");
                WriteLine("AUTH LOGIN");
                WriteLine(ToBase64(userName));
                WriteLine(ToBase64(password));
                str += ReadFirst(1000);
                if (str.ToLower().Contains("successful"))
                {
                    WriteLine("quit");
                    DisConnect();
                    return true;
                    //判断是否登录成功
                }
                //未知错误的
                WriteLine("quit");
                DisConnect();
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            
          
        }

        private void DisConnect()
        {
            if (tcpSocket != null)
            {
                if (tcpSocket.Connected)
                {
                    tcpSocket.Client.Disconnect(true);
                }
                else
                {
                    tcpSocket.Close();
                }
            }
        }

        public void WriteLine(string cmd)
        {
            Write(cmd + "\r\n");
        }

        public void Write(string cmd)
        {
            if (!tcpSocket.Connected) return;

            byte[] buf = System.Text.ASCIIEncoding.ASCII.GetBytes(cmd.Replace("\0xFF", "\0xFF\0xFF"));
            tcpSocket.GetStream().Write(buf, 0, buf.Length);
        }
        //字符转换 str 变成base64
        public string ToBase64(string str)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(str);
            var strBase64 = Convert.ToBase64String(b);
            return strBase64;
        }

        public void WriteBase64(string str)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(str);
            var strBase64 = Convert.ToBase64String(b);
            if (!tcpSocket.Connected) return;
            byte[] buf = System.Text.ASCIIEncoding.ASCII.GetBytes(strBase64.Replace("\0xFF", "\0xFF\0xFF"));
            tcpSocket.GetStream().Write(buf, 0, buf.Length);
        }
        public string ReadFirst(int waitTime)
        {
            if (!tcpSocket.Connected) return null;
            StringBuilder sb = new StringBuilder();
            do
            {
                ParseTelnet(sb);
                System.Threading.Thread.Sleep(waitTime);
            } while (tcpSocket.Available > 0);

            return ConvertToGB2312(sb.ToString());
        }
        public string Read()
        {
            if (!tcpSocket.Connected) return null;
            StringBuilder sb = new StringBuilder();
            do
            {
                ParseTelnet(sb);
                System.Threading.Thread.Sleep(TimeOutMs);
            } while (tcpSocket.Available > 0);

            return ConvertToGB2312(sb.ToString());
        }

        public bool IsConnected
        {
            get { return tcpSocket.Connected; }
        }

        void ParseTelnet(StringBuilder sb)
        {
            while (tcpSocket.Available > 0)
            {
                int input = tcpSocket.GetStream().ReadByte();
                switch (input)
                {
                    case -1:
                        break;
                    case (int)Verbs.IAC:
                        // interpret as command 
                        int inputverb = tcpSocket.GetStream().ReadByte();
                        if (inputverb == -1) break;
                        switch (inputverb)
                        {
                            case (int)Verbs.IAC:
                                //literal IAC = 255 escaped, so append char 255 to string 
                                sb.Append(inputverb);
                                break;
                            case (int)Verbs.DO:
                            case (int)Verbs.DONT:
                            case (int)Verbs.WILL:
                            case (int)Verbs.WONT:
                                // reply to all commands with "WONT", unless it is SGA (suppres go ahead) 
                                int inputoption = tcpSocket.GetStream().ReadByte();
                                if (inputoption == -1) break;
                                tcpSocket.GetStream().WriteByte((byte)Verbs.IAC);
                                if (inputoption == (int)Options.SGA)
                                    tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WILL : (byte)Verbs.DO);
                                else
                                    tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WONT : (byte)Verbs.DONT);
                                tcpSocket.GetStream().WriteByte((byte)inputoption);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        sb.Append((char)input);
                        break;
                }
            }
        }

        private string ConvertToGB2312(string str_origin)
        {
            char[] chars = str_origin.ToCharArray();
            byte[] bytes = new byte[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                int c = (int)chars[i];
                bytes[i] = (byte)c;
            }
            Encoding Encoding_GB2312 = Encoding.GetEncoding("GB2312");
            string str_converted = Encoding_GB2312.GetString(bytes);
            return str_converted;
        }

     
    }

    //自定义超时检测
    public class SMTPConnectionWithTimeout
    {
        protected string _hostname;
        protected int _port;
        protected int _timeout_milliseconds;
        protected SMTPConnection smtpConnection;
        protected bool connected;
        protected Exception exception;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostname">主机</param>
        /// <param name="port">端口</param>
        /// <param name="timeout_milliseconds">超时</param>
        public SMTPConnectionWithTimeout(string hostname, int port, int timeout_milliseconds)
        {
            _hostname = hostname;
            _port = port;
            _timeout_milliseconds = timeout_milliseconds;
        }

        public SMTPConnection smtpConnectionRun()
        {
            connected = false;
            exception = null;
            Thread thread = new Thread(new ThreadStart(BeginConnect));
            thread.IsBackground = true;
            thread.Start();
            thread.Join(_timeout_milliseconds);
            if (connected)
            {
                thread.Abort();
                return smtpConnection;
            }
            if (exception != null)
            {
                thread.Abort();
                throw exception;
            }
            else
            {
                thread.Abort();
                string message = string.Format("链接超时");
                throw new TimeoutException(message);
            }

            return new SMTPConnection(_hostname, _port);
        }
        protected void BeginConnect()
        {
            try
            {
                smtpConnection = new SMTPConnection(_hostname, _port);
                // 标记成功，返回调用者
                connected = true;
            }
            catch (Exception ex)
            {
                // 标记失败
                exception = ex;
            }
        }
    }
}