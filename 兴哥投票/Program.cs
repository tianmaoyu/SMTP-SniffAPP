using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace 兴哥投票
{
    class Program
    {
        static void Main(string[] args)
        {
            var pattrent= "(?<={\"status\").+?(?=})";
            var url = $"http://cvote.gog.cn/Ticket/Data?callback=jQuery111302431248002963058_1514778118706&tid=aro4GNTO3RzmH6ut4CC4dX2PC11sy%2Fej&tkey=C6lcH9Te7MshW3Cfu6LXUBJa2CYZ0Daf&_=";
            Console.WriteLine("输入投票次数");
            var count=int.Parse(  Console.ReadLine());
            for(int i=0;i< count; i++)
            {
                var url1 = $"http://cvote.gog.cn/Ticket/Data?callback=jQuery111302431248002963058_1514778118706&tid=aro4GNTO3RzmH6ut4CC4dX2PC11sy%2Fej&tkey=C6lcH9Te7MshW3Cfu6LXUBJa2CYZ0Daf&_=" + GetTimeStamp();
                var html = OpenUrl(url1);
                if(html!=null&&html.Contains("投票成功")) 
                {
                    var match= Regex.Match(html, pattrent);
                    Console.WriteLine($"{match.Value}");
                    Thread.Sleep(500);
                }
                else
                {
                    count++;
                }
            }
            Console.WriteLine("任意按钮退出");
            Console.ReadKey();
        }

        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds * 1000);
        }

        public static string OpenUrl(string url)
        {
            WebRequest webReq = null;
            HttpWebRequest myReq = null;
            Stream receviceStream = null;
            StreamReader readerOfStream = null;
            try
            {
                Uri uri = new Uri(url);
                 webReq = WebRequest.Create(uri);
                webReq.Timeout = 3000;
                //WebResponse webRes = webReq.GetResponse();
                 myReq = (HttpWebRequest)webReq;
                myReq.UserAgent = "User-Agent:Mozilla/5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B143 Safari/601.1 wechatdevtools/0.7.0 MicroMessenger/6.3.9 Language/zh_CN webview/0";
                myReq.Accept = "*/*";
                myReq.Method = "GET";
                myReq.Host = "cvote.gog.cn";
                myReq.Referer="http://dvote.gog.cn/fdwd.html?from=timeline&isappinstalled=0";
                myReq.KeepAlive = true;
                myReq.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                myReq.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
                var result = (HttpWebResponse)myReq.GetResponse();
                 receviceStream = result.GetResponseStream();
                 readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("utf-8"));
                var html = readerOfStream.ReadToEnd();
                return html;

            }
            catch (Exception ex)
            {
                 webReq = null;
                 myReq = null;
                 receviceStream = null;
                 readerOfStream = null;
            }
            return "";
        }
    }
}
