using System;
using System.Collections.Generic;
using NUnit.Framework;
using UITests.Helpers;
using UITests.Models;
using UITests.Pages;

namespace UITests.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixtureSource(typeof(BrowserFixture))]
    public class CalculateTranslationCostTests : BaseTest
    {
        public const String TEST_DOCUMENT = @"Resources\testdoc.txt";

        public static object[] DocumentsForCalculation = {
            new object[] {TEST_DOCUMENT , "Английский", "Русский", new List<ServiceDescription>() {
                    new ServiceDescription() { Type = ServiceType.Express, Price = 1.00m },
                    new ServiceDescription() { Type = ServiceType.Standart, Price = 2.20m },
                    new ServiceDescription() { Type = ServiceType.Professional, Price = 3.40m },
                    new ServiceDescription() { Type = ServiceType.Expert, Price = 4.50m },
                }},

            new object[] {TEST_DOCUMENT , "Немецкий", "Русский", new List<ServiceDescription>() {
                    new ServiceDescription() { Type = ServiceType.Express, Price = 1.20m },
                    new ServiceDescription() { Type = ServiceType.Standart, Price = 2.60m },
                    new ServiceDescription() { Type = ServiceType.Professional, Price = 3.50m },
                    new ServiceDescription() { Type = ServiceType.Expert, Price = 5.00m },
                }},

            new object[] {TEST_DOCUMENT , "Японский", "Русский", new List<ServiceDescription>() {
                    new ServiceDescription() { Type = ServiceType.Express, Price = 1.50m },
                    new ServiceDescription() { Type = ServiceType.Standart, Price = 3.00m },
                    new ServiceDescription() { Type = ServiceType.Professional, Price = 4.00m },
                    new ServiceDescription() { Type = ServiceType.Expert, Price = 6.00m },
                }}
        };

        public CalculateTranslationCostTests(Type browserType) : base(browserType)
        {
        }


        [Test, TestCaseSource("DocumentsForCalculation")]
        public void CalculateTranslationCostForDocumentShouldHaveRIghtPriceTest(String document, String fromLang, String toLang, List<ServiceDescription> expectedResults)
        {
            String documentPath = FileHelpers.GetAbsolutePath(document);

            var page = new CalculatorPage(Driver);

            IList<ServiceDescription> serviceDescriptions = page.CalculateTranslationCost(documentPath, fromLang, toLang);

            Assert.That(serviceDescriptions, Has.Count.EqualTo(expectedResults.Count));

            foreach (var expectedResult in expectedResults)
            {
                Assert.That(serviceDescriptions,
                            Has.Some.Matches<ServiceDescription>(p => p.Type == expectedResult.Type
                                                                 && p.Price == expectedResult.Price),
                            String.Format("Service descriptions should contaiuns: {0}", expectedResult));
            }
        }
    }
}

