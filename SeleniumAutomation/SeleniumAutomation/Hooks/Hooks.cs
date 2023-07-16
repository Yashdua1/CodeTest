using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;

namespace SeleniumAutomation.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private driverManager driverManager;

        public Hooks(driverManager _driver)
        {
            driverManager = _driver;
        }

        [BeforeScenario("@Smoke"), BeforeScenario("@Regression")]
        public void BeforeScenarioWithTag()
        {
            ChromeOptions chromeOpt = new ChromeOptions();

            driverManager._driver = new ChromeDriver(chromeOpt);
            driverManager._driver.Navigate().GoToUrl("http://www.amazon.in");
            driverManager._driver.Manage().Window.Maximize();
            driverManager._driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario()
        {
              
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driverManager._driver.Quit();           
        }
    }
}