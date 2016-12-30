using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace 多线程低版本
{
    class Program
    {
        static void Main(string[] args)
        {
          
            Thread thread = new Thread(ThreadManger);
            thread.Start();
            thread.Join(1000);

        }


        public static void ThreadManger(Object obj)
        {
            var info = (List<String>)obj;
            int count = info.Count-1;
            for (int i = 0; i < count-1; i++)
            {
                var url = info[i];
                Thread thread = new Thread(ThreadMethod);
                thread.IsBackground = true;
                thread.Start(url);
            }
          
        }
        public static void ThreadMethod(object obj)
        {
            var url = (string)obj;
            Console.WriteLine(url);
        }
        protected void BeginConnect(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                WebRequest webReq = WebRequest.Create(uri);
                webReq.Timeout = 3000;
                WebResponse webRes = webReq.GetResponse();
                HttpWebRequest myReq = (HttpWebRequest)webReq;
                myReq.UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705";
                myReq.Accept = "*/*";
                myReq.KeepAlive = true;
                myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                var result = (HttpWebResponse)myReq.GetResponse();
                Stream receviceStream = result.GetResponseStream();
                StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("utf-8"));
                var  html = readerOfStream.ReadToEnd();
            }
            catch (Exception ex)
            {
              
            }
        }
    }
}
