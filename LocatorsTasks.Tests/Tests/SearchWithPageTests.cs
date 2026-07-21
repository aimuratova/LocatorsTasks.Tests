using LocatorsTasks.Core.Pages;
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
    public class SearchWithPageTests : BaseTest
    {
        [TestCase("Java","Poland")]
        [TestCase("C#", "Poland")]
        [TestCase("Python", "Germany")]
        public void SearchWithPage(string jobTitle, string country)
        {
            var mainPage = new MainPage(DriverWrapper);
            mainPage.AcceptCookiesIfDisplayed();

            var careersPage = mainPage.NavigateToCareersPage();
            
            var jobsPage = careersPage.NavigateToJobsPage();
            jobsPage.AcceptCookiesIfDisplayed();

            jobsPage.SelectCountry(country);
            WaitForLoader();
            jobsPage.EnterJobTitle(jobTitle);            
            jobsPage.ClickRemote();            
            WaitForLoader();

            jobsPage.ClickSearchButton();

            WaitForLoader();

            jobsPage.LastSearchResultClick();

            var resultDescription = jobsPage.GetResultDescription();

            Assert.That(resultDescription.ToLower(), Does.Contain(jobTitle.ToLower()));
        }

        private void WaitForLoader()
        {
            Wait.Until(d =>
            {
                var loaders = d.FindElements(
                    By.CssSelector("div.Preloader_fullSize__jIIky"));

                return loaders.Count == 0 || !loaders.Any(x => x.Displayed);
            });
        }
    }
}
