using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumAutoTestServer
{
    public class ServerManger
    {
        public static ServerTestInstance ServerTestInstance { get; set; }

        public static bool IsRunningTest { get; set; }

        public static object IsRunningTestLock { get; set; }

        public static Dictionary<string, TestInstanceLoader> Tests { get; set; }

        public TcpMessage MessageContext { get; set; }

        public static bool ServerInit()
        {
            try
            {
                IsRunningTest = false;
                IsRunningTestLock = new object();
                Tests = new Dictionary<string, TestInstanceLoader>();
                LoadDlls();
                return true;
            }
            catch(Exception ex)
            {
                LogManger.WriteError("Init Failed:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 开始测试
        /// </summary>
        /// <param name="testName"></param>
        /// <returns></returns>
        public bool StartTest(string testName)
        {
            try
            {
                //判断是否存在正在运行中的测试
                lock (IsRunningTestLock)
                {
                    if (!IsRunningTest)
                    {
                        IsRunningTest = true;
                    }
                    else
                    {
                        return false;
                    }
                }
                //寻找是否有测试用例
                if(!Tests.Keys.Contains(testName))
                {
                    throw new Exception("Cannot find test named " + testName);
                }
                //加载
                var testInstance = Tests[testName].Load();
                ServerTestInstance = new ServerTestInstance(testInstance, testName, MessageContext);
                ServerTestInstance.CompleteEvent += (object e) =>
                {
                    lock(IsRunningTestLock)
                    {
                        IsRunningTest = false;
                    }
                };
                Thread t = new Thread(new ThreadStart(ServerTestInstance.Run));
                t.Start();
                return true;
            }
            catch(Exception ex)
            {
                lock (IsRunningTestLock)
                {
                    IsRunningTest = false;
                }
                LogManger.WriteError("Start test failed:" + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 中断测试
        /// </summary>
        /// <returns></returns>
        public bool StopTest()
        {
            return ServerTestInstance.Stop();
        }

        public bool DeleteTest(string testName)
        {
            return true;
        }

        public string[] GetTests()
        {
            return Tests.Keys.ToArray();
        }

        private static void LoadDlls()
        {
            string workDir = Directory.GetCurrentDirectory() + "\\Tests";
            if(!Directory.Exists(workDir))
            {
                //如果没找到Dll的目录
                throw new Exception("Test dll directory is not found,Please create directory name \"Tests\" at work direactory");
            }
            else
            {
                //获取所有文件
                var dlls = Directory.GetFiles(workDir);
                foreach(var item in dlls)
                {
                    //分解文件名 判断扩展名
                    var fileNameForSplit = item.Split('.');
                    if (fileNameForSplit[fileNameForSplit.Length-1].ToLower() == "dll")
                    {
                        TestInstanceLoader loader = new TestInstanceLoader();
                        loader.DllDir = item;
                        //获取文件名
                        string fileName = item.Substring(item.LastIndexOf("\\") + 1, (item.LastIndexOf(".") - item.LastIndexOf("\\") - 1));
                        ServerManger.Tests.Add(fileName, loader);
                    }
                }
            }
        }
    }
}
