using AventStack.ExtentReports;
using ExtentReportWithKlov.BaseClass;
using System;
using System.Runtime.CompilerServices;

namespace ExtentReportWithKlov.Utils
{
    public class ExtentReportsHelper
    {
        [ThreadStatic]
        private static ExtentTest _parentTest;

        [ThreadStatic]
        private static ExtentTest _childTest;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateParentTest(string testName, string description = "")
        {
            _parentTest = BaseValues.ExtentReports.CreateTest(testName, description);
            return _parentTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTest(string testName, string description = "")
        {
            _childTest = _parentTest.CreateNode(testName, description);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTestStep(string testName, string description = "")
        {
            _childTest = _childTest.CreateNode(testName, description);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest AddScreenCapture(string path, string title = "")
        {
            _childTest = _childTest.Info(title, MediaEntityBuilder.CreateScreenCaptureFromBase64String(path).Build());
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogFail(string pathImg = null, string details = "Details: ")
        {
            if (pathImg != null)
            {
                _childTest = _childTest.Fail(details, MediaEntityBuilder.CreateScreenCaptureFromBase64String(pathImg).Build());
            }
            else
                _childTest = _childTest.Fail(details);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogInformation(string pathImg = null, string details = "Details: ")
        {
            if (pathImg is null)
                _childTest = _childTest.Info(details);
            else
            {
                _childTest = _childTest.Pass(details, MediaEntityBuilder.CreateScreenCaptureFromBase64String(pathImg).Build());
            }
            System.Threading.Thread.Sleep(300);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogPass(string pathImg = null, string details = "Details: ")
        {
            if (pathImg is null)
                _childTest = _childTest.Pass(details);
            else
            {
                _childTest = _childTest.Pass(details, MediaEntityBuilder.CreateScreenCaptureFromBase64String(pathImg).Build());
            }
            System.Threading.Thread.Sleep(300);
            return _childTest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTest()
        {
            return _childTest;
        }
    }
}
