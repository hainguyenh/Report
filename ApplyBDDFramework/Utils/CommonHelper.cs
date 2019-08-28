using OpenQA.Selenium;
using ApplyBDDFramework.BaseClass;

namespace ApplyBDDFramework.Utils
{
    public static class CommonHelper
    {
        /// <summary>
        /// Capture screen and highlight the control, if does not have the control, the system will capture current screen
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static string CaptureScreen(IWebElement control = null)
        {
            HighLightElement(control);

            ITakesScreenshot ts = (ITakesScreenshot)BaseDriver.Browser;
            Screenshot screenshot = ts.GetScreenshot();
            string image = screenshot.AsBase64EncodedString;
            RemoveHighLightElement(control);
            return image;
        }

        public static string CaptureScreen(BaseControl control)
        {
            HighLightElement(control.WrappedControl);

            ITakesScreenshot ts = (ITakesScreenshot)BaseDriver.Browser;
            Screenshot screenshot = ts.GetScreenshot();
            string image = screenshot.AsBase64EncodedString;
            RemoveHighLightElement(control.WrappedControl);
            return image;
        }

        private static void HighLightElement(IWebElement iControl = null)
        {
            if (iControl != null)
            {
                IJavaScriptExecutor js = BaseDriver.Browser as IJavaScriptExecutor;
                js.ExecuteScript("arguments[0].scrollIntoView({block: 'end'});", iControl);
                js.ExecuteScript("arguments[0].style='border: 2px solid red;'", iControl);
            }
        }

        private static void RemoveHighLightElement(IWebElement iControl = null)
        {
            if (iControl != null)
            {
                IJavaScriptExecutor js = BaseDriver.Browser as IJavaScriptExecutor;
                js.ExecuteScript("arguments[0].style=''", iControl);
            }
        }
    }
}