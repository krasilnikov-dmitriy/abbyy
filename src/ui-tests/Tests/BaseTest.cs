using System;
using System.Collections;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
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
        private String screenshotsPath;

        protected BaseTest(Type browserType)
        {
            this.browserType = browserType;
        }

        public IWebDriver Driver { get; private set; }

        [OneTimeSetUp]
        public void InitializeScreenshotDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            string path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));

            screenshotsPath = Path.Combine(path, ConfigurationManager.Instance.ScreenshotsPath);
        }

        [SetUp]
        public void SetUp()
        {
            initializeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    var test = TestContext.CurrentContext.Test;
                    Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                    String filename = String.Format("/{0}.png", Guid.NewGuid().ToString()); //screenshotsPath + String.Format("/{0}.png", Guid.NewGuid().ToString());
                    screenshot.SaveAsFile(filename, ImageFormat.Png);

                    // TODO: Work only if create true nuget package
                    //Allure.Lifecycle.AttachScreenshot(test.ID, screenshot.AsByteArray);
                }
            }
            finally
            {
                Driver.Close();
            }
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

