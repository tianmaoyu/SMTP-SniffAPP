//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace userCMD
//{
//    public  class Search
//    {
//        Network network;
//       public Search()
//        {
//            network = new Network();
//        }
//        //开启第一个任务
//        //异步
//        //根据邮件读取SMTP服务器地址
//        public async Task<Tuple<string, string>> GetEamilAddress(string eamil)
//        {
//            return await Task.Run(() =>
//            {
//                string server = network.GetMailServer(eamil);
//                EmailAndHostAll.Add(server, eamil);
//                if (!string.IsNullOrEmpty(server))
//                {
//                    EmailAndHostSuccess.Add(server, eamil);
//                    //成功的-保存到文件去
//                    using (StreamWriter sw = new StreamWriter(CheckFile("server-success.txt"), true, Encoding.Default))
//                    {
//                        sw.WriteLine(string.Format("{0}&{1}", eamil, server));
//                    }
//                }
//                else
//                {
//                    //失败的
//                    EmailAndHostFail.Add(server, eamil);
//                    using (StreamWriter sw = new StreamWriter(CheckFile("server-fail.txt"), true, Encoding.Default))
//                    {
//                        sw.WriteLine(string.Format("{0}", eamil));
//                    }
//                }
//                return new Tuple<string, string>(eamil, server);
//            });
//        }
//    }
//}
