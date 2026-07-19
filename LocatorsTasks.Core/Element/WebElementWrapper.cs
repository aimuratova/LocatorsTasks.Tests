using LocatorsTasks.Core.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTasks.Core.Element
{
    public class WebElementWrapper : IWebElementWrapper
    {
        private readonly IWebDriverWrapper _webDriverWrapper;
        private readonly TimeSpan _timeout;

        public WebElementWrapper(IWebDriverWrapper webDriverWrapper, int waitTimeInSeconds)
        {
            _webDriverWrapper = webDriverWrapper;
            _timeout = TimeSpan.FromSeconds(waitTimeInSeconds);
        }

        public void Click(By by)
        {
            var element = WaitForElementToBePresent(_webDriverWrapper, by, _timeout);
            new Actions(_webDriverWrapper.GetWebDriver()).MoveToElement(element).Click().Perform();
        }

        public void EnterText(By by, string text)
        {
            var element = WaitForElementToBePresent(_webDriverWrapper, by, _timeout);
            element.Clear();
            element.SendKeys(text);
        }

        public void ClearText(By by)
        {
            var element = WaitForElementToBePresent(_webDriverWrapper, by, _timeout);

            element.Click();
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
        }

        public string GetText(By by)
        {
            var element = WaitForElementToBePresent(_webDriverWrapper, by, _timeout);
            return element.Text;
        }

        public IWebElement FindElement(By by)
        {
            var elementPresent = WaitForElementToBePresent(_webDriverWrapper, by, _timeout);
            return elementPresent;
        }

        public IWebElement FindChildByName(By byParent, string childName)
        {
            var elementParent = WaitForElementToBePresent(_webDriverWrapper, byParent, _timeout);
            return elementParent.FindElement(By.Name(childName));
        }

        public void ClickAndSendAction(IWebElement element, string textToSend)
        {
            var clickAndSendKeysActions = new Actions(_webDriverWrapper.GetWebDriver());
            clickAndSendKeysActions.Click(element)
                .Pause(TimeSpan.FromSeconds(1))
                .SendKeys(textToSend)
                .Perform();
        }

        public IWebElement WaitForElementToBePresent(IWebDriverWrapper webDriverWrapper, By by, TimeSpan timeout)
        {
            var wait = new WebDriverWait(_webDriverWrapper.GetWebDriver(), _timeout);

            return wait.Until(drv =>
            {
                try
                {
                    var el = drv.FindElement(by);
                    return el.Displayed ? el : null;
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("WaitForElementToBePresent method: 'NoSuchElementException' is found.");
                }
                return null;
            });
        }
    }
}
