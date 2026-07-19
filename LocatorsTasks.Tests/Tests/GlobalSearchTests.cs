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


            // Act
            // 1. Click magnifier icon
            var searchIcon = new WebDriverWait(DriverWrapper.GetWebDriver(), TimeSpan.FromSeconds(10))
                .Until(d => d.FindElement(
                    By.CssSelector("button[class*='search']")));

            searchIcon.Click();


            // 2. Find search input
            var searchInput = new WebDriverWait(DriverWrapper.GetWebDriver(), TimeSpan.FromSeconds(10))
                .Until(d => d.FindElement(
                    By.XPath("//input[@placeholder='Search']")));

            searchInput.Clear();
            searchInput.SendKeys(searchText);


            // 3. Click Find button
            var findButton = DriverWrapper.GetWebDriver().FindElement(
                By.XPath("//button[contains(text(),'Find')]"));

            findButton.Click();


            // Assert
            var resultLinks = new WebDriverWait(DriverWrapper.GetWebDriver(), TimeSpan.FromSeconds(10))
                .Until(d =>
                {
                    var elements = d.FindElements(
                        By.XPath("//a[contains(@href,'/content')]"));

                    return elements.Count > 0 ? elements : null;
                });


            bool allContainKeyword = resultLinks
                .All(link =>
                    link.Text.Contains(
                        searchText,
                        StringComparison.OrdinalIgnoreCase));


            Assert.That(
                allContainKeyword,
                Is.True,
                $"Not all search results contain '{searchText}'");
        }
    }
}
