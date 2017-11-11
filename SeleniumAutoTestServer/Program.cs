using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutoTestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            LogManger.WriteInfo("Start server..");
            LogManger.WriteInfo("Server init..");
            if(ServerManger.ServerInit())
            {
                LogManger.WriteInfo("Start tcp server..");
                TCPServer tcpserver = new TCPServer();
                if (tcpserver.ServerInit())
                {
                    LogManger.WriteInfo("Tcp server init is complete");
                    LogManger.WriteInfo("Start listening..");
                    tcpserver.Listen();
                }
                else
                {
                    LogManger.WriteInfo("Tcp server init was failed");
                }
            }
            else
            {
                LogManger.WriteInfo("Start server was failed");
            }
        }
    }
}
