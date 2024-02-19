using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.PageObjects
{
    public class AbstractPage
    {
        protected IWebDriver _driver;
        protected int _currentId;

        public AbstractPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public AbstractPage NavigateToHomePage()
        {
            _driver.Navigate().GoToUrl("http://localhost:7226/");
            return this;
        }
        public String GetCurrentUrl()
        {
            return _driver.Url;
        }
        public void CloseBrowser()
        {
            _driver.Quit();
        }
    }
}
