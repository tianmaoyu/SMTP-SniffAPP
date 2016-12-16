using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace validateSMTP
{
    class ClassTelnet
    {
        TcpClient telnet_tcp_client;

        public string strhost;      // IP 地址  
        public string strusername;  // username  
        public string strpassword;  // password  
        public int port;
        private int ilogin_wait_time = 200; //网络延迟等待时间  
        private int irecv_wait_time = 100; //网络延迟等待时间  

        //Telnet protocal key  
        enum Verbs
        {
            WILL = 251,
            WONT = 252,
            DO = 253,
            DONT = 254,
            IAC = 255
        }
        public ClassTelnet(string host, int port, string userName, string password, string email)
        {
            this.strhost = host;
            this.strusername = userName;
            this.strpassword = password;
            this.strusername = userName;
            this.port = port;
        }
        /** 
         * Telnet 关闭连接 
         */
        public void close_telnet()
        {
            try
            {
                if (telnet_tcp_client == null)
                {
                    return;
                }
                if (telnet_tcp_client.Connected)
                {
                    telnet_tcp_client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.Write("异常");
            }
        }

        /** 
         * Telnet连接到服务器 
         */
        public bool open_connect()
        {
            bool blresult;
            string strtemp;
            blresult = true;
            try
            {
                // new socket  
                telnet_tcp_client = new TcpClient(this.strhost, port);

                System.Threading.Thread.Sleep(ilogin_wait_time);
                // read host info data  
                strtemp = recv_data_from_host();
                if (string.IsNullOrEmpty(strtemp))
                {
                    Console.Write("链接失败");
                    return blresult;
                }
                blresult = send_data_to_host("ehlo BDDDDDDDDDDDOY" + "/n/r");
                strtemp = recv_data_from_host();
                System.Threading.Thread.Sleep(irecv_wait_time);
                blresult = send_data_to_host("auth login" + "/n/r");
                System.Threading.Thread.Sleep(irecv_wait_time);
                strtemp = recv_data_from_host();
                if (!strtemp.Contains("334"))
                {
                    Console.Write("没有返回登录信息");
                    return false;
                }
                // username send to host  
                blresult = send_data_to_host(ToBase64(this.strusername)+ "/n/r");
                strtemp = recv_data_from_host();
                System.Threading.Thread.Sleep(irecv_wait_time);
                blresult = send_data_to_host(ToBase64(this.strpassword) + "/n/r");
                if (blresult == false)
                {
                    Console.Write("username send error");
                    return false;
                }

                System.Threading.Thread.Sleep(irecv_wait_time);
                strtemp = recv_data_from_host();
              
                if (strtemp.ToLower().Contains("successful"))
                {
                    Console.Write("密码用户认证失败了");
                    return false;
                }
                else
                {
                    blresult = false;
                }
            }
            catch (Exception ex)
            {
                blresult = false;
            }
            return blresult;
        }


        /// <summary>
        ///   //字符转换 str 变成base64
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ToBase64(string str)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(str);
            var strBase64 = Convert.ToBase64String(b);
            return strBase64;
        }
         
        public bool send_data_to_host(string strcmd)
        {
            try
            {
                // socket error时、return  
                if (!telnet_tcp_client.Connected)
                {
                    return false;
                }

                byte[] bbuf = System.Text.ASCIIEncoding.ASCII.GetBytes(strcmd.Replace("/0xFF", "/0xFF/0xFF"));

                telnet_tcp_client.GetStream().Write(bbuf, 0, bbuf.Length);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /** 
         * Telnet从主机接受数据 
         */
        public string recv_data_from_host()
        {
            int iinput_data;    //data  
            int inputverb;
            int inputoption;
            StringBuilder sbtemp;
            NetworkStream ns_temp;
            byte[] bread_buffer;
            StringBuilder sbcomplete_message;
            int iread_bytes_num;

            sbtemp = new StringBuilder();

            // socket没有连接的时候，返回空  
            if (!telnet_tcp_client.Connected)
            {
                return null;
            }

            do
            {
                // read 1 byte  
                iinput_data = telnet_tcp_client.GetStream().ReadByte();
                switch (iinput_data)
                {
                    case -1:
                        break;
                    case (int)Verbs.IAC: // 接受的数据有keyword  

                        // read 1 byte  
                        inputverb = telnet_tcp_client.GetStream().ReadByte();
                        if (inputverb == -1) break;
                        switch (inputverb)
                        {
                            case (int)Verbs.IAC:
                                sbtemp.Append(inputverb);
                                break;
                            case (int)Verbs.DO:
                            case (int)Verbs.DONT:
                            case (int)Verbs.WILL:
                            case (int)Verbs.WONT:
                                inputoption = telnet_tcp_client.GetStream().ReadByte();
                                if (inputoption == -1) break;
                                telnet_tcp_client.GetStream().WriteByte((byte)Verbs.IAC);
                                telnet_tcp_client.GetStream().WriteByte(inputverb ==
                                (int)Verbs.DO ? (byte)Verbs.WONT : (byte)Verbs.DONT);
                                telnet_tcp_client.GetStream().WriteByte((byte)inputoption);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        sbtemp.Append((char)iinput_data);
                        bread_buffer = new byte[8192];
                        sbcomplete_message = new StringBuilder();
                        iread_bytes_num = 0;
                        ns_temp = telnet_tcp_client.GetStream();
                        if (ns_temp.CanRead)
                        {
                            System.Threading.Thread.Sleep(ilogin_wait_time);
                            iread_bytes_num = ns_temp.Read(bread_buffer, 0, bread_buffer.Length);
                            sbtemp.AppendFormat("{0}", Encoding.ASCII.GetString(bread_buffer,
                                                0, iread_bytes_num));
                        }
                        break;
                }

                // timeout  
                System.Threading.Thread.Sleep(irecv_wait_time);
            } while (telnet_tcp_client.Available > 0);

            // 返回接受的数据  
            return sbtemp.ToString();
        }
       
    }
}
