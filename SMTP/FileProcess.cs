using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SMTP
{
    public class FileProcess
    {

        public void EmailGroup(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string parentPath = Path.GetDirectoryName(filePath);
            if (!File.Exists(filePath))
            {
                throw new Exception(string.Format("文件{0}不存在", fileName));
                return;
            }
            string newPath = Path.Combine(parentPath, DateTime.Now.ToShortTimeString() + "-" + "Group" + fileName);
            //StreamWriter sw = new StreamWriter(newPath);

            using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
            {
                var lineStr = sr.ReadToEndAsync();
                string strContent = lineStr.Result;
                //邮件
                //File.ReadAllLines("").ToList();
                List<string> list = new List<string>();
                Regex regex = new Regex(@"@.*");//[A-Za-z].*$
                //Regex regex2 = new Regex(@"\r");
                //var matches2 = regex2.Matches(strContent);
                //var totalCount = matches2.Count;
                var matches = regex.Matches(strContent);
                ParallelQuery lists = matches.AsParallel();
                foreach (Match match in lists)
                {
                    list.Add(match.Value);
                }
                list = list.Distinct().ToList();
                //if (matches.Count > 0)
                //{
                //    foreach (Match match in matches)
                //    {
                //        string _email = match.Value;

                //    }
                //}
            }

        }
        //public string ReaderEmail()

        public void EmailGroup2(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string parentPath = Path.GetDirectoryName(filePath);
            if (!File.Exists(filePath))
            {
                throw new Exception(string.Format("文件{0}不存在", fileName));
                return;
            }
            string newPath = Path.Combine(parentPath, DateTime.Now.ToShortTimeString() + "-" + "Group" + fileName);
            //StreamWriter sw = new StreamWriter(newPath);
            List<String> allList = File.ReadAllLines(filePath, Encoding.Default).ToList();
            var list = allList.Distinct().ToList();
            List<string> earaList = new List<string>();
            Regex regex = new Regex(@"@.*");

            list.AsParallel().ForAll(item =>
            {
                var matche = regex.Match(item);
                var _value = matche.Value;
                if (!string.IsNullOrEmpty(_value))
                {
                    earaList.Add(_value);
                }
                //var _array = item.Split('@');
                //if (_array.Length > 1)
                //{
                //    earaList.Add(_array[1]);
                //}
            }
            );
            earaList = earaList.Distinct().ToList();
            var qqlist=  earaList.Where(item=> item!=null&& item.Contains("qq.com")==true);
            if (qqlist != null)
            {
                var _qqlist = qqlist.ToList();
            }
        }

        public void WriterEmail()
        {

        }
    }
}
