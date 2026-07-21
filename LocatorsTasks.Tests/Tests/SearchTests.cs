using LocatorsTasks.Core.Element;
using LocatorsTasks.Core.Utilities;
using LocatorsTasks.Tests.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;
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
            AcceptCookiesIfExists();

            var startButton = Wait.Until(d =>
            {
                var elements = d.FindElements(By.XPath("//a[contains(@href,'careers.epam.com/en/jobs')]"));
                return elements.Any() ? elements.First() : null;
            });
            startButton.Click();
            AcceptCookiesIfExists();
        }

        [TestCase("Java", "Kazakhstan")]
        [TestCase("C#", "Poland")]
        [TestCase("Python", "Germany")]
        public void Careers_SearchText_ReturnsResults(string position, string country)
        {
            var countryInput = Wait.Until(d => d.FindElement(By.XPath("//input[@aria-label='Choose your country']")));

            countryInput.Click();
            countryInput.Clear();                        
            countryInput.SendKeys(country);

            var countryDropdown = Wait.Until(_ =>
            {
                countryInput.Click();
                var countryDropdown = Wait.Until(d => d.FindElement(By.XPath($"//div[contains(@class, 'dropdown__menu')]")));

                return countryDropdown.Displayed ? countryDropdown : null;
            });

            var countryOption = Wait.Until(_ =>
            {
                try
                {
                    var child = countryDropdown.FindElement(By.XPath($"//div[@data-testid='dropdown-option'][contains(., '{country}')]"));
                    return child.Displayed ? child : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    // Reacquire the parent if it became stale
                    countryDropdown = Wait.Until(d => d.FindElement(By.XPath($"//div[contains(@class, 'dropdown__menu')]")));
                    return null;
                }
            });

            countryOption.Click();
            WaitForLoader();


            var keyword = Wait.Until(d => d.FindElement(By.Name("search")));
            keyword.Clear();
            keyword.SendKeys(position);

            WaitForLoader();

            Wait.Until(d => d.FindElement(By.XPath("//span[normalize-space()='Remote']"))).Click();

            var searchButton = Wait.Until(d =>
            {
                var element = d.FindElements(By.CssSelector("button[type='submit']"));
                return element.Count > 0 ? element.First(b => b.Displayed && b.Enabled) : null;
            });
            searchButton.Click();

            WaitForLoader();

            var latestJob = Wait.Until(d => d.FindElement(By.XPath("(//div[@data-testid='accordion-section-container'])[last()]")));
            latestJob.Click();

            Wait.Until(d =>
            {
                var applyButton = d.FindElement(By.XPath("(//*[@id='cta_job_apply_unauthorized'])[last()]"));

                return applyButton.Displayed &&
                       applyButton.Location.Y > 0 &&
                       applyButton.Size.Height > 0;
            });

            var resultDescription = Wait.Until(d => d.FindElement(By.XPath("(//div[@data-testid='accordion-section-container'])[last()]"))).Text;

            Assert.That(resultDescription.ToLower(), Does.Contain(position.ToLower()));
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
