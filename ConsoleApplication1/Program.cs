using SMTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static private Network network;
        static List<Tuple<string, string>> listEmail = new List<Tuple<string, string>>();
        static void Main(string[] args)
        {
            network = new Network();
            Console.WriteLine("开始运行");
            List<string> emials = GetEmail();
            foreach (string email in emials)
            {
               GetEamilAddress(email);
            }
            Console.ReadKey();
          
        }

        //读取邮件文件
        static public List<string> GetEmail()
        {
            List<string> result = new List<string>();
            result= File.ReadAllLines("1000.txt").ToList();
            return result;
        }
        ////全部邮件一次性放入查找
        //public async List<Tuple<string, string>> GetALLAsync(List<string> emails)
        //{
        //    foreach (string email in emails)
        //    {
        //       await  GetEamilAddress(email);
        //    }
           
        //}




        //启用异步方式运行
        /// <summary>
        /// 得到，邮件得服务期
        /// </summary>
        /// <param name="eamil"></param>
        /// <returns> 邮件名，和邮件的服务地址</returns>
        static async Task<Tuple<string,string>> GetEamilAddress(string eamil)
        {

            return await Task.Run(() =>
             {
                 string server = network.GetMailServer(eamil);
                 listEmail.Add(new Tuple<string, string>(eamil, server));
                 Console.WriteLine("邮件为：{0}----服务器地址为：{1}", eamil, server);
                 return new Tuple<string, string>(eamil, server);
             });
        }

        static async Task<string> MyAsync(int i)
        {
            Console.WriteLine("{0}-异步开始", i);
            var reshult = await MyMethod( i);
            Console.WriteLine("{0}-异步完成",i);
            return reshult;
        }
     static  async Task<String> MyMethod(int step)
        {
            for(int i = 0; i < 6; i++)
            {
                Console.WriteLine("{0}-异步执行步骤：{1}", step, i);
                await Task.Delay(5000);
            }
            return "ss";
        }
    }
}
