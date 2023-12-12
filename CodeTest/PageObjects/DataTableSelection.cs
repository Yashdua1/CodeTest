using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using CodeTest.Support;
using SeleniumAutomation;
using NUnit.Framework;

namespace CodeTest.PageObjects
{
    public class DataTableSelection 
    {

        driverManager driver;
        //ExtentReportsHelper reporter;

        public DataTableSelection(driverManager driver, ExtentReportsHelper rHelper)
        {
            this.driver = driver;
            PageFactory.InitElements(driver._driver, this);
           // reporter = rHelper;
        }


        public void scrollTillTextFound(string strFind)
        {
            IWebElement lblText = driver._driver.FindElement(By.XPath("//h5[normalize-space()='" + strFind + "']"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver._driver;
            
            js.ExecuteScript("arguments[0].scrollIntoView();", lblText);

            driver._driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
            driver._extentReport.SetStepStatusPass("Text " + strFind + " scrolled successfully on the DataTable Page");
        }

        public void click(string strRowText)
        {
            try
            {
                int rows = driver._driver.FindElements(By.XPath("(//table[@role='grid'])[6]//tr")).Count;

                for (int iRow = 1; iRow <= rows; iRow++)
                {
                    int numOfCell = driver._driver.FindElements(By.XPath("(//table[@role='grid'])[6]/tbody/tr["+iRow+"]/td")).Count;

                    for (int iCell = 1; iCell <= numOfCell; iCell++)
                    {
                        String category = driver._driver.FindElement(By.XPath("(//table[@role='grid'])[6]/tbody/tr["+iRow+"]/td[3]")).Text;

                        if (category.Equals("Blue Band"))
                        {
                           driver._driver.FindElement(By.XPath("(//table[@role='grid'])[6]/tbody/tr["+iRow+"]/td[1]")).Click();
                            break;
                        }

                    }

                }
                
                //Assert.That(rows, Has.Count.EqualTo(10));
                
                driver._extentReport.SetStepStatusPass(strRowText + " Clicked successfully on the Table record");
            }
            catch(Exception ex)
            {
                driver._extentReport.SetTestStatusFail(strRowText + " not Clicked successfully on the Table record");
                throw (ex); 
            }
       }
    }
}

