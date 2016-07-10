using System;
using NUnit.Framework;
using OpenQA.Selenium;
using UITests.Helpers;
using UITests.Pages;

namespace UITests.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixtureSource(typeof(BrowserFixture))]
    public class ContactInformationTests : BaseTest
    {
        public ContactInformationTests(Type browserType) : base(browserType)
        {
        }

        [TestCaseSource(typeof(PageHelpers), "GetAllPageInitializers")]
        public void PageShouldHaveContactPhone(Func<IWebDriver, BasePage> pageInitializer)
        {
            BasePage page = pageInitializer(Driver);
            String phoneNumber = page.GetContactPhoneNumber();

            Assert.That(phoneNumber, Is.Not.Empty);
        }
    }
}

