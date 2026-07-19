using LocatorsTasks.Core.Element;
using LocatorsTasks.Core.Utilities;
using LocatorsTasks.Tests.Base;
using OpenQA.Selenium;
using System.Xml.Linq;

namespace LocatorsTasks.Tests.Tests
{
    public class SearchTests : BaseTest
    {
        public SearchTests() { }


        [SetUp]
        public void SetUp()
        {
            Wait.Until(d => d.FindElement(By.PartialLinkText("Care"))).Click();

            Wait.Until(d =>
            {
                var button = d.FindElement(By.Id("onetrust-accept-btn-handler"));
                button.Click();
                return true;
            });

            var startButton = Wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath("//a[contains(@href,'careers.epam.com/en/jobs')]"));
                return elements.Any() ? elements.First() : null;
            });
            startButton.Click();

            Wait.Until(d =>
            {
                var button = d.FindElement(By.Id("onetrust-accept-btn-handler"));
                button.Click();
                return true;
            });
        }

        [TestCase("Java", "Kazakhstan")]
        [TestCase("C#", "Poland")]
        [TestCase("Python", "Germany")]
        public void Careers_SearchText_ReturnsResults(string position, string country)
        {

            // Click country input
            var countryInput = Wait.Until(d =>
                d.FindElement(By.XPath("//input[@aria-label='Choose your country']")));

            countryInput.Click();
            countryInput.Clear();
            countryInput.SendKeys(country);

            var countryOption = Wait.Until(d =>
            {
                var options = d.FindElements(
                    By.XPath($"//div[@data-testid='dropdown-option'][contains(., '{country}')]"));

                return options.Any() ? options.First() : null;
            });

            countryOption.Click();

            WaitForLoader();

            // Name
            var keyword = Wait.Until(d => d.FindElement(By.Name("search")));
            keyword.Clear();
            keyword.SendKeys(position);

            WaitForLoader();

            Wait.Until(d => d.FindElement(By.XPath("//span[normalize-space()='Remote']"))).Click();

            var searchButton = DriverWrapper.GetWebDriver().FindElements(By.CssSelector("button[type='submit']")).First(b => b.Displayed && b.Enabled);
            searchButton.Click();

            // XPath
            var latestJob =
                Wait.Until(d => d.FindElement(By.XPath("(//div[@data-testid='accordion-section-container'])[last()]")));
            latestJob.Click();

            Assert.That(latestJob.Text.ToLower(), Does.Contain(position.ToLower()));

            //Assert.Pass();
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
