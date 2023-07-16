using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace SeleniumAutomation.StepDefinitions
{
    [Binding]
    public class SearchStepDefinitions
    {
        private driverManager driverManager;

        public SearchStepDefinitions(driverManager driver) 
        {
            driverManager = driver;
        }

        [Given(@"HomePage is ""([^""]*)""")]
        public void GivenHomePageIs(string strSite)
        {

            Assert.That("Online Shopping site in India: Shop Online for Mobiles, Books, Watches, Shoes and More - Amazon.in" == driverManager._driver.Title);

        }


        [When(@"Search with keyword ""([^""]*)""")]
        public void WhenSearchWithKeyword(string strInput)
        {
            driverManager._driver.FindElement(By.Id("twotabsearchtextbox")).Clear();
            driverManager._driver.FindElement(By.Id("twotabsearchtextbox")).SendKeys(strInput);
            driverManager._driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driverManager._driver.FindElement(By.Id("nav-search-submit-button")).Click();

            
        }

        [Then(@"Correct results are shown")]
        public void ThenCorrectResultsAreShown()
        {
            //
        }
    }
}
