using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SeleniumAutoTestCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutoTestServer
{
    public class ServerTestInstance
    {
        private TestInstance testInstance;

        private string testName;

        private TestPlatform nowDriver;

        private bool isStart;

        private object isStartLock;

        public event TestComleteHandle CompleteEvent;

        public TcpMessage tcpMessageContext { get; set; }

        public ServerTestInstance(TestInstance instance,string testName,TcpMessage messageContext)
        {
            this.testInstance = instance;
            this.testName = testName;
            this.tcpMessageContext = messageContext;
            this.isStart = false;
            this.isStartLock = new object();
        }
        public void Run()
        {
            testInstance.Init();
            lock (isStartLock)
            {
                if (!isStart)
                {
                    isStart = true;
                }
                else
                {
                    CompleteEvent(this);
                    return;
                }
            }
            //根据用户选择的平台每个平台都测试
            foreach (var item in testInstance.Platforms)
            {
                this.nowDriver = item;
                //开始每个测试用例
                foreach(var testCase in testInstance.TestCases)
                {
                    lock (isStartLock)
                    {
                        if (!isStart)
                        {
                            CompleteEvent(this);
                            return;
                        }
                    }
                    RunTestCase(testCase);
                }
            }
            CompleteEvent(this);
        }

        private void RunTestCase(TestCase testCase)
        {
            LogManger.WriteInfo("Start test case:" + testCase.Name);
            IWebDriver driver = SetWebDriver(nowDriver);
            //开始子测试
            foreach(var item in testCase.Tests)
            {
                lock (isStartLock)
                {
                    if (!isStart)
                    {
                        break;
                    }
                }
                RunTest(item.Key, item.Value, driver);
            }
            //结束测试
            driver.Quit();
            LogManger.WriteInfo("Test case:" + testCase.Name + " is complete");
        }

        private IWebDriver SetWebDriver(TestPlatform platform)
        {
            switch (platform)
            {
                case TestPlatform.Chrome: return new ChromeDriver();
                case TestPlatform.IE: return new InternetExplorerDriver();
                case TestPlatform.FireFox: return new FirefoxDriver();
                default:return null;
            }
        }

        private void RunTest(string name,Func<IWebDriver,Log,Assert> testScript,IWebDriver chooseDriver)
        {
            lock(isStartLock)
            {
                if(!isStart)
                {
                    return;
                }
            }
            //开始测试
            LogManger.WriteInfo("Start test:" + name);
            //创建日志上下文
            Log scriptLogContext = new Log();
            scriptLogContext.TestCaseName = name;
            //绑定新日志事件
            scriptLogContext.NewLog += (object e, NewLogArgs args)=>{
                LogManger.WriteTestLog(args.FormatString);
                switch(args.Type)
                {
                    case SeleniumAutoTestCommon.LogType.Warning: tcpMessageContext.Warn(args.Msg);break;
                    case SeleniumAutoTestCommon.LogType.Debug: tcpMessageContext.Debug(args.Msg); break;
                    case SeleniumAutoTestCommon.LogType.Info: tcpMessageContext.Info(args.Msg); break;
                    case SeleniumAutoTestCommon.LogType.Error: tcpMessageContext.Error(args.Msg); break;
                }
            };
            //开始
            var assest = RunTestAndReturnResult(testScript, scriptLogContext, chooseDriver);
            //回报消息
            switch(assest.Result)
            {
                case AssestResult.Error: tcpMessageContext.AssertError(); break;
                case AssestResult.Success: tcpMessageContext.AssertSuccess(); break;
                case AssestResult.Failed: tcpMessageContext.AssertFailed(); break;
            }
            //结束
            LogManger.WriteTestLog("Test result:" + assest.Result + " Time:" + assest.UsedTime.ToString());
            LogManger.WriteInfo("Test complete:" + name);
        }

        private Assert RunTestAndReturnResult(Func<IWebDriver, Log, Assert> testScript,Log scriptLogContext, IWebDriver chooseDriver)
        {
            //计算一个测试使用的时间
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            Assert assert;
            try
            {
                assert = testScript(chooseDriver, scriptLogContext);
            }
            catch(Exception ex)
            {
                assert = Assert.TestError();
                scriptLogContext.Error("Test Error:" + ex.ToString());
            }
            stopwatch.Stop();
            assert.UsedTime = stopwatch.ElapsedMilliseconds;
            return assert;
        }

        public bool Stop()
        {
            lock (isStartLock)
            {
                if (isStart)
                {
                    isStart = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }


    public delegate void TestComleteHandle(object e);
}
