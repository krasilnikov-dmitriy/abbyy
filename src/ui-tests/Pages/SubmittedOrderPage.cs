using System;
using OpenQA.Selenium;

namespace UITests.Pages
{
    public class SubmittedOrderPage : BasePage
    {
        private int orderId;

        public SubmittedOrderPage(IWebDriver driver) : base(driver)
        {
            orderId = -1;
        }

        public SubmittedOrderPage(IWebDriver driver, int orderId) : base(driver)
        {
            this.orderId = orderId;
        }

        public SubmittedOrderPage(IWebDriver driver, bool redirected) : base(driver, redirected)
        {
            orderId = -1;
        }

        public override string PageUrl
        {
            get
            {
                return String.Format("/node/{0}/done", orderId);
            }
        }

        public bool IsSuccessful()
        {
            IWebElement title = Driver.FindElement(By.Id("page-title"));
            IWebElement confirmationElement = Driver.FindElement(By.ClassName("webform-confirmation"));

            return title.Text == "Заказ услуги"
                        && confirmationElement.Text.Contains("Благодарим за обращение в нашу компанию.")
                        && confirmationElement.Text.Contains("В ближайшее время мы обязательно свяжемся с Вами для обсуждения деталей заявки.");
        }
    }
}

