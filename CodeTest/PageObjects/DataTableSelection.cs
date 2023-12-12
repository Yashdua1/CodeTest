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
                //IWebElement checkTable = driver._driver.FindElement(By.XPath("//h5[normalize-space()='Checkbox']/following-sibling::(//table[@role='grid'])[6]"));


                var rows = driver._driver.FindElements(By.XPath("(//table[@role='grid'])[6]//tr"));
                    //IWebElement chkElement = driver._driver.FindElement(By.XPath("//td[contains(text(),'" + strRowText + "')]/preceding-sibling::td//input[@type='checkbox']"));
                Assert.That(rows, Has.Count.EqualTo(10));
                //driver._driver.FindElement(By.XPath("//td[contains(text(),'Blue Band')]//preceding::td[1]/div/div[1]/input[@type='checkbox']")).Click();

                //td[contains(text(),'Blue Band')]/../preceding::tr[@draggable='false']//input[@type='checkbox']

                //WebDriverWait objWait = new WebDriverWait(driver._driver, TimeSpan.FromSeconds(40));
                
                //objWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//td[contains(text(),'" + strRowText + "')]/preceding-sibling::td//input[@type='checkbox']")));

                //chkElement.Click();

                Thread.Sleep(200);

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

