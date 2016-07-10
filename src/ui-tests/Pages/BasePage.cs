using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using UITests.Configuration;
using UITests.Helpers;

namespace UITests.Pages
{
    public abstract class BasePage
    {
        private String baseUrl;

        public BasePage(IWebDriver driver) : this(driver, false)
        {
        }

        public BasePage(IWebDriver driver, bool redirected)
        {
            Driver = driver;
            baseUrl = ConfigurationManager.Instance.SiteUrl;

            if (!redirected)
            {
                Driver.Navigate().GoToUrl(constructPageUri());
            }

            PageFactory.InitElements(Driver, this);

        }

        public abstract String PageUrl { get; }

        public IEnumerable<String> GetAllLangSwitcherOptions()
        {
            IWebElement languageSwitcher = Driver.FindElement(By.ClassName("lang-switcher"));

            Actions action = new Actions(Driver);
            action.MoveToElement(languageSwitcher).Perform();

            IWebElement currentLanguage = Driver.FindElement(By.ClassName("lang-switcher__current-item"));
            IEnumerable<IWebElement> dropDownLanguages = Driver.FindElements(By.ClassName("lang-switcher__item"));

            foreach (var item in dropDownLanguages)
            {
                yield return item.Text;
            }

            yield return currentLanguage.Text.Trim();
        }

        public virtual String GetContactPhoneNumber()
        {
            IWebElement contactPhoneNumber = Driver.FindElement(By.CssSelector("div[class='email-phone-block'] > p"));
            return contactPhoneNumber.Text;
        }

        protected IWebDriver Driver { get; }

        private Uri constructPageUri()
        {
            if (UriHelpers.IsAbsoluteUrl(PageUrl))
            {
                return new Uri(PageUrl);
            }

            Uri baseUri = new Uri(baseUrl);
            return new Uri(baseUri, PageUrl);
        }

    }
}