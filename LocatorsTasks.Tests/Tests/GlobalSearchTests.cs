using LocatorsTasks.Core.Utilities;
using LocatorsTasks.Tests.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTasks.Tests.Tests
{
    public class GlobalSearchTests : BaseTest
    {
        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void GlobalSearch_ShouldReturnRelevantResults(string searchText)
        {
            //AcceptCookiesIfExists();

            var searchIcon = Wait.Until(d => d.FindElement(By.CssSelector("button[class*='search']")));
            searchIcon.Click();

            var searchInput = Wait.Until(d => d.FindElement(By.XPath("//input[@type='search']")));
            searchInput.Clear();
            searchInput.SendKeys(searchText);

            var findButton = DriverWrapper.GetWebDriver().FindElement(By.XPath("//button[.//span[normalize-space()='Find']]"));
            findButton.Click();

            var resultLinks = Wait.Until(d =>
                {
                    var container = d.FindElement(By.XPath("//div[contains(@class,'search-results__items')]"));
                    var elements = container.FindElements(By.XPath(".//article[contains(@class,'search-results__item')]"));
                    return elements.Count > 0 ? elements : null;
                });

            var notFoundResults = resultLinks.Where(x=>!x.Text.Contains(searchText)).ToList();

            bool allContainKeyword = resultLinks.All(link => link.Text.Contains(searchText, StringComparison.OrdinalIgnoreCase));
            Assert.That(allContainKeyword, Is.True, $"Not all search results contain '{searchText}'");
        }

        private void AcceptCookiesIfExists()
        {
            // Accept cookies if displayed
            try
            {
                var acceptButton = new WebDriverWait(DriverWrapper.GetWebDriver(), TimeSpan.FromSeconds(5))
                    .Until(d => d.FindElement(By.Id("onetrust-accept-btn-handler")));

                acceptButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                // Cookie banner was not displayed
            }
        }
    }
}
