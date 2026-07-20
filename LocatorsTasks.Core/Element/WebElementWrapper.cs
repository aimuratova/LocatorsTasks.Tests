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
        private readonly By _by;

        public WebElementWrapper(IWebDriverWrapper webDriverWrapper, By by, int waitTimeInSeconds = 10)
        {
            _webDriverWrapper = webDriverWrapper;
            _by = by;
            _timeout = TimeSpan.FromSeconds(waitTimeInSeconds);
        }

        public void Click()
        {
            var element = WaitForElementToBePresent(_webDriverWrapper, _by, _timeout);
            new Actions(_webDriverWrapper.GetWebDriver()).MoveToElement(element).Click().Perform();
        }

        public void EnterText(string text)
        {
            var element = WaitForElementToBePresent(_webDriverWrapper, _by, _timeout);
            element.Clear();
            element.SendKeys(text);
        }

        public void ClearText()
        {
            var element = WaitForElementToBePresent(_webDriverWrapper, _by, _timeout);

            element.Click();
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
        }

        public string GetText()
        {
            var element = WaitForElementToBePresent(_webDriverWrapper, _by, _timeout);
            return element.Text;
        }

        public IWebElement FindElement()
        {
            var elementPresent = WaitForElementToBePresent(_webDriverWrapper, _by, _timeout);
            return elementPresent;
        }

        public IWebElement FindChildByName(string childName)
        {
            var elementParent = WaitForElementToBePresent(_webDriverWrapper, _by, _timeout);
            return elementParent.FindElement(By.Name(childName));
        }

        //public void ClickAndSendAction(IWebElement element, string textToSend)
        //{
        //    var clickAndSendKeysActions = new Actions(_webDriverWrapper.GetWebDriver());
        //    clickAndSendKeysActions.Click(element)
        //        .Pause(TimeSpan.FromSeconds(1))
        //        .SendKeys(textToSend)
        //        .Perform();
        //}

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

        public IWebElement FindChildBy(By by)
        {
            var parent = WaitForElementToBePresent(_webDriverWrapper, _by, _timeout);

            var wait = new WebDriverWait(_webDriverWrapper.GetWebDriver(), _timeout);

            return wait.Until(_ =>
            {
                try
                {
                    var child = parent.FindElement(by);
                    return child.Displayed ? child : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    // Reacquire the parent if it became stale
                    parent = WaitForElementToBePresent(_webDriverWrapper, _by, _timeout);
                    return null;
                }
            });
        }
    }
}
