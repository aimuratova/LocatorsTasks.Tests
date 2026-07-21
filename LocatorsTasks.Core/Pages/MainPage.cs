using LocatorsTasks.Core.Driver;
using LocatorsTasks.Core.Element;
using LocatorsTasks.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTasks.Core.Pages
{
    public class MainPage : BasePage
    {
        private readonly IWebElementWrapper careersLink;
        private readonly IWebElementWrapper searchMagnifier;
        private readonly IWebElementWrapper searchInput;
        private readonly IWebElementWrapper findButton;

        public MainPage(IWebDriverWrapper driver) : base(driver)
        {
            careersLink = new WebElementWrapper(driver, By.PartialLinkText("Care"));
            searchMagnifier = new WebElementWrapper(driver, By.CssSelector("button[class*='search']"));
            searchInput = new WebElementWrapper(driver, By.XPath("//input[@type='search']"));
            findButton = new WebElementWrapper(driver, By.XPath("//button[.//span[normalize-space()='Find']]"));
        }

        public CareersPage NavigateToCareersPage()
        {
            careersLink.Click();
            return new CareersPage(driver);
        }

        public SearchResultPage PerformGlobalSearch(string searchText)
        {
            searchMagnifier.Click();

            searchInput.Click();
            searchInput.ClearText();
            searchInput.EnterText(searchText);

            findButton.Click();

            return new SearchResultPage(driver);
        }
    }
}
