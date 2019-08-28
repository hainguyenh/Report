using AventStack.ExtentReports;
using System;

namespace ExtentReportOnly.BaseClass
{
    public static class BaseValues
    {
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        public static ExtentReports ExtentReports { get { return _lazy.Value; } }
    }
}
