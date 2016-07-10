using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using UITests.Configuration;
using UITests.Helpers;
using UITests.Models;

namespace UITests.Pages
{
    public class CalculatorPage : BasePage
    {
        private static int CalculationStartTimeout = 5;
        private static int CalculationTimeout = 60;

        public CalculatorPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public override string PageUrl
        {
            get
            {
                return "/calculator";
            }
        }

        public IList<ServiceDescription> CalculateTranslationCost(String documentPath, String fromLang, String toLang)
        {
            sendTextFromFile(documentPath);

            selectOptionByName("from-lang", fromLang);

            selectOptionByName("to-lang", toLang);

            waitUntilProgressTimerDisapear();

            return parseServiceDescription().ToList();
        }

        public SubmittedOrderPage Submit(ServiceType serviceType, String email)
        {
            String previousUrl = Driver.Url;
            String serviceIdLocator = "div.service-description-block.service-description-";

            switch (serviceType)
            {
                case ServiceType.Express:
                    serviceIdLocator += "express";
                    break;

                case ServiceType.Standart:
                    serviceIdLocator += "standart";
                    break;

                case ServiceType.Professional:
                    serviceIdLocator += "pro";
                    break;

                case ServiceType.Expert:
                    serviceIdLocator += "expert";
                    break;

                default:
                    throw new ArgumentException(String.Format("Unexpected service type: {0}", serviceType));
            }
            serviceIdLocator += " > label";
            IWebElement serviceTypeSelect = Driver.FindElement(By.CssSelector(serviceIdLocator));
            serviceTypeSelect.Click();

            IWebElement emailElement = Driver.FindElement(By.CssSelector("input[type=\"email\"]"));
            emailElement.Clear();
            emailElement.SendKeys(email);

            IWebElement submit = Driver.FindElement(By.ClassName("submit-button"));
            submit.Click();

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(ConfigurationManager.Instance.WebDriverImplicitlyWaitTime));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            wait.Until(d => d.Url != previousUrl);

            return new SubmittedOrderPage(Driver, true);
        }



        private IEnumerable<ServiceDescription> parseServiceDescription()
        {
            var elements = Driver.FindElements(By.CssSelector("div.service-description-block"));
            foreach (IWebElement element in elements)
            {
                var serviceDescription = new ServiceDescription();
                String type = element.FindElement(By.Name("type")).GetAttribute("value");
                String priceText = element.FindElement(By.ClassName("service-description-price")).Text;

                String priceValue = priceText.Replace("\u20bd", String.Empty).Replace(',', '.').Trim();

                Decimal price;

                if (Decimal.TryParse(priceValue, out price))
                {
                    serviceDescription.Price = Decimal.Round(price, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    continue;
                }

                switch (type.ToLowerInvariant())
                {
                    case "express":
                        serviceDescription.Type = ServiceType.Express;
                        break;

                    case "standart":
                        serviceDescription.Type = ServiceType.Standart;
                        break;

                    case "pro":
                        serviceDescription.Type = ServiceType.Professional;
                        break;

                    case "expert":
                        serviceDescription.Type = ServiceType.Expert;
                        break;

                    default:
                        continue;
                }

                serviceDescription.Features = parseFeatures(element).ToList();
                yield return serviceDescription;
            }
        }

        private IEnumerable<Feature> parseFeatures(IWebElement element)
        {
            var featureElements = element.FindElements(By.ClassName("service-description-feature"));
            foreach (var featureElement in featureElements)
            {
                Feature feature = new Feature();
                var descriptionElements = featureElement.FindElements(By.ClassName("service-description-info"));

                String title = featureElement.Text.Trim().ToLowerInvariant();
                feature.Title = title;

                String description = String.Empty;
                if (descriptionElements.Count > 0)
                {
                    description = descriptionElements.First().GetAttribute("data-balloon").Trim();
                }
                feature.Description = description;

                switch (title)
                {
                    case "―":
                        continue;

                    case "перевод":
                        feature.Type = FeatureType.Translate;
                        break;

                    case "редактура":
                        feature.Type = FeatureType.Editing;
                        break;

                    case "корректура":
                        feature.Type = FeatureType.ProofSheet;
                        break;

                    case "вычитка":
                        feature.Type = FeatureType.ProofReading;
                        break;

                    default:
                        throw new ArgumentException(String.Format("Unknown feature title '{0}'.", title));
                }

                yield return feature;
            }
        }

        private void waitUntilProgressTimerDisapear()
        {
            var waitUtilCalculationStart = new WebDriverWait(Driver, TimeSpan.FromSeconds(CalculationStartTimeout));
            waitUtilCalculationStart.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            waitUtilCalculationStart.Until(d => d.FindElement(By.ClassName("timer-description")).Displayed);

            var waitUtilCalculationInProgress = new WebDriverWait(Driver, TimeSpan.FromSeconds(CalculationTimeout));
            waitUtilCalculationInProgress.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            waitUtilCalculationInProgress.Until(d => !d.FindElement(By.ClassName("timer-description")).Displayed);
        }

        private void selectOptionByName(String selector, String name)
        {
            IWebElement fromLangElement = Driver.FindElement(By.Name(selector));
            var selectFromLang = new SelectElement(fromLangElement);
            selectFromLang.SelectByText(name);
        }

        private void sendTextFromFile(String path)
        {
            
            IWebElement input = Driver.FindElement(By.CssSelector("fieldset.source-file.visible > div.ui-wrapper > textarea"));
            Actions action = new Actions(Driver);
            action.MoveToElement(input).Click().Perform();

            input.Clear();
            input.SendKeys(FileHelpers.GetFileContent(path));

        }
    }
}

