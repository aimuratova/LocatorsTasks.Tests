using LocatorsTasks.Core.Driver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTasks.Core.Element
{
    public interface IWebElementWrapper
    {
        void Click();
        void EnterText(string text);
        void ClearText();
        string GetText();
        IWebElement FindElement();
        IWebElement FindChildByName(string childName);
        //void ClickAndSendAction(IWebElement element, string textToSend);
        IWebElement WaitForElementToBePresent(IWebDriverWrapper webDriverWrapper, By by, TimeSpan timeout);
        IWebElement FindChildBy(By by);
    }
}
