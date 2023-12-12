using CodeTest.PageObjects;
using SeleniumAutomation;
using CodeTest.Support;

namespace CodeTest.StepDefinitions
{
    [Binding]
    public sealed class DataTableStepDefinitions
    {
        private driverManager driverManager;
        DataTableSelection pageDataTable;
        ExtentReportsHelper reporter;

        public DataTableStepDefinitions(driverManager _driver, ExtentReportsHelper rHelper)
        {
            driverManager = _driver;
            reporter = rHelper;
            pageDataTable = new DataTableSelection(driverManager, reporter);
        }


        [When(@"the user is on the ""([^""]*)"" table")]
        public void WhenTheUserIsOnTheTable(string checkbox)
        {
            pageDataTable.scrollTillTextFound(checkbox);
        }

        [Then(@"click the ""([^""]*)"" row")]
        public void ThenClickTheRow(string rowText)
        {
            pageDataTable.click(rowText);
        }


    }
}