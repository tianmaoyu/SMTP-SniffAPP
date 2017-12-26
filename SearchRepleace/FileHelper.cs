using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchRepleace
{

    public class FileHelper
    {
        public static string ReadFile(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileLoadException("文件错误");
            var str = File.ReadAllText(fileName);
            return str;
        }

        public static bool Replace(string fileName, string oldStr, string newStr)
        {
            if (!File.Exists(fileName)) throw new FileLoadException("文件错误");
            var contents = File.ReadAllText(fileName);
            if (!oldStr.Contains(oldStr)) return false;
            contents = contents.Replace(oldStr, newStr);
            File.WriteAllText(fileName, contents, Encoding.UTF8);
            return true;
        }

        public static List<string> MatchStr(string pattern, string fileName)
        {
            var text = FileHelper.ReadFile(fileName);
            var result = new List<string>();
            foreach (Match match in Regex.Matches(text, pattern))
            {
                result.Add(match.Value);
            }
            return result;
        }
    }
}
