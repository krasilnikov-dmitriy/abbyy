using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UITests.Helpers;
using UITests.Models;
using UITests.Pages;

namespace UITests.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixtureSource(typeof(BrowserFixture))]
    public class SendDocumentForTranslateTests : BaseTest
    {
        private const String TEST_DOCUMENT = @"Resources\testdoc.txt";

        private const String FROM_LANG = "Английский";

        private const String TO_LANG = "Русский";

        private const String EMAIL = "test@test.ru";

        public SendDocumentForTranslateTests(Type browserType) : base(browserType)
        {
        }

        [Theory]
        public void SubmitDocumentForTranslateShouldRedirectOnCompletedOrderPage(ServiceType serviceType)
        {
            String documentPath = FileHelpers.GetAbsolutePath(TEST_DOCUMENT);

            var page = new CalculatorPage(Driver);

            IList<ServiceDescription> serviceDescriptions = page.CalculateTranslationCost(documentPath, FROM_LANG, TO_LANG);

            var serviceDescription = serviceDescriptions.FirstOrDefault(i => i.Type == serviceType);

            Assert.That(serviceDescription, Is.Not.Null, String.Format("Service plan '{0}' not found", serviceType));

            SubmittedOrderPage result =  page.Submit(serviceType, EMAIL);

            Assert.That(result.IsSuccessful, Is.True);
        }
    }
}

