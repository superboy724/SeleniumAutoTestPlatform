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

        private TcpMessage tcpMessageContext;

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
            //告知客户端测试用例数量
            this.tcpMessageContext.SendEvent("SetTestCauseCount", new string[] { testInstance.TestCases.Count().ToString() });
            //告知客户端需要测试的平台数量
            this.tcpMessageContext.SendEvent("SetPlatformCount", new string[] { testInstance.Platforms.Count().ToString() });
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
            LogManger.WriteInfo("Start test case:" + testCase.Name + " Platform:" + nowDriver.ToString());
            IWebDriver driver = SetWebDriver(nowDriver);
            //开始子测试
            //告知客户端当前正在进行的测试用例
            this.tcpMessageContext.SendEvent("SetRunningTestCauseName", new string[] { testCase.Name });
            foreach(var item in testCase.Tests)
            {
                lock (isStartLock)
                {
                    if (!isStart)
                    {
                        break;
                    }
                }
                RunTest(testCase.Name,item.Key, item.Value, driver);
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

        private void RunTest(string testcauseName,string name,Func<IWebDriver,Log,Assert> testScript,IWebDriver chooseDriver)
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
                case AssestResult.Error: tcpMessageContext.AssertError(testcauseName,name); break;
                case AssestResult.Success: tcpMessageContext.AssertSuccess(testcauseName, name, assest.UsedTime); break;
                case AssestResult.Failed: tcpMessageContext.AssertFailed(testcauseName, name, assest.UsedTime,assest.TrueValue,assest.FalseValue); break;
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
