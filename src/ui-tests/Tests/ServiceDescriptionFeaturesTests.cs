using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UITests.Helpers;
using UITests.Models;
using UITests.Pages;

namespace UITests.Tests
{
    class FeatureDescriptionConstraint : Constraint
    {
        private Feature expected;

        public FeatureDescriptionConstraint(Feature expected)
        {
            this.expected = expected;
        }

        public override string Description
        {
            get { return String.Format("Actual feature not equals to expected: {0}", expected); }
        }

        public override ConstraintResult ApplyTo<T>(T actual)
        {
            Feature actualFeature = actual as Feature;
            return new ConstraintResult(this, actual, check(actualFeature));
        }

        private bool check(Feature actual)
        {
            if (actual == null || expected == null)
                return actual == expected;

            return actual.Title == expected.Title
                         && actual.Type == expected.Type
                         && actual.Description == expected.Description;
        }
    }

    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixtureSource(typeof(BrowserFixture))]
    public class ServiceDescriptionFeaturesTests : BaseTest
    {
        private const String TEST_DOCUMENT = @"Resources\testdoc.txt";

        private const String FROM_LANG = "Английский";

        private const String TO_LANG = "Русский";

        public ServiceDescriptionFeaturesTests(Type browserType) : base(browserType)
        {
        }

        [Test]
        public void ServiceDescriptionShouldHaveRightFeatureTooltipInfo()
        {
            String documentPath = FileHelpers.GetAbsolutePath(TEST_DOCUMENT);

            var page = new CalculatorPage(Driver);

            IList<ServiceDescription> serviceDescriptions = page.CalculateTranslationCost(documentPath, FROM_LANG, TO_LANG);

            Assert.That(serviceDescriptions, Has.Count.EqualTo(4));

            serviceDescriptions.ToList().ForEach(checkServiceDescriptionTooltip);
        }

        private void checkServiceDescriptionTooltip(ServiceDescription serviceDescription)
        {
            switch (serviceDescription.Type)
            {
                case ServiceType.Express:
                    Assert.True(true, "Tooltip Ok");
                    break;

                case ServiceType.Standart:
                    checkEditingFeature(serviceDescription);
                    break;

                case ServiceType.Professional:
                    checkEditingFeature(serviceDescription);
                    checkProofSheetFeature(serviceDescription);
                    break;

                case ServiceType.Expert:
                    checkEditingFeature(serviceDescription);
                    checkProofSheetFeature(serviceDescription);
                    checkProofReadingFeature(serviceDescription);
                    break;

                default:
                    Assert.Fail(String.Format("Unknown service description type {0}", serviceDescription.Type));
                    break;
            }
        }

        private void checkEditingFeature(ServiceDescription serviceDescription)
        {
            Assert.That(serviceDescription.Features, Has.Some.Matches(new FeatureDescriptionConstraint(new Feature()
            {
                Type = FeatureType.Editing,
                Title = "редактура",
                Description = "Контроль лексического единообразия и сверка терминологии носителем языка"

            })));
        }

        private void checkProofSheetFeature(ServiceDescription serviceDescription)
        {
            Assert.That(serviceDescription.Features, Has.Some.Matches(new FeatureDescriptionConstraint(new Feature()
            {
                Type = FeatureType.ProofSheet,
                Title = "корректура",
                Description = "Процесс проверки стиля, орфографии, пунктуации и других недостатков"

            })));
        }

        private void checkProofReadingFeature(ServiceDescription serviceDescription)
        {
            Assert.That(serviceDescription.Features, Has.Some.Matches(new FeatureDescriptionConstraint(new Feature()
            {
                Type = FeatureType.ProofReading,
                Title = "вычитка",
                Description = "Дополнительный контроль качества с привлечением отраслевого эксперта"

            })));
        }


    }
}

