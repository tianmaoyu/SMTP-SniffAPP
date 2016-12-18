using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace userCMD
{
    class ScanerHelper
    {

        private Network network;
        private Dictionary<string, string> listEmail = new Dictionary<string, string>();
        public ScanerHelper()
        {
            network = new Network();
        }
        //启用异步方式运行
        /// <summary>
        /// 得到，邮件得服务期
        /// </summary>
        /// <param name="eamil"></param>
        /// <returns> 邮件名，和邮件的服务地址</returns>
       public  async Task<Tuple<string, string>>GetEamilAddress(string eamil)
        {
            return await Task.Run(() =>
            {
                string server = network.GetMailServer(eamil);
                listEmail.Add(server,eamil);
                if (!string.IsNullOrEmpty(server))
                {
                    //成功的-保存到文件去
                    using (StreamWriter sw = new StreamWriter(CheckFile("server-success.txt"), true, Encoding.Default))
                    {
                        sw.WriteLine(string.Format("{0}&{1}", eamil, server));
                    }
                }
                else
                {
                    //失败的
                    using (StreamWriter sw = new StreamWriter(CheckFile("server-fail.txt"), true, Encoding.Default))
                    {
                        sw.WriteLine(string.Format("{0}", eamil));
                    }
                }
                return new Tuple<string, string>(eamil, server);
            });
        }
        //如果没有这个文件或文件夹，则创建文件夹，文件
        public string CheckFile(string filePath)
        {
           if(!File.Exists(filePath))
            {
               FileStream fileStream=   File.Create(filePath);
                fileStream.Close();
            }
            return filePath;
        }

        //读取邮件原始文件
        public List<string> GetEmail(string filePath)
        {
            List<string> result = new List<string>();
            result = File.ReadAllLines(filePath).ToList();
            return result;
        }
        //
        //读取服务器文件
        public List<string> GetHost(string filePath)
        {
            List<string> result = new List<string>();
            result = File.ReadAllLines(filePath).ToList();
            return result;
        }
    }
}
