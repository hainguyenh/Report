using ApplyBDDFramework.BaseClass;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using System;
using System.Runtime.CompilerServices;

namespace ApplyBDDFramework.Utils
{
    public class ExtentReportsHelper
    {
        [ThreadStatic]
        private static ExtentTest _parentTest;

        [ThreadStatic]
        private static ExtentTest _childTest;

        /// <summary>
        /// Apply BDD
        /// </summary>
        [ThreadStatic]
        private static ExtentTest _features;
        [ThreadStatic]
        private static ExtentTest _scenarios;
        [ThreadStatic]
        private static ExtentTest _steps;
        [ThreadStatic]
        private static ExtentTest _stepDetails;



        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateParentTest(string testName, string description = null)
        {
            _parentTest = BaseValues.ExtentReports.CreateTest(testName, description);
            return _parentTest;
        }

        // BDD
        // Create the Feature Test instead of Parent
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateFeatureTest(string testName, string description = "")
        {
            _features = BaseValues.ExtentReports.CreateTest<Feature>(testName, description);
            return _features;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTest(string testName, string description = null)
        {
            _childTest = _parentTest.CreateNode(testName, description);
            return _childTest;
        }

        // BDD
        // Create the Scenario Test
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateScenarioTest(string testName, string description = "")
        {
            _scenarios = _features.CreateNode<Scenario>(testName, description);
            return _scenarios;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTestStep(string testName, string description = null)
        {
            _childTest = _childTest.CreateNode(testName, description);
            return _childTest;
        }

        // BDD
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTestStepBDD(string stepType, string testName, string description = "")
        {
            switch (stepType)
            {
                case "Given":
                    _steps = _scenarios.CreateNode<Given>("Given " + testName, description);
                    break;
                case "When":
                    _steps = _scenarios.CreateNode<When>("When " + testName, description);
                    break;
                case "And":
                    _steps = _scenarios.CreateNode<And>("And " + testName, description);
                    break;
                case "Then":
                    _steps = _scenarios.CreateNode<Then>("Then " + testName, description);
                    break;
                default:
                    _steps = _scenarios.CreateNode<And>(testName, description);
                    break;
            }
            return _steps;
        }

        #region "BDD"
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogFail(string pathImg = null, string details = "Details: ")
        {
            if (pathImg != null)
            {
                // log with snapshot
                _stepDetails = _steps.Fail(details, MediaEntityBuilder.CreateScreenCaptureFromBase64String(pathImg).Build());
            }
            else
                _stepDetails = _steps.Fail(details);
            return _stepDetails;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogWarning(string pathImg = null, string details = "Details: ")
        {
            if (pathImg != null)
            {
                _stepDetails = _steps.Warning(details, MediaEntityBuilder.CreateScreenCaptureFromBase64String(pathImg).Build());
            }
            else
                _stepDetails = _steps.Warning(details);
            return _stepDetails;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogInformation(string pathImg = null, string details = "Details: ")
        {
            if (pathImg is null)
                _stepDetails = _steps.Info(details);
            else
            {
                _stepDetails = _steps.Info(details, MediaEntityBuilder.CreateScreenCaptureFromBase64String(pathImg).Build());
            }
            System.Threading.Thread.Sleep(300);
            return _stepDetails;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest LogPass(string pathImg = null, string details = "Details: ")
        {
            if (pathImg is null)
                _stepDetails = _steps.Pass(details);
            else
            {
                _stepDetails = _steps.Pass(details, MediaEntityBuilder.CreateScreenCaptureFromBase64String(pathImg).Build());
            }
            System.Threading.Thread.Sleep(300);
            return _stepDetails;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTest()
        {
            return _steps;
        }
        #endregion

        #region "Original"
        /*

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
        */
        #endregion
    }
}
