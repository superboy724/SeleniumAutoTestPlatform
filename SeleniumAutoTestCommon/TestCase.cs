using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutoTestCommon
{
    public class TestCase
    {
        public string Name { get; set; }

        public Dictionary<string, Func<IWebDriver,Log, Assert>> Tests { get; set; }

        public TestCase()
        {
            this.Name = "";
            this.Tests = new Dictionary<string, Func<IWebDriver,Log, Assert>>();
        }

        public void NewTest(string testName, Func<IWebDriver, Log, Assert> testDo)
        {
            this.Tests.Add(testName, testDo);
        }
    }
}
