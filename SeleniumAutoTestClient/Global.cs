using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SeleniumAutoTestClient
{
    /// <summary>
    /// 公共变量类
    /// </summary>
    public class Global
    {
        static string serverIP;

        static TcpClient client;

        static Client clientManger;

        public static string ServerIP
        {
            get
            {
                if(serverIP == null)
                {
                    serverIP = ConfigurationManager.AppSettings["ServerIP"];
                }
                return serverIP;
            }
            set
            {
                serverIP = value;
                Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configFile.AppSettings.Settings["ServerIP"].Value = value;
                configFile.Save();
            }
        }

        public static TcpClient TcpClient
        {
            get;
            set;
        }

        public static Client ClientManger
        {
            get;
            set;
        }
    }
}
