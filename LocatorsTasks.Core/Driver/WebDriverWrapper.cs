using LocatorsTasks.Core.Element;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocatorsTasks.Core.Driver
{
    public class WebDriverWrapper : IWebDriverWrapper
    {
        private readonly IWebDriver _driver;
        
        public WebDriverWrapper(BrowserType browserType)
        {
            _driver = DriverFactory.CreateDriver(browserType);
        }

        public void StartBrowser(int implicitWaitTime)
        {
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWaitTime);
        }

        public void Close()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        public void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void WindowMaximize()
        {
            _driver.Manage().Window.Maximize();
        }

        public string GetTitle()
        {
            return _driver.Title;
        }

        public string GetUrl()
        {
            return _driver.Url;
        }

        public IWebDriver GetWebDriver()
        {
            return _driver;
        }

    }
}
