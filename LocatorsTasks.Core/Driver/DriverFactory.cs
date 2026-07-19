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
                    options.AddArgument("disable-infobars");
                    options.AddArgument(@"--user-data-dir=C:\SeleniumProfile");

                    //options.AddArgument("--incognito");

                    return new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(30));
                default:
                    throw new ArgumentException($"Unsupported browser type: {browserType}");
            }
        }
    }
}
