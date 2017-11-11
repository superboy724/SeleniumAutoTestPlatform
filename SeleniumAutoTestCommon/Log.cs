using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutoTestCommon
{
    public class Log
    {
        public event NewLogHandler NewLog;
        public string TestCaseName { get; set; }

        public void Write(LogType type,string msg)
        {
            string format = "";

            switch (type)
            {
                case LogType.Info:format = "Info:" + msg;break;
                case LogType.Warning: format = "Warning:" + msg; break;
                case LogType.Debug: format = "Debug:" + msg; break;
                case LogType.Error: format = "Error:" + msg; break;
            }

            NewLog(this, new NewLogArgs()
            {
                FormatString = format,
                Msg = msg,
                Type = type
            });
        }

        public void Info(string msg)
        {
            this.Write(LogType.Info, msg);
        }

        public void Debug(string msg)
        {
            this.Write(LogType.Debug, msg);
        }

        public void Warn(string msg)
        {
            this.Write(LogType.Warning, msg);
        }

        public void Error(string msg)
        {
            this.Write(LogType.Error, msg);
        }
    }

    public class NewLogArgs
    {
        public string Msg { get; set; }

        public LogType Type { get; set; }

        public string FormatString { get; set; }
    }

    public delegate void NewLogHandler(object e, NewLogArgs args);

    
}
