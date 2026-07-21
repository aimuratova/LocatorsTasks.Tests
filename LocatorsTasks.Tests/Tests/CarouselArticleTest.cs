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
    public class CarouselArticleTest : BaseTest
    {
        public CarouselArticleTest() { }

        [Test]
        public void CarouselArticleShouldMatchOpenedArticle()
        {
            AcceptCookiesIfExists();

            DriverWrapper.GetWebDriver().FindElement(By.CssSelector("a.top-navigation__item-link[href='/insights']")).Click();

            var next = Wait.Until(d =>
            {
                return d.FindElement(By.CssSelector("button.slider__right-arrow"));
            });                

            next.Click();
            Wait.Until(d => true);

            next.Click();

            string expected = Wait.Until(d => d.FindElement(By.XPath("//div[contains(@class,'owl-item active')]//div[contains(@class,'text-image-slide-ui__parsys--2')]//span[contains(@class,'font-size-44')]"))).Text.Trim();
            string oldUrl = DriverWrapper.GetWebDriver().Url;

            DriverWrapper.GetWebDriver().FindElement(By.XPath("//div[contains(@class,'owl-item active')]//a[contains(@class,'slider-cta-link')]")).Click();

            Wait.Until(d => d.Url != oldUrl);

            string actual = Wait.Until(d => d.FindElement(By.TagName("h1"))).Text.Trim();

            Assert.That(expected.Trim().ToLower().Contains(actual.Trim().ToLower()), Is.True);
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
