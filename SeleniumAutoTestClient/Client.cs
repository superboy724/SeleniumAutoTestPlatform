using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumAutoTestClient
{
    public class Client
    {
        public List<string> TestInstances { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            var ip = Global.ServerIP;
            this.TestInstances = new List<string>();
            if (ip == "")
            {
                return false;
            }
            else
            {
                Global.TcpClient = new TcpClient(ip);
                Global.TcpClient.Connect();
                return true;
            }
        }

        /// <summary>
        /// 获取服务端现有的测试实例
        /// </summary>
        /// <param name="completeCbk">完成事件</param>
        public void GetInstance(Action completeCbk)
        {
            Global.TcpClient.SendMessage("GET", null, (string[] instances) => {
                this.TestInstances.AddRange(instances);
                completeCbk();
            });
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="testInstanceName"></param>
        public void Run(string testInstanceName,Action<bool> runningStatusckb)
        {
            Global.TcpClient.SendMessage("RUN", new string[] { testInstanceName }, (string[] status) =>
            {
                if(status[0] == "OK")
                {
                    runningStatusckb(true);
                }
                else
                {
                    runningStatusckb(false);
                }
            });
        }

        public void Disconnect()
        {
            Global.TcpClient.Disconnect();
        }
    }
}
