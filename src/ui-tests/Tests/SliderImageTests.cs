using System;
using NUnit.Framework;
using UITests.Pages;

namespace UITests.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixtureSource(typeof(BrowserFixture))]
    public class SliderImageTests : BaseTest
    {
        public static String[] Tabs = { "Снижение издержек", "Инновации сервиса", "Качество перевода", "Автоматизация процессов", "Управление переводом" };
        //public static String[] Tabs = { "Снижение издержек", "Инновации сервиса" };

        public SliderImageTests(Type browserType) : base(browserType)
        {
        }

        [Test, Pairwise, Timeout(10000)]
        public void SliderImageShouldChangeAfterChangeTab([ValueSource("Tabs")] String fromTab, [ValueSource("Tabs")] String toTab)
        {
            // TODO: Create generator with excluded cases
            if (fromTab == toTab)
            {
                Assert.Ignore("Exclude case with same tabs");
            }

            MainPage page = new MainPage(Driver);

            page.ClickOnTab(fromTab);
            String fromTabPosition = page.GetSliderImagePosition();

            page.ClickOnTab(toTab);
            String toTabPosition = page.GetSliderImagePosition();

            // TODO: Add comparison for image in this position
            Assert.That(fromTabPosition, Is.Not.EqualTo(toTabPosition), "Sprite position not changed.");
        }
    }
}
