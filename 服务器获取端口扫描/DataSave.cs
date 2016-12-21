using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 服务器获取端口扫描
{
   public class DataSave
    {
        private static Mutex mutex;
        public DataSave()
        {
            mutex = new Mutex();
        }
        //报存得到的服务器地址--成功的
        private void SaveHostSuccess(string domain, string host)
        {
            mutex.WaitOne(); 
            using (StreamWriter sw = new StreamWriter(CheckFile("domain-host-success.txt"), true, Encoding.Default))
            {
                sw.WriteLine(string.Format("{0}${1}", domain, host));
            }
            mutex.ReleaseMutex(); 
        }
        //报存得到的服务器地址 -失败的
        private  void SaveHostFail(string domain)
        {
            mutex.WaitOne();
            using (StreamWriter sw = new StreamWriter(CheckFile("domain-host-fail.txt"), true, Encoding.Default))
            {
                sw.WriteLine(string.Format("{0}", domain));
            }
            mutex.ReleaseMutex(); 
        }

        //报存扫描到打开的端口
        private  void SaveServerAndPortSuccess(string server, string port)
        {
            mutex.WaitOne(); 
            using (StreamWriter sw = new StreamWriter(CheckFile("host-port-success.txt"), true, Encoding.Default))
            {
                sw.WriteLine(string.Format("{0}${1}", server, port));
            }
            mutex.ReleaseMutex(); 
        }
        //报存扫描到打开的端口
        private  void SaveServerAndPortFail(string server, string port)
        {
            mutex.WaitOne(); 
            using (StreamWriter sw = new StreamWriter(CheckFile("host-port-fail.txt"), true, Encoding.Default))
            {
                sw.WriteLine(string.Format("{0}${1}", server, port));
            }
            mutex.ReleaseMutex(); 
        }
        //如果没有这个文件或文件夹，则创建文件夹，文件
        protected string CheckFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                FileStream fileStream = File.Create(filePath);
                fileStream.Close();
            }
            return filePath;

        }
    }
}
