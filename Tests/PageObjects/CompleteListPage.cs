using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.PageObjects
{
    internal class CompleteListPage : AbstractPage
    {
        public CompleteListPage(IWebDriver driver) : base(driver)
        {
        }

        public CompleteListPage NavigateToListPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:7226/Concert/CompleteSummary");
            return this;
        }
        
        public List<String> FindConcertNames()
        {
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
    }
}
