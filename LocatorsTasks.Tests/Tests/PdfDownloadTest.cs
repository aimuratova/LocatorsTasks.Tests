using LocatorsTasks.Tests.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LocatorsTasks.Tests.Tests
{
    public class PdfDownloadTest : BaseTest
    {
        public PdfDownloadTest() { }

        [TestCase("Code-Of-Conduct_01_26.pdf")]
        public void PdfShouldBeDownloaded(string fileName)
        {
            AcceptCookiesIfExists();
            var downloadFolder = Path.Combine(Path.GetTempPath(), "Downloads");
            Directory.CreateDirectory(downloadFolder);

            var pdf = DriverWrapper.GetWebDriver().FindElement(By.XPath("//a[contains(@href,'Code-Of-Conduct_01_26.pdf')]"));

            ((IJavaScriptExecutor)DriverWrapper.GetWebDriver()).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", pdf);

            pdf.Click();

            var isFileDownloaded = WaitForDownload(downloadFolder, fileName);
            Assert.That(isFileDownloaded, Is.True);
        }

        private bool WaitForDownload(string folder, string fileName, int timeoutSeconds = 30)
        {
            var wait = new WebDriverWait(new SystemClock(), DriverWrapper.GetWebDriver(),
                TimeSpan.FromSeconds(timeoutSeconds),
                TimeSpan.FromMilliseconds(500));

            return wait.Until(_ =>
            {
                var path = Path.Combine(folder, fileName);
                return File.Exists(path) && !File.Exists(path + ".crdownload");
            });
        }

        private void AcceptCookiesIfExists()
        {
            // Accept cookies if displayed
            try
            {
                var acceptButton = new WebDriverWait(DriverWrapper.GetWebDriver(), TimeSpan.FromSeconds(5))
                    .Until(d => d.FindElement(By.Id("onetrust-accept-btn-handler")));

                acceptButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                // Cookie banner was not displayed
            }
        }
    }
}
