using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using UITests.Pages;

namespace UITests.Helpers
{
    public static class PageHelpers
    {
        public static IEnumerable<Func<IWebDriver, BasePage>> GetAllPageInitializers()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var pageClass in assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(BasePage))))
                {
                    yield return new Func<IWebDriver, BasePage>(d => (BasePage)Activator.CreateInstance(pageClass, d));
                }
            }
        }
    }
}

