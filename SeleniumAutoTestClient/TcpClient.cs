using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SeleniumAutoTestClient
{
    public class TcpClient
    {
        string ip;

        Socket socket;

        public const int PORT = 1919;

        public Dictionary<string, Action<string[]>> messageBuffer;

        public event NewMessageHandle NewMessageEvent;

        public TcpClient(string ip)
        {
            this.messageBuffer = new Dictionary<string, Action<string[]>>();
            this.ip = ip;
        }

        /// <summary>
        /// 连接到远程服务器
        /// </summary>
        public void Connect()
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipaddress = IPAddress.Parse(ip);
            IPEndPoint ipendpoint = new IPEndPoint(ipaddress, 1919);
            this.socket.Connect(ipendpoint);
            Thread t = new Thread(new ThreadStart(MessageReceive));
            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        /// 消息接收
        /// </summary>
        public void MessageReceive()
        {
            try
            {
                while(true)
                {
                    byte[] buffer = new byte[4096];
                    int msgLength = this.socket.Receive(buffer);
                    byte[] data = new byte[msgLength];
                    Array.Copy(buffer, data, msgLength);

                    //byte to utf8
                    var str = Encoding.UTF8.GetString(data);
                    //解析数据
                    string[] commandAndArgs = str.Split(':');
                    string command = commandAndArgs[0];
                    string[] args = commandAndArgs[1].Split(',');
                    if (command != "MSG")
                    {
                        //如果不是消息类信息
                        //寻找是否需要进行回调
                        if (messageBuffer.Keys.Contains(command))
                        {
                            messageBuffer[command](args);
                            messageBuffer.Remove(command);
                        }

                    }
                    else
                    {
                        NewMessageEvent(this, ParseMsgToNewMessageArgs(commandAndArgs[1]));
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void Disconnect()
        {
            this.socket.Disconnect(false);
        }

        public void SendMessage(string command,string[] args = null,Action<string[]> cbk = null)
        {
            try
            {
                string str = command + ":";
                if (args != null)
                {
                    str += string.Join(",", args);
                }
                socket.Send(Encoding.UTF8.GetBytes(str));
                if (cbk != null)
                {
                    this.messageBuffer.Add(command, cbk);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private NewMessageArgs ParseMsgToNewMessageArgs(string msg)
        {
            //结构如下
            //消息类型,(Event类型Command),参数1,参数2,参数N
            var newMessageArgs = new NewMessageArgs();

            var datas = msg.Split(',');

            newMessageArgs.Msg = msg;

            //获取指令
            switch(datas[0])
            {
                case "Info": 
                    newMessageArgs.Type = MessageType.Info;
                    break;
                case "Warning":
                    newMessageArgs.Type = MessageType.Warning;
                    break;
                case "Debug":
                    newMessageArgs.Type = MessageType.Debug;
                    break;
                case "Error":
                    newMessageArgs.Type = MessageType.Error;
                    break;
                case "AssertSuccess":
                    newMessageArgs.Type = MessageType.AssertSuccess;
                    break;
                case "AssertFailed":
                    newMessageArgs.Type = MessageType.AssertFailed;
                    break;
                case "AssertError":
                    newMessageArgs.Type = MessageType.AssertError;
                    break;
                case "Event":
                    newMessageArgs.Type = MessageType.Event;
                    break;

            }
            
            if(datas[0] != "Event")
            {
                newMessageArgs.MessageArgs = new string[datas.Length - 1];
                Array.Copy(datas, 1, newMessageArgs.MessageArgs, 0, datas.Length - 1);
            }
            else
            {
                newMessageArgs.MessageArgs = new string[datas.Length - 2];
                newMessageArgs.EventCommand = datas[1];
                Array.Copy(datas,2,newMessageArgs.MessageArgs,0,datas.Length - 2);
            }

            return newMessageArgs;
        }
    }

    public class NewMessageArgs
    {
        public string Msg { get; set; }

        public MessageType Type { get; set; }

        public string EventCommand { get; set; }

        public string[] MessageArgs { get; set; }
    }

    public delegate void NewMessageHandle(object e,NewMessageArgs args);
}
