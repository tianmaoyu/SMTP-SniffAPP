﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 服务器获取端口扫描
{
    public class TcpClientWithTimeout
    {
        protected string _hostname;
        protected int _port;
        protected int _timeout_milliseconds;
        protected TcpClient connection;
        protected bool connected;
        protected Exception exception;

        public TcpClientWithTimeout(string hostname, int port, int timeout_milliseconds)
        {
            _hostname = hostname;
            _port = port;
            _timeout_milliseconds = timeout_milliseconds;
        }
        public TcpClient Connect()
        {
            // kick off the thread that tries to connect
            connected = false;
            exception = null;
            Thread thread = new Thread(new ThreadStart(BeginConnect));
            // 作为后台线程处理
            thread.IsBackground = true; 
            // 不会占用机器太长的时间
            thread.Start();

            // 次线程在此等待一段时间
            thread.Join(_timeout_milliseconds);
            if (connected == true)
            {
                // 如果成功就返回TcpClient对象
                thread.Abort();
                return connection;
            }
            if (exception != null)
            {
                // 如果失败就抛出错误
                thread.Abort();
                return null;
            }
            else
            {
                // 同样地抛出错误
                thread.Abort();
                return null;
                string message = string.Format("TcpClient connection to {0}:{1} timed out",
                  _hostname, _port);
                throw new TimeoutException(message);
            }
        }
        protected void BeginConnect()
        {
            try
            {
                connection = new TcpClient(_hostname, _port);
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
