using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using ExtentReportOnly.Utils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Reflection;

namespace ExtentReportOnly.BaseClass
{
    public class TestFixture
    {
        protected TestFixture()
        {
            CleanUpAllDriver();
            BaseDriver.StartBrowser(BrowserType.Chrome);

            // Create the folder report
            DateTime now = DateTime.Now;
            var dirReport = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Report " + now.ToString("ddd dd-MMM-yyyy HH-mm-ss/");
            var htmlReporter = new ExtentHtmlReporter(dirReport);

            htmlReporter.LoadConfig(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\extent-config.xml");
            BaseValues.ExtentReports.AttachReporter(htmlReporter);
        }

        [OneTimeSetUp]
        public void ClassInit()
        {
            ExtentReportsHelper.CreateParentTest(GetType().Name);
        }

        [SetUp]
        public void TestInit()
        {
            // Create Test Set
            ExtentReportsHelper.CreateTest(TestContext.CurrentContext.Test.Name);
        }


        [TearDown]
        public void TestCleanup()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.Message);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            ExtentReportsHelper.GetTest().Log(logstatus, "Test ended with " + logstatus + stacktrace);
            if (logstatus == Status.Fail)
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen());

        }

        [OneTimeTearDown]
        public void ClassCleanup()
        {
            // Runs once after all tests in this class are executed. (Optional)
            // Not guaranteed that it executes instantly after all tests from the class.
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            if (status == TestStatus.Failed && ExtentReportsHelper.GetTest() == null)
            {
                ExtentReportsHelper.CreateTest(TestContext.CurrentContext.Test.Name);
                ExtentReportsHelper.LogFail(null, "Test Fail.");
            }

            BaseValues.ExtentReports.Flush();

            // Disppose the instance of webdriver
            BaseDriver.StopBrowser();
            CleanUpAllDriver();
        }

        #region "CleanUp"
        private void CleanUpAllDriver()
        {
            KillProcessAndChildren("chromedriver.exe");
        }

        private void KillProcessAndChildren(string p_name)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where Name = '" + p_name + "'");

            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                try
                {
                    KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
                }
                catch (ArgumentException)
                {
                    break;
                }
            }

        }

        private void KillProcessAndChildren(int pid)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
             ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {

                try
                {
                    KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
                }
                catch
                {
                    break;
                }
            }

            try
            {
                Process proc = Process.GetProcessById(pid);

                proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }
        #endregion
    }
}
