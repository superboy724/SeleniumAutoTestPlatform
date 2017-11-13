using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutoTestServer
{
    public class TcpMessage
    {
        public event NewMessageHandler NewMessage;
        public string TestCaseName { get; set; }

        public void Write(LogType type, string msg)
        {
            string format = "";

            switch (type)
            {
                case LogType.Info: format = "Info," + msg; break;
                case LogType.Warning: format = "Warning," + msg; break;
                case LogType.Debug: format = "Debug," + msg; break;
                case LogType.Error: format = "Error," + msg; break;
                case LogType.AssertSuccess : format = "AssertSuccess," + msg; break;
                case LogType.AssertFailed: format = "AssertFailed," + msg; break;
                case LogType.AssertError: format = "AssertError," + msg; break;
                case LogType.Event: format = "Event" + msg; break;
            }

            NewMessage(this, new NewMessageArgs()
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

        public void AssertSuccess()
        {
            this.Write(LogType.AssertSuccess, "");
        }

        public void AssertFailed()
        {
            this.Write(LogType.AssertFailed, "");
        }

        public void AssertError()
        {
            this.Write(LogType.AssertError, "");
        }

        public void SendEvent(string eventName)
        {
            this.Write(LogType.Event, eventName);
        }
    }

    public class NewMessageArgs
    {
        public string Msg { get; set; }

        public LogType Type { get; set; }

        public string FormatString { get; set; }
    }

    public delegate void NewMessageHandler(object e, NewMessageArgs args);


}
