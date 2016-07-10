using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using UITests.Helpers;
using UITests.Pages;

namespace UITests.Tests
{
    
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixtureSource(typeof(BrowserFixture))]
    public class LanguageSwitcherTests : BaseTest
    {
        public LanguageSwitcherTests(Type browserType) : base(browserType)
        {
        }

        [TestCaseSource(typeof(PageHelpers), "GetAllPageInitializers")]
        public void PageShouldHaveLanguageSwitcherWith5Options(Func<IWebDriver, BasePage> pageInitializer)
        {
            BasePage page = pageInitializer(Driver);
            List<String> options = page.GetAllLangSwitcherOptions().ToList();

            Assert.That(options, Has.Count.EqualTo(5));

            Assert.That(options, Has.Member("Russia — Русский"));
            Assert.That(options, Has.Member("USA — English"));
            Assert.That(options, Has.Member("Germany — Deutsch"));
            Assert.That(options, Has.Member("Kazakhstan — Русский"));
            Assert.That(options, Has.Member("Ukraine — Українська"));
        }
    }
}

