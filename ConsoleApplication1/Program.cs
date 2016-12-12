using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程开始运行");
            string cc = "";
            for (int i= 0; i < 9; i++)
            {
                 MyAsync(i);
            }
           
            Console.WriteLine("主线程运行JS");
          
            Console.ReadKey();
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
