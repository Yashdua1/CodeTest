﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;

namespace CodeTest.Support
{
    public class ExtentReportsHelper
    {
        public ExtentReports extent { get; set; }
        public ExtentSparkReporter  reporter { get; set; }
        public ExtentTest test { get; set; }
        public ExtentReportsHelper()
        {
            extent = new ExtentReports();
            reporter = new ExtentSparkReporter(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ExtentReports.html"));
            reporter.Config.DocumentTitle = "Automation Testing Report";
            reporter.Config.ReportName = "Regression Testing";
            reporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Standard;
            extent.AttachReporter(reporter);
            extent.AddSystemInfo("Application Under Test", "Code test");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Machine", Environment.MachineName);
            extent.AddSystemInfo("OS", Environment.OSVersion.VersionString);
        }
        public void CreateTest(string testName)
        {
            test = extent.CreateTest(testName);
        }
        public void SetStepStatusPass(string stepDescription)
        {
            test.Log(Status.Pass, stepDescription);
        }
        public void SetStepStatusWarning(string stepDescription)
        {
            test.Log(Status.Warning, stepDescription);
        }
        public void SetTestStatusPass()
        {
            test.Pass("Test Executed Sucessfully!");
        }
        public void SetTestStatusFail(string message = null)
        {
            var printMessage = "<p><b>Test FAILED!</b></p>";
            if (!string.IsNullOrEmpty(message))
            {
                printMessage += $"Message: <br>{message}<br>";
            }
            test.Fail(printMessage);
        }
        public void AddTestFailureScreenshot(string base64ScreenCapture)
        {
            test.AddScreenCaptureFromBase64String(base64ScreenCapture, "Screenshot on Error:");
        }
        public void SetTestStatusSkipped()
        {
            test.Skip("Test skipped!");
        }
        public void Close()
        {
            extent.Flush();
        }
    }
}
