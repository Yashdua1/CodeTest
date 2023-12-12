using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CodeTest.Support;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace SeleniumAutomation.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private static driverManager driverManager;
        private static ScenarioContext scenario;
        private static ExtentReportsHelper extentHelper;

        public Hooks(driverManager _driver, ScenarioContext _scenario, ExtentReportsHelper reportHelper)
        {
            driverManager = _driver;
            scenario = _scenario;
            extentHelper = reportHelper;
        }

        /*[BeforeTestRun]
        public static void BeforeTestSetup()
        {
            
        }*/

        [BeforeScenario("@Smoke"), BeforeScenario("@Regression")]
        public void BeforeScenarioWithTag()
        {
            driverManager._extentReport = new ExtentReportsHelper();

            driverManager._extentReport.CreateTest(TestContext.CurrentContext.Test.Name);

            ChromeOptions chromeOpt = new ChromeOptions();

            driverManager._driver = new ChromeDriver(chromeOpt);
            driverManager._driver.Navigate().GoToUrl("https://www.primefaces.org/primereact-v5/#/datatable/selection");
            driverManager._driver.Manage().Window.Maximize();
            driverManager._driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
        }

        
        [AfterTestRun]
        public static void AfterTestRun()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = TestContext.CurrentContext.Result.StackTrace;
                var errorMessage = "<pre>" + TestContext.CurrentContext.Result.Message + "</pre>";
                switch (status)
                {
                    case TestStatus.Failed:
                        driverManager._extentReport.SetTestStatusFail($"<br>{errorMessage}<br>Stack Trace: <br>{stacktrace}<br>");
                        string errorText = scenario.ScenarioInfo.Title;
                        driverManager._extentReport.AddTestFailureScreenshot(errorText);
                        driverManager.screenCaptureAsImage(errorText);
                        break;
                    case TestStatus.Skipped:
                        driverManager._extentReport.SetTestStatusSkipped();
                        break;
                    default:
                        driverManager._extentReport.SetTestStatusPass();
                        break;
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                driverManager._extentReport.Close();
                driverManager._driver.Quit();
            }

        }

    }
}