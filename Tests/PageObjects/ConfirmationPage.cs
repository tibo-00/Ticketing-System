using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.PageObjects
{
    internal class ConfirmationPage : AbstractPage
    {
        public ConfirmationPage(IWebDriver driver) : base(driver)
        {
        }

        public void WaitForCofirmationPage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));

            wait.Until(driver => driver.Url.StartsWith("http://localhost:7226/Concert/Confirmation"));

        }
    }
}
