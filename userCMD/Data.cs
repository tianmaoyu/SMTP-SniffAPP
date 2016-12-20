using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace userCMD
{
    public class Data
    {
        //任务队列
        public static  ConcurrentQueue<JObject> Tasks { get; set; }
        //端口列表
        public static ConcurrentBag<int> Ports { set; get; }
      
        //域名，服务器字典
        public static ConcurrentDictionary<string, string>  DomainHost {set;get; }
        //域名，服务器，端口
        public static ConcurrentDictionary<string,Tuple<string,int>> DomainHostPort { set; get; }

        //邮件列表
        public List<string> Emails { set; get; }
        //进度
        public int Count { set; get; }
        public Data()
        {
            if (!File.Exists("配置文件.txt"))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("当前没有配置文件，请先配置");
                Console.ReadKey();
            }
           Emails = File.ReadAllLines("配置文件.txt", Encoding.Default).ToList();
            //DomainHost.GetOrAdd("ddd", "ddd");
        }
    }
}
