using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UITests.Pages
{
    public class MainPage : BasePage
    {
        private static int WaitSliderChangeTabTimeout = 10;

        public MainPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public override string PageUrl
        {
            get
            {
                return "/";
            }
        }

        public void ClickOnTab(String tabName)
        {
            IWebElement link = Driver.FindElement(By.LinkText(tabName));
            link.Click();

            int index;

            if (int.TryParse(link.GetAttribute("data"), out index))
            {
                index++;
            }
            else
            {
                throw new Exception("Slider item doesn't contains 'data' attribute.");
            }


            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(WaitSliderChangeTabTimeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            wait.Until(d => d.FindElement(By.CssSelector(String.Format("li.frontslider2-leftcol-list-item.item{0}.frontslider2bg.active", index))) != null);
        }

        public String GetSliderImagePosition()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(WaitSliderChangeTabTimeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            IWebElement image = wait.Until(d =>
            {
                var images = d.FindElements(By.CssSelector(String.Format("div.frontslider2-rightcol-img.frontslider2bg")));
                var img = images.FirstOrDefault(e => e.Displayed);
                if (img == null)
                {
                    return null;
                }

                return img;
            });

            if (image == null || !image.Displayed)
            {
                throw new ElementNotVisibleException("All slider images are invisible.");
            }

            return image.GetCssValue("background-position");
        }
    }
}
