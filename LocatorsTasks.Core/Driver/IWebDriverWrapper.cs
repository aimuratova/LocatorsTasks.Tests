using LocatorsTasks.Core.Element;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTasks.Core.Driver
{
    public interface IWebDriverWrapper
    {
        void StartBrowser(int implicitWaitTime);
        void Close();
        void NavigateTo(string url);
        void WindowMaximize();
        string GetTitle();
        string GetUrl();
        IWebDriver GetWebDriver();
    }
}
