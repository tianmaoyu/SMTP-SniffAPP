using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 服务器获取端口扫描
{
   public class WaitHandlePlus
    {
        
        public static bool WaitALL(List<EventWaitHandle> enventWaitHandles)
        {
            Loop(enventWaitHandles);
            return true;
        }

        private static void Loop(List<EventWaitHandle> enventWaitHandles)
        {

            if (enventWaitHandles.Count <= 0) return;
            if (enventWaitHandles.Count > 64)
            {
                List<EventWaitHandle> hanlds = new List<EventWaitHandle>();
                hanlds.AddRange(enventWaitHandles.Where((item, index) => index < 64).ToList());
                WaitHandle.WaitAll(hanlds.ToArray());
                enventWaitHandles.RemoveRange(0,64);
                Loop(enventWaitHandles);
            }
            else
            {
                if (enventWaitHandles.Count > 0)
                {
                    WaitHandle.WaitAll(enventWaitHandles.ToArray());
                }
                return;
            }
           
        }

        //超时设置
        public static bool WaitALL(List<EventWaitHandle> enventWaitHandles,int timeOut)
        {
            Loop(enventWaitHandles,timeOut);
            return true;
        }

        private static void Loop(List<EventWaitHandle> enventWaitHandles, int timeOut)
        {

            if (enventWaitHandles.Count <= 0) return;
            if (enventWaitHandles.Count > 64)
            {
                List<EventWaitHandle> hanlds = new List<EventWaitHandle>();
                hanlds.AddRange(enventWaitHandles.Where((item, index) => index < 64).ToList());
                WaitHandle.WaitAll(hanlds.ToArray(), timeOut);
                enventWaitHandles.RemoveRange(0, 64);
                Loop(enventWaitHandles);
            }
            else
            {
                if (enventWaitHandles.Count > 0)
                {
                    WaitHandle.WaitAll(enventWaitHandles.ToArray(), timeOut);
                }
                return;
            }

        }
    }
}
