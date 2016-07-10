using System;
using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace UITests.Tests
{
    public class BrowserFixture : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return typeof(FirefoxDriver);
            //yield return typeof(ChromeDriver);
            //yield return typeof(InternetExplorerDriver);
        }
    }
}

