using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace userCMD
{
    class Program
    {
        static void Main(string[] args)
        {
            IsPortOpened("smtp.vip.sina.com", 25);
            Console.ReadKey();
        }
      static  public string IsPortOpened(string server, int port)
        {
            Process p = null;
            p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            string strOutput = "telnet " + server + " 25";
            p.StandardInput.WriteLine(strOutput);
          
            return "";
        }
    }
}
