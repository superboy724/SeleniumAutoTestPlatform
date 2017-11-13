using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumAutoTestServer
{
    public class TCPServer
    {
        private Socket serverSocket;

        private const int PORT = 1919;

        public bool ServerInit()
        {
            try
            {
                //开启Tcp服务器
                LogManger.WriteInfo("Start Server,IP port 1919");
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
                serverSocket.Listen(100);
                return true;
            }
            catch(Exception ex)
            {
                LogManger.WriteError("Cannot start server:" + ex.ToString());
                return false;
            }
        }

        public void Listen()
        {
            try
            {
                //监听请求
                LogManger.WriteInfo("Server is listening");
                while(true)
                {
                    var clientSocket = this.serverSocket.Accept();
                    LogManger.WriteInfo("New connect:" + clientSocket.RemoteEndPoint.ToString());
                    Thread clientThread = new Thread(new ParameterizedThreadStart((object param) => {
                        try
                        {
                            var socket = param as Socket;
                            //创建用户操作服务器的实例
                            ServerManger serverManger = new ServerManger();
                            //创建TCP消息上下文
                            TcpMessage message = new TcpMessage();
                            message.NewMessage += (object e, NewMessageArgs args) =>
                            {
                                socket.Send(Encoding.UTF8.GetBytes("MSG:" + args.FormatString));
                            };
                            serverManger.MessageContext = message;
                            while (true)
                            {
                                byte[] buffer = new byte[256];
                                var length = socket.Receive(buffer);
                                //断连
                                if(length == 0)
                                {
                                    return;
                                }
                                string data = Encoding.UTF8.GetString(buffer);
                                LogManger.WriteInfo(clientSocket.RemoteEndPoint.ToString() + ":" + data);
                                socket.Send(Encoding.UTF8.GetBytes(Parse(data, serverManger)));
                            }
                        }
                        catch(Exception ex)
                        {
                            LogManger.WriteError("Listen error:" + ex.ToString());
                        }
                    }));
                    clientThread.Start(clientSocket);
                }
            }
            catch(Exception ex)
            {
                LogManger.WriteError("Listen error:" + ex.ToString());
            }
        }

        /// <summary>
        /// 解析指令
        /// </summary>
        public static string Parse(string data,ServerManger serverManger)
        {
            data = data.Replace("\0", "");
            string[] commandAndArgs = data.Split(':');
            string command = commandAndArgs[0];
            string[] args = commandAndArgs[1].Split(',');
            string returnStr = command + ":";
            switch(command)
            {

                case "GET": returnStr += string.Join(",", serverManger.GetTests());break;
                case "RUN": 
                    if(serverManger.StartTest(args[0]))
                    {
                        returnStr += "OK";
                    }
                    else
                    {
                        returnStr += "FAILED";
                    }
                    break;
            }
            return returnStr;
        }
    }
}
