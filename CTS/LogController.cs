using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTS
{
    public static class LogController
    {
        public static List<string> log = new List<string>();
        public static StreamWriter sw = new StreamWriter("log.txt", true);

        public static string GetLog(int num)
        {
            string s = "";
            for (int i = 0; i < num; i++)
            {
                if (log.Count - 1 - i < 0) break;
                s += log[log.Count - 1 - i] + "\r\n";
            }
            return s;
        }

        public static string GetLog()
        {
            return GetLog(20);
        }

        public static void Add(string s)
        {
            log.Add(s);
            lock (sw)
            {
                sw.Write(s + "\r\n");
                sw.Flush();
            }
        }
    }
}
