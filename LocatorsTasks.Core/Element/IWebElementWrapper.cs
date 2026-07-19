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
        void Click(By by);
        void EnterText(By by, string text);
        void ClearText(By by);
        string GetText(By by);
        IWebElement FindElement(By by);
        IWebElement FindChildByName(By byParent, string childName);
        void ClickAndSendAction(IWebElement element, string textToSend);
        IWebElement WaitForElementToBePresent(IWebDriverWrapper webDriverWrapper, By by, TimeSpan timeout);
    }
}
