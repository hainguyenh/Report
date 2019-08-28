using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace ExtentReportWithKlov.BaseClass
{
    public static class BaseDriver
    {

        private static IWebDriver _browser;

        public static IWebDriver Browser
        {
            get
            {
                if (_browser == null)
                {
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method Start.");
                }
                return _browser;
            }
            private set => _browser = value;
        }

        public static void StartBrowser(BrowserType browserType = BrowserType.Firefox, int defaultTimeOut = 30)
        {
            switch (browserType)
            {
                case BrowserType.Firefox:
                    break;
                case BrowserType.InternetExplorer:
                    break;
                case BrowserType.Chrome:
                    Browser = new ChromeDriver();
                    break;
                default:
                    throw new ArgumentException("You need to set a valid browser type.");
            }
        }

        public static void StopBrowser()
        {
            Browser.Quit();
            Browser = null;
            Browser.Dispose();
        }

    }
}
