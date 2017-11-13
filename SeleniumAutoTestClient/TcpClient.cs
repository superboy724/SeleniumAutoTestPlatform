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
                        }

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
    }

    public class NewMessageArgs
    {
        public string Msg { get; set; }

        public LogType Type { get; set; }

        public string FormatString { get; set; }
    }
}
