using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearchRepleace
{

    public class FileHelper
    {
        private static Action<string,string> LogAction = (oldText, newText) =>WriteLog(oldText, newText);

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
            LogAction(oldStr, newStr);
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

        public static List<TextRepace> ReadJSON(string logFileName)
        { 
           var data=   SerializerHelper.JsonReader<List<TextRepace>>(logFileName);
          return data;
        }

        public static  void WriteLog( string oldStr, string newStr, string logFileName = "log.json")
        {
            var list = SerializerHelper.JsonReader<List<TextRepace>>(logFileName);
            if (list == null) list = new List<TextRepace>();
            TextRepace textRepace = new TextRepace();
            textRepace.OldText = oldStr;
            textRepace.NewText = newStr;
            list.Add(textRepace);
            SerializerHelper.JsonWrite<List<TextRepace>>(list,logFileName);
        }


    }
    [Serializable]
    public class TextRepace
    {
        public string OldText { set; get; }
        public string NewText { set; get; }
    }

    public class SerializerHelper
    {
        public static void JsonWrite<T>(T t, string fileName)
        {
            using (StreamWriter streamWriter = File.CreateText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(streamWriter, t);
            }
        }

        public static T JsonReader<T>(string fileName)
        {
            if (!File.Exists(fileName)) return default(T);
            using (StreamReader streamReader = File.OpenText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (T)serializer.Deserialize(streamReader, typeof(T));
            }
        }

    }

}
