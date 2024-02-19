using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.PageObjects
{
    internal class FutureListPage : AbstractPage
    {
        public FutureListPage(IWebDriver driver) : base(driver)
        {
        }


        public FutureListPage NavigateToFutureListPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:7226/Concert/Summary");
            return this;
        }

        public List<String> FindConcertNames()
        {
            WaitForFuturelistPage();
            List<string> concertNames = new List<string>();

            // Find all the <td> elements in the table that contain concert names
            var nameElements = _driver.FindElements(By.XPath("//table[@class='table']//tr/td[1]"));

            foreach (var element in nameElements)
            {
                // Add the text of each element to the list
                concertNames.Add(element.Text);
            }

            return concertNames;
        }

        public void WaitForFuturelistPage(Boolean isNewConcert = true)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));

            wait.Until(driver => driver.Url.StartsWith("http://localhost:7226/Concert/Summary"));
        }
    }
}
