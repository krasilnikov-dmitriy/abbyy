using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using AllureNUnitAdapter.Helpers;
using AllureNUnitAdapter.Models.NUnit.EventArgs;
using log4net;
using NUnit.Engine;
using NUnit.Engine.Extensibility;

namespace AllureNUnitAdapter
{
    [Extension]
    public class AllureEventListener : ITestEventListener
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AllureEventListener));
        private static readonly Allure lifecycle = Allure.Lifecycle;

        public AllureEventListener()
        {
            try
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));

                AllureConfig.ResultsPath = 
                    XDocument.Load(path + "/config.xml")
                        .Descendants()
                        .First(x => x.Name.LocalName.Equals("results-path")).Value + "/";

                logger.Info("Initialization completed successfully.\n");
                logger.Info(String.Format("Results Path: {0}", AllureConfig.ResultsPath));

                if (Directory.Exists(AllureConfig.ResultsPath))
                {
                    Directory.Delete(AllureConfig.ResultsPath, true);
                }
                Directory.CreateDirectory(AllureConfig.ResultsPath);
            }
            catch (Exception e)
            {
                logger.Error(String.Format("Exception in initialization"), e);
            }
        }

        public void OnTestEvent(string report)
        {
            try
            {
                var xmlElement = XmlHelper.CreateXmlElement(report);

                switch (xmlElement.Name.LocalName.ToLowerInvariant())
                {
                    case "start-run":
                        //Logger.Info(report);
                        break;

                    case "test-run":
                        //Logger.Info(report);

                        break;

                    case "start-suite":
                        var startSuiteEventArgs = XmlHelper.Deserialize<StartTestSuiteEventArgs>(report);
                        lifecycle.TestSuiteStarted(startSuiteEventArgs);
                        break;

                    case "test-suite":
                        var stopSuiteEventArgs = XmlHelper.Deserialize<StopTestSuiteEventArgs>(report);
                        lifecycle.TestSuiteFinished(stopSuiteEventArgs);
                        break;

                    case "start-test":
                        var startTestCaseEventArgs = XmlHelper.Deserialize<StartTestCaseEventArgs>(report);
                        lifecycle.TestCaseStarted(startTestCaseEventArgs);
                        break;

                    case "test-case":
                        var stopTestCaseEventArgs = XmlHelper.Deserialize<StopTestCaseEventArgs>(report);
                        lifecycle.TestCaseFinished(stopTestCaseEventArgs);
                        break;

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
    }
}

