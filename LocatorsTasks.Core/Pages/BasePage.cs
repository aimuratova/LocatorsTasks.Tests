using LocatorsTasks.Core.Driver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTasks.Core.Pages
{
    public class BasePage
    {
        protected IWebDriverWrapper driver;

        protected BasePage(IWebDriverWrapper driver)
        {
            this.driver = driver;
        }

        public void AcceptCookiesIfDisplayed()
        {
            try
            {
                var acceptButton = driver.GetWebDriver().FindElement(By.Id("onetrust-accept-btn-handler"));
                acceptButton.Click();
            }
            catch (NoSuchElementException)
            {
                // Cookie banner was not displayed
            }
        }
    }
}
