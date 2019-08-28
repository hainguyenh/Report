using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;

namespace ExtentReportWithKlov.BaseClass
{
    public static class BaseValues
    {
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());
        public static ExtentReports ExtentReports { get { return _lazy.Value; } }


        // Klov
        private static readonly Lazy<ExtentKlovReporter> _lazyKlov = new Lazy<ExtentKlovReporter>(() => new ExtentKlovReporter());
        public static ExtentKlovReporter KlovReport { get { return _lazyKlov.Value; } }
    }
}
