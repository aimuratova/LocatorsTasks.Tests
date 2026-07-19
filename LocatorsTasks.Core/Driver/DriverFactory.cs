using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTasks.Core.Driver
{
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    ChromeOptions options = new();
                    options.AddArgument("--start-maximized");
                    options.AddArgument("--disable-notifications");
                    options.AddArgument("--disable-popup-blocking");

                    return new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(30));

                case BrowserType.Firefox:
                    return new OpenQA.Selenium.Firefox.FirefoxDriver();
                default:
                    throw new ArgumentException($"Unsupported browser type: {browserType}");
            }
        }
    }
}
