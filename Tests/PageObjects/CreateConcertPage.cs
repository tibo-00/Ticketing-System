using Docker.DotNet.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tests.PageObjects
{
    internal class CreateConcertPage : AbstractPage
    {
        public CreateConcertPage(IWebDriver driver) : base(driver)
        {
        }

        public AbstractPage NavigateToCreateConcertPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:7226/Concert/Create");
            return this;
        }

        public void WriteName(string name)
        {
            _driver.FindElement(By.Id("InputModel_Name")).SendKeys(name);
        }

        public void WriteDescription(string description)
        {
            _driver.FindElement(By.Id("InputModel_Description")).SendKeys(description);
        }

        public void WritePriceAdults(string priceAdults)
        {
            _driver.FindElement(By.Id("InputModel_AdultPrice")).SendKeys(priceAdults);
        }

        public void WritePriceChildren(string priceChildren)
        {
            _driver.FindElement(By.Id("InputModel_ChildPrice")).SendKeys(priceChildren);
        }

        public void ClickCreate()
        {
            _driver.FindElement(By.ClassName("btn-primary")).Click();
        }

        public void CreateConcert(String name = "The Weeknd")
        {
            this.NavigateToCreateConcertPage();
            WriteName(name);
            WriteDescription("Buy tickets for The Weeknd concerts");
            WritePriceAdults("44");
            WritePriceChildren("12");
            ClickCreate();
        }
    }
}
