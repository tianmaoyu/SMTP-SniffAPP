using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 服务器获取端口扫描
{
    class UserConfigInfo
    {
        public int ThreadCount { set; get; }
        public int StartLine { set; get; }
        public List<string> Emails { set; get; }
        private string emailFile = null;
        public UserConfigInfo()
        {
            ReadConfig();
            if (!File.Exists(emailFile))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("邮件文件不存在");
                Console.ReadKey();
            }
            else
            {
                Emails = File.ReadAllLines(emailFile, Encoding.Default).ToList();
            }
        }
        private void ReadConfig()
        {
            if (!File.Exists("配置文件.txt"))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("当前没有配置文件，请先配置");
                Console.ReadKey();
            }
            List<string> configInf = File.ReadAllLines("配置文件.txt", Encoding.Default).ToList();
            emailFile = configInf[0].Split('$')[1];
            ThreadCount = Int32.Parse(configInf[1].Split('$')[1]);
            StartLine = Int32.Parse(configInf[2].Split('$')[1]);
        }
    }
}
