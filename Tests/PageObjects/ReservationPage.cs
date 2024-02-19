using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.PageObjects
{
    internal class ReservationPage : AbstractPage
    {
        public ReservationPage(IWebDriver driver) : base(driver)
        {
        }

        public ReservationPage NavigateToReservationPage()
        {
            _driver.Navigate().GoToUrl($"http://localhost:7226/Concert/Reservation/{_currentId}");
            return this;
        }

        public void WriteFirstName(string firstname)
        {
            _driver.FindElement(By.Id("InputModel_FirstName")).SendKeys(firstname);
        }

        public void WriteLastName(string lastname)
        {
            _driver.FindElement(By.Id("InputModel_LastName")).SendKeys(lastname);
        }

        public void WriteNumberOfAdults(int adults)
        {
            _driver.FindElement(By.Id("InputModel_NumberOfAdults")).SendKeys(adults.ToString());
        }

        public void WriteNumberOfChildren(int children)
        {
            _driver.FindElement(By.Id("InputModel_NumberOfChildren")).SendKeys(children.ToString());
        }

        public void ClickCreate()
        {
            _driver.FindElement(By.ClassName("btn-primary")).Click();
        }

        public void WaitForReservationPage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));

            wait.Until(driver => driver.Url.StartsWith("http://localhost:7226/Concert/Reservation"));

        }
    }
}
