using AventStack.ExtentReports;
using ExtentReportWithKlov.Utils;
using OpenQA.Selenium;
using System;

namespace ExtentReportWithKlov.BaseClass
{
    public class BaseControl
    {
        internal IWebElement WrappedControl { get; private set; }

        protected void CaptureStepAndLogInfo(IWebElement control, string message = "")
        {
            if (control is null)
                CaptureStepAndLogInfo(message);
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(control), message);
            }
        }

        protected void CaptureStepAndLogInfo(string message = "")
        {
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(WrappedControl), message);
        }

        protected BaseControl(IWebElement control)
        {
            WrappedControl = control;
        }
    }
}
