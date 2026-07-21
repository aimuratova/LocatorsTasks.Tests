using LocatorsTasks.Core.Pages;
using LocatorsTasks.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTasks.Tests.Tests
{
    public class GlobalSearchWithPageTests : BaseTest
    {
        public GlobalSearchWithPageTests() { }

        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void GlobalSearch_ShouldReturnRelevantResults(string searchText)
        {
            var mainPage = new MainPage(DriverWrapper);
            mainPage.AcceptCookiesIfDisplayed();

            var searchResultsPage = mainPage.PerformGlobalSearch(searchText);
            
            var resultLinks = searchResultsPage.GetResultLinks();
            
            Assert.That(resultLinks, Is.Not.Empty, "No search results were found.");
            Assert.That(resultLinks.All(link => link.Text.ToLower().Contains(searchText.ToLower())),
                $"Not all search results contain the search text '{searchText}'.");
        }
    }
}
