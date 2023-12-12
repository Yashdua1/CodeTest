using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Drawing.Imaging;
using CodeTest.Support;

namespace SeleniumAutomation
{
    public class driverManager
    {
        public IWebDriver _driver { get; set;}

        public ExtentReportsHelper _extentReport { get; set; }

        public void screenCaptureAsImage(string scenarioTitle)
        {
            Screenshot ss = ((ITakesScreenshot)_driver).GetScreenshot();
            
            string Runname = scenarioTitle + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss");
            string screenshotfilename = "X:\\screenshots\\" + Runname + ".jpg";
            ss.SaveAsFile(screenshotfilename);
        }
    }

}
