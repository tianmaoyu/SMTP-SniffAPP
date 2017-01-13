using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 网页登录_网易
{
    class WebRequestWithTimeout
    {
        protected string url;
        protected int timeout;
        protected string html=null;
        protected bool connected;
        protected Exception exception;
        public WebRequestWithTimeout(string url,int timeout)
        {
            this.url = url;
            this.timeout = timeout;
        }
        public string Connect()
        {
            connected = false;
            exception = null;
            Thread thread = new Thread(new ThreadStart(BeginConnect));
            thread.IsBackground = true;
            thread.Start();
            //线程在此等待一段时间
            thread.Join(timeout);

            if (connected)
            {
                thread.Abort();
                return html;
            }
            if (exception != null)
            {
                thread.Abort();
                return null;
            }
            else
            {
                thread.Abort();
                string message = string.Format("WebRequest connection to {0} timed out", url);
                throw new TimeoutException(message);
               
            }
        }
       protected void BeginConnect()
        {
            try
            {
                Uri uri = new Uri(url);
                WebRequest webReq = WebRequest.Create(uri);
                webReq.Timeout=3000;
                WebResponse webRes = webReq.GetResponse();
                HttpWebRequest myReq = (HttpWebRequest)webReq;
                myReq.UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705";
                myReq.Accept = "*/*";
                myReq.KeepAlive = true;
                myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                var result = (HttpWebResponse)myReq.GetResponse();
                Stream receviceStream = result.GetResponseStream();
                StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("utf-8"));
                html = readerOfStream.ReadToEnd();
                connected = true;
            }
            catch(Exception ex)
            {
                exception = ex;
            }
        } 
    }
}
