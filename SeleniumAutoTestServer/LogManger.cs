using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutoTestServer
{
    public class LogManger
    {
        public static void Write(string msg)
        {
            var time = DateTime.Now.ToString();
            msg = time + " " + msg;
            WriteToConsole(msg);
            WriteToFile(msg);
        }

        public static void WriteToConsole(string msg)
        {
            Console.WriteLine(msg);
        }

        public static void WriteToFile(string msg)
        {

        }

        public static void WriteInfo(string msg)
        {
            Write("Info:" + msg);
        }

        public static void WriteError(string msg)
        {
            Write("Error:" + msg);
        }

        public static void WriteWarning(string msg)
        {
            Write("Warn:" + msg);
        }

        public static void WriteTestLog(string msg)
        {
            Write("FromTest:" + msg);
        }
    }
}
