using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 查找注册与登录
{
    class UserConfigInfo:IDisposable
    {
        public int ThreadCount { set; get; }
        public List<string> Emails { set; get; }
        private string emailFile = null;
        public int TimeOut { set; get; }
              
        public int StartLine = 0;
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
            StartLine = Int32.Parse(configInf[2].Split('$')[1]);
            TimeOut = Int32.Parse(configInf[3].Split('$')[1]);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~UserConfigInfo() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
