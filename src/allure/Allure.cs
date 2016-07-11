using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using AllureNUnitAdapter.Helpers;
using AllureNUnitAdapter.Models.Allure;
using AllureNUnitAdapter.Models.NUnit.EventArgs;
using AllureNUnitAdapter.Storages;
using log4net;

namespace AllureNUnitAdapter
{
    public class Allure
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AllureEventListener));
        private static readonly Object testSuiteLock = new Object();
        private static readonly Object attachmentLock = new Object();
        private static Allure instance = null;
        private static object syncRoot = new Object();

        private const String VERSION = "1.4.23";

        private readonly TestSuiteStorage testSuiteStorage = new TestSuiteStorage();
        private readonly AttachmentStorage attachmentStorage = new AttachmentStorage();

        public static Allure Lifecycle
        {
            get
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new Allure();
                    }
                    return instance;
                }
            }
        }

        public void AttachScreenshot(String testCaseId, byte[] screenshot)
        {
            logger.Info(String.Format("Attach screenshot to {0}", testCaseId));
            var attachment = new Attachment() { AttachmentAsByte = screenshot };
            lock (attachmentLock)
            {
                attachmentStorage.Put(testCaseId, attachment);
            }
        }


        private Allure()
        {
            Utils.Logger.Setup();
        }

        internal void TestSuiteStarted(StartTestSuiteEventArgs evt)
        {
            testSuiteStorage.Put(evt.Id, new TestSuiteResult());
        }

        internal void TestSuiteFinished(StopTestSuiteEventArgs eventArgs)
        {
            switch (eventArgs.Type)
            {
                case TestSuiteType.TestFixture:
                    lock (testSuiteLock)
                    {
                        TestSuiteResult result = testSuiteStorage.Get(eventArgs.Id);

                        result.Name = eventArgs.Name;
                        result.Title = eventArgs.FullName;
                        result.Start = eventArgs.StartTime.ToUnixTimestamp();
                        result.Stop = eventArgs.EndTime.ToUnixTimestamp();

                        XmlHelper.SaveToFile(result, AllureConfig.ResultsPath + Guid.NewGuid().ToString() + "-testsuite.xml");
                    }
                    break;

                default:
                    lock (testSuiteLock)
                    {
                        if (String.IsNullOrEmpty(eventArgs.ParentId))
                        {
                            break;
                        }
                        var parrentSuite = testSuiteStorage.Get(eventArgs.ParentId);
                        var suite = testSuiteStorage.Get(eventArgs.Id);
                        parrentSuite.TestCaseResults.AddRange(suite.TestCaseResults);
                        testSuiteStorage.Update(eventArgs.ParentId, parrentSuite);
                        testSuiteStorage.Remove(eventArgs.Id);
                    }
                    break;
            }
        }

        internal void TestCaseStarted(StartTestCaseEventArgs evt)
        {
            // Do Nothing
            //testCaseStorage.Put(evt.Id, new TestCaseResult());
        }

        internal void TestCaseFinished(StopTestCaseEventArgs eventArgs)
        {
            TestCaseResult result = new TestCaseResult();
            result.Name = eventArgs.Name;
            result.Title = eventArgs.FullName;
            result.Start = eventArgs.StartTime.ToUnixTimestamp();
            result.Stop = eventArgs.EndTime.ToUnixTimestamp();

            switch (eventArgs.Result)
            {
                case "Inconclusive":
                    result.Status = TestResult.Broken;
                    break;

                case "Skipped":
                    result.Status = TestResult.Canceled;
                    break;

                case "Passed":
                    result.Status = TestResult.Passed;
                    break;

                case "Failed":
                    result.Status = TestResult.Failed;

                    if (eventArgs.Failure != null)
                    {
                        result.Failure = new Models.Allure.Failure { Message = eventArgs.Failure.Message, StackTrace = eventArgs.Failure.StackTrace };
                    }
                    break;

                default:
                    result.Status = TestResult.Broken;
                    break;
            }

            //var attachments = attachmentStorage.Get(eventArgs.Id);
            //result.Attachments = attachments.Where(a => a.AttachmentAsByte != null).Select(a => saveScreenshotAsAttachment(a.AttachmentAsByte, "Screenshot")).ToList();

            lock (testSuiteLock)
            {
                var suite = testSuiteStorage.GetOrCreate(eventArgs.ParentId);
                suite.TestCaseResults.Add(result);
                testSuiteStorage.Update(eventArgs.ParentId, suite);
            }
        }

        private Attachment saveScreenshotAsAttachment(byte[] attachment, String title)
        {
            var relativePath = generateSha256(attachment) + "-attachment.png";
            var path = AllureConfig.ResultsPath + relativePath;
            if (!File.Exists(path))
            {
                try
                {
                    using (var ms = new MemoryStream(attachment))
                    {
                        var image = Image.FromStream(ms);
                        lock (attachmentLock)
                        {
                            image.Save(path, ImageFormat.Png);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }

            }
            return new Attachment() { Title = title, Type = "png", Source = relativePath };
        }

        private static string generateSha256(byte[] data)
        {
            var crypt = new SHA256Managed();
            var hash = String.Empty;
            var crypto = crypt.ComputeHash(data);
            return crypto.Aggregate(hash, (current, bit) => current + bit.ToString("x2"));
        }
    }
}

