using ExtentReportWithKlov.BaseClass;
using ExtentReportWithKlov.Utils;
using OpenQA.Selenium;

namespace ExtentReportWithKlov.Controls
{
    public class Button : BaseControl
    {
        public Button(IWebElement control)
          : base(control)
        {
        }

        public void Click()
        {
            if (WrappedControl is null)
            {
                throw new NotFoundException("Element not found.");
            }
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(WrappedControl), $"Click on the Button '{WrappedControl.Text}'.");
            WrappedControl.Click();
        }

    }
}
