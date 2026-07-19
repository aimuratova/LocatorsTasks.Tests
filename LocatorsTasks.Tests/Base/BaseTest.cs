using LocatorsTasks.Core.Driver;
using LocatorsTasks.Core.Utilities;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTasks.Tests.Base
{
    public class BaseTest
    {
        protected IWebDriverWrapper DriverWrapper { get; private set; }
        protected WebDriverWait Wait { get; private set; }

        [SetUp]
        public void OneTimeSetUp()
        {
            Configuration.LoadConfiguration();

            if (Configuration.BrowserType == null)
                throw new InvalidOperationException("BrowserType is not configured");

            if (string.IsNullOrWhiteSpace(Configuration.AppUrl))
                throw new InvalidOperationException("AppUrl is not configured");

            DriverWrapper = new WebDriverWrapper(Configuration.BrowserType.Value);
            DriverWrapper.StartBrowser(Configuration.ImplicitWaitTime);
            DriverWrapper.NavigateTo(Configuration.AppUrl);

            Wait = new WebDriverWait(DriverWrapper.GetWebDriver(), TimeSpan.FromSeconds(Configuration.ImplicitWaitTime));
        }

        [TearDown]
        public void OneTimeTearDown()
        {
            DriverWrapper.Close();
        }
    }
}
