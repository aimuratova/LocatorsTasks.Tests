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

        public MainPage(IWebDriverWrapper driver) : base(driver)
        {
            careersLink = new WebElementWrapper(driver, By.PartialLinkText("Care"));
        }

        public CareersPage NavigateToCareersPage()
        {
            careersLink.Click();
            return new CareersPage(driver);
        }
    }
}
