using ApplyBDDFramework.Utils;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Reflection;
using TechTalk.SpecFlow;

namespace ApplyBDDFramework.BaseClass
{
    [Binding]
    public sealed class TestFixtureOfBDD
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            CleanUpAllDriver();
            BaseDriver.StartBrowser(BrowserType.Chrome);

            // Create the folder report
            DateTime now = DateTime.Now;
            var dirReport = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Report " + now.ToString("ddd dd-MMM-yyyy HH-mm-ss/");
            var htmlReporter = new ExtentHtmlReporter(dirReport);

            htmlReporter.LoadConfig(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\extent-config.xml");
            // Klov***************

            BaseValues.KlovReport.InitMongoDbConnection("127.0.0.1", 27017);
            // URL of the KLOV server
            BaseValues.KlovReport.InitKlovServerConnection("http://127.0.0.1:5689");

            BaseValues.KlovReport.ProjectName = "Extent Report - Klov";
            BaseValues.KlovReport.ReportName = "Test results " + DateTime.Now.ToString();

            try
            {
                BaseValues.ExtentReports.AttachReporter(BaseValues.KlovReport, htmlReporter);
            }
            catch (TimeoutException)
            {
                throw new TimeoutException("Please check the connection to Mongo DB server. A timeout occured after 30s selecting a server using CompositeServerSelector");
            }
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext _featureContext)
        {
            //Create dynamic feature name
            ExtentReportsHelper.CreateFeatureTest(_featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void ScenarioSetUp(ScenarioContext _scenarioContext)
        {
            //Create dynamic scenario name 
            ExtentReportsHelper.CreateScenarioTest(_scenarioContext.ScenarioInfo.Title);
        }

        [BeforeStep]
        public void BeforeSteps()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            ExtentReportsHelper.CreateTestStepBDD(stepType, ScenarioStepContext.Current.StepInfo.Text);
        }

        [AfterStep]
        public void InsertReportingAfterSteps(ScenarioContext _scenarioContext)
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            // Handle spending steps
            if (_scenarioContext.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
            {
                ExtentReportsHelper.LogWarning(stepType, ScenarioStepContext.Current.StepInfo.Text);
            }
            // Handle failed steps
            else if (_scenarioContext.TestError != null)
            {
                ExtentReportsHelper.LogFail(stepType, ScenarioStepContext.Current.StepInfo.Text);
            }
        }



        [AfterScenario]
        public void ScenarioTearDown(ScenarioContext _scenarioContext)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)
                    ? ""
                    : $"<pre>{TestContext.CurrentContext.Result.Message}</pre>";
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
            ExtentReportsHelper.CreateTestStepBDD(CommonHelper.CaptureScreen(), $"The Scenario '{_scenarioContext.ScenarioInfo.Title}' ended with status {logstatus}").Info(stacktrace);
        }

        [AfterTestRun]
        public static void AfterRunAllTest()
        {
            BaseValues.ExtentReports.Flush();
            CleanUpAllDriver();
        }




        #region "CleanUp"
        private static void CleanUpAllDriver()
        {
            KillProcessAndChildren("chromedriver.exe");
        }

        private static void KillProcessAndChildren(string p_name)
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

        private static void KillProcessAndChildren(int pid)
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
