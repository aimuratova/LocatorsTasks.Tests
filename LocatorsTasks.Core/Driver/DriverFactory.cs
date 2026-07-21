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

                    options.AddArgument(@"user-data-dir=C:\Users\Myrzaliyev\Desktop\Epam\Work\Selenium");

                    options.AddArgument("--profile-directory=Default");

                    var downloadFolder = Path.Combine(Path.GetTempPath(), "Downloads");
                    options.AddUserProfilePreference("download.default_directory", downloadFolder);
                    options.AddUserProfilePreference("download.prompt_for_download", false);
                    options.AddUserProfilePreference("download.directory_upgrade", true);
                    options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

                    return new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(30));

                case BrowserType.Firefox:
                    return new OpenQA.Selenium.Firefox.FirefoxDriver();
                default:
                    throw new ArgumentException($"Unsupported browser type: {browserType}");
            }
        }
    }
}
