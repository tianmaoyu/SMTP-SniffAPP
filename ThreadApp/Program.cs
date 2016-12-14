using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ThreadApp
{
    public delegate void CallBackDelegate(string message);

    class Program
    {

        private static Mutex mutex;
        static void Main(string[] args)
        {
            mutex = new Mutex();
            var waits = new List<EventWaitHandle>();
            for (int i = 0; i < 10; i++)
            {
                CallBackDelegate cbd = CallBack;
                var handler = new ManualResetEvent(false);
                waits.Add(handler);
                new Thread(new ParameterizedThreadStart(Print))
                {
                    Name = "H" + i.ToString()
                }.Start(new Tuple<string, EventWaitHandle, CallBackDelegate>("test print:" + i, handler, cbd));
            }
            //WaitHandle.WaitAll(waits.ToArray());
            Console.WriteLine("Completed!");
            Console.Read();

        }

        private static void Print(object param)
        {
            var p = (Tuple<string, EventWaitHandle, CallBackDelegate>)param;
            Console.WriteLine(Thread.CurrentThread.Name + ": Begin!");
            Console.WriteLine(Thread.CurrentThread.Name + ": Print--" + p.Item1);
            Thread.Sleep(300);
            Console.WriteLine(Thread.CurrentThread.Name + ": End!");
            p.Item2.Set();
            CallBackDelegate cbd = p.Item3 as CallBackDelegate;
            cbd(Thread.CurrentThread.Name);
        }
      static public void CallBack(string str)
        {
            Console.WriteLine("执行回调的线程为：" + Thread.CurrentThread.Name+" 返回的结果为："+ str);
        }

        //多线程对一个文件进行操作


    //假设你的操作XML文件的方法为UpdateXml
    void UpdataXml()
    {
        mutex.WaitOne(); //当一个线程正在使用该方法的时候，锁定该方法，使其他线程处于等待状态
         //todosometing         
        mutex.ReleaseMutex(); //使用完了，释放锁，让其他线程继续使用
    }
}
}
