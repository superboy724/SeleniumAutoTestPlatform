using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutoTestCommon
{
    public abstract class TestInstance
    {
        public List<TestPlatform> Platforms { get; set; }

        public List<TestCase> TestCases { get; set; }
        public virtual void Init()
        {
            Platforms = new List<TestPlatform>();
            TestCases = new List<TestCase>();
        }

        /// <summary>
        /// 配置需要测试的平台
        /// </summary>
        /// <param name="platform">平台</param>
        public void SetTestPlatform(params TestPlatform[] platform)
        {
            Platforms.AddRange(platform);
        }

        /// <summary>
        /// 创建一个新测试用例
        /// </summary>
        /// <param name="caseName">名称</param>
        /// <returns></returns>
        public TestCase CreateNewTestCase(string caseName)
        {
            TestCase testCase = new TestCase()
            {
                Name = caseName
            };
            this.TestCases.Add(testCase);
            return testCase;
        }
    }
}
