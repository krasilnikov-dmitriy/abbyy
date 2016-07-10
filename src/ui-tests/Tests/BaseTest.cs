using System;
using System.Collections;
using AllureNUnitAdapter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using UITests.Configuration;

namespace UITests.Tests
{
    public abstract class BaseTest
    {
        private Type browserType;

        protected BaseTest(Type browserType)
        {
            this.browserType = browserType;
        }

        public IWebDriver Driver { get; private set; }

        [SetUp]
        public void SetUp()
        {
            initializeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var test = TestContext.CurrentContext.Test;
                Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                Allure.Lifecycle.AttachScreenshot(test.ID, screenshot.AsByteArray);
            }

            Driver.Close();
        }

        protected void initializeDriver()
        {
            if (ConfigurationManager.Instance.UseWebDriverRC)
            {
                DesiredCapabilities desiredCapabilities;
                if (browserType == typeof(FirefoxDriver))
                {
                    desiredCapabilities = DesiredCapabilities.Firefox();
                }
                else if (browserType == typeof(ChromeDriver))
                {
                    desiredCapabilities = DesiredCapabilities.Chrome();
                }
                else if (browserType == typeof(InternetExplorerDriver))
                {
                    desiredCapabilities = DesiredCapabilities.InternetExplorer();
                }
                else
                {
                    throw new NotImplementedException(String.Format("Driver {0}, not found.", browserType));
                }

                Driver = new RemoteWebDriver(new Uri(ConfigurationManager.Instance.WebDriverHubUrl), desiredCapabilities);
            }
            else {
                Driver = Activator.CreateInstance(browserType) as IWebDriver;
            }
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(ConfigurationManager.Instance.WebDriverImplicitlyWaitTime));
        }
    }
}

