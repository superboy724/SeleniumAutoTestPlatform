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

        public void Write(MessageType type, string msg)
        {
            string format = "";

            switch (type)
            {
                case MessageType.Info: format = "Info," + msg; break;
                case MessageType.Warning: format = "Warning," + msg; break;
                case MessageType.Debug: format = "Debug," + msg; break;
                case MessageType.Error: format = "Error," + msg; break;
                case MessageType.AssertSuccess : format = "AssertSuccess," + msg; break;
                case MessageType.AssertFailed: format = "AssertFailed," + msg; break;
                case MessageType.AssertError: format = "AssertError," + msg; break;
                case MessageType.Event: format = "Event," + msg; break;
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
            this.Write(MessageType.Info, msg);
        }

        public void Debug(string msg)
        {
            this.Write(MessageType.Debug, msg);
        }

        public void Warn(string msg)
        {
            this.Write(MessageType.Warning, msg);
        }

        public void Error(string msg)
        {
            this.Write(MessageType.Error, msg);
        }

        public void AssertSuccess(string testCauseName,string testName,long usedTime)
        {
            this.Write(MessageType.AssertSuccess, string.Format("{0},{1},{2}",testCauseName,testName,usedTime));
        }

        public void AssertFailed(string testCauseName, string testName, long usedTime,string trueValue,string falseValue)
        {
            this.Write(MessageType.AssertFailed, string.Format("{0},{1},{2},{3},{4}", testCauseName, testName, usedTime,trueValue,falseValue));
        }

        public void AssertError(string testCauseName, string testName)
        {
            this.Write(MessageType.AssertError, string.Format("{0},{1}", testCauseName, testName));
        }

        public void SendEvent(string eventName,string[] args)
        {
            this.Write(MessageType.Event, eventName + "," + string.Join(",",args));
        }
    }

    public class NewMessageArgs
    {
        public string Msg { get; set; }

        public MessageType Type { get; set; }

        public string FormatString { get; set; }
    }

    public delegate void NewMessageHandler(object e, NewMessageArgs args);


}
