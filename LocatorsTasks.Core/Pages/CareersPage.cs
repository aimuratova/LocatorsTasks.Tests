using LocatorsTasks.Core.Driver;
using LocatorsTasks.Core.Element;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTasks.Core.Pages
{
    public class CareersPage : BasePage
    {
        private readonly IWebElementWrapper jobsLink;
        public CareersPage(IWebDriverWrapper driver) : base(driver)
        {
            jobsLink = new WebElementWrapper(driver, By.XPath("//a[contains(@href,'careers.epam.com/en/jobs')]"));
        }

        public JobsPage NavigateToJobsPage()
        {
            jobsLink.Click();
            return new JobsPage(driver);
        }
    }
}
