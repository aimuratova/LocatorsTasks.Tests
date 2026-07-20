using LocatorsTasks.Core.Driver;
using LocatorsTasks.Core.Element;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTasks.Core.Pages
{
    public class JobsPage : BasePage
    {
        private readonly IWebElementWrapper countryInput;
        private readonly IWebElementWrapper countryDropdown;
        private readonly IWebElementWrapper jobTitleInput;
        private readonly IWebElementWrapper remoteCheckbox;
        private readonly IWebElementWrapper searchButton;
        private readonly IWebElementWrapper lastSearchResult;

        public JobsPage(IWebDriverWrapper driver) : base(driver)
        {
            countryInput = new WebElementWrapper(driver, By.XPath("//input[@aria-label='Choose your country']"));
            countryDropdown = new WebElementWrapper(driver, By.XPath($"//div[contains(@class, 'dropdown__menu')]"));
            jobTitleInput = new WebElementWrapper(driver, By.Name("search"));
            remoteCheckbox = new WebElementWrapper(driver, By.XPath("//span[normalize-space()='Remote']"));
            searchButton = new WebElementWrapper(driver, By.CssSelector("button[type='submit']"));
            lastSearchResult = new WebElementWrapper(driver, By.XPath("(//div[@data-testid='accordion-section-container'])[last()]"));
        }

        public void ClickRemote()
        {
            var element = remoteCheckbox.FindElement();
            if (!element.Selected)
            {
                element.Click();
            }
        }

        public void ClickSearchButton()
        {
            searchButton.Click();            
        }

        public void EnterJobTitle(string jobTitle)
        {
            jobTitleInput.Click();
            jobTitleInput.ClearText();
            jobTitleInput.EnterText(jobTitle);
        }

        public string GetResultDescription()
        {
            //return lastSearchResult.GetText();
            var wait = new WebDriverWait(driver.GetWebDriver(), TimeSpan.FromSeconds(10));

            var description = wait.Until(d =>
            {
                var el = d.FindElement(By.XPath("(//div[@data-testid='accordion-section-container'])[last()]"));
                el.Click();
                return !string.IsNullOrWhiteSpace(el.Text) ? el : null;
            });

            return description.Text;
        }

        public void LastSearchResultClick()
        {
            lastSearchResult.Click();
            //var wait = new WebDriverWait(driver.GetWebDriver(), TimeSpan.FromSeconds(10));

            //var result = wait.Until(d => lastSearchResult.FindElement());

            //result.Click();
        }

        public void SelectCountry(string country)
        {
            countryInput.Click();
            countryInput.ClearText();
            countryInput.EnterText(country);

            countryDropdown.FindElement();
            var option = countryDropdown.FindChildBy(By.XPath($"//div[@data-testid='dropdown-option'][contains(., '{country}')]"));
            option.Click();
        }
    }
}
