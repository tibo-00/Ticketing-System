using FluentAssertions.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing_System.Data;

namespace Tests.PageObjects
{
    internal class DetailsPage : AbstractPage
    {
        private List<int> _testConcertenIds = new List<int>();
        private Performance _testPerformance = new Performance();
        public DetailsPage(IWebDriver driver) : base(driver)
        {
        }

        public DetailsPage NavigateToDetailsPage()
        {
            _driver.Navigate().GoToUrl($"http://localhost:7226/Concert/Details/{_currentId}");
            return this;
        }

        public List<string> GetDetailsConcert()
        {
            List<string> details = new List<string>();

            details.Add(_driver.FindElement(By.XPath("//div[.//strong[text()='Name']]/following-sibling::div")).Text);
            details.Add(_driver.FindElement(By.XPath("//div[.//strong[text()='Description']]/following-sibling::div")).Text);
            String adultPrice = _driver.FindElement(By.XPath("//div[.//strong[text()='Price Adults']]/following-sibling::div")).Text;
            details.Add(adultPrice.Substring(1));
            String childPrice = _driver.FindElement(By.XPath("//div[.//strong[text()='Price Children']]/following-sibling::div")).Text;

            details.Add(childPrice.Substring(1));
            
            return details;
        }


        public void ClickCreatePerformace()
        {
            _driver.FindElement(By.ClassName("btn-primary")).Click();
        }

        public void ClickDeleteConcert()
        {
            _driver.FindElement(By.ClassName("btn-danger")).Click();
        }

        public int FindConcertId()
        {
            this.GetCurrentUrl();
            string url = this.GetCurrentUrl();
            string[] parts = url.Split('/');
            string lastPart = parts[parts.Length - 1];
            int id = int.Parse(lastPart);
            _testConcertenIds.Add(id);
            _currentId = id;
            return id;
        }

        public void RemoveTestConcerts()
        {
            foreach(int Id in _testConcertenIds)
            {
                _driver.Navigate().GoToUrl($"http://localhost:7226/Concert/Details/{Id}");
                this.WaitForDetailsPage(false);
                this.ClickDeleteConcert();
            }
        }


        public void WaitForDetailsPage(Boolean isNewConcert =true)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));

            wait.Until(driver => driver.Url.StartsWith("http://localhost:7226/Concert/Details/"));

            if (isNewConcert)
            {
                FindConcertId();
            }
        }

        public void WriteStarttime(DateTime? starttime = null)
        {
            if(starttime is null)
            {
                starttime = DateTime.Now.AddMonths(2);
            }
            _testPerformance.StartTime = (DateTime)starttime;
            var startTimeInput = _driver.FindElement(By.Id("InputModel_StartTime"));
            startTimeInput.SendKeys(starttime.Value.ToString("MM/dd/yyyy"));
            startTimeInput.SendKeys(Keys.Tab);
            startTimeInput.SendKeys(starttime.Value.ToString("hh:mmtt"));
            
        }

        public void WriteConcertHall(string concerthallName = "Bach")
        {
            var selectElement = new SelectElement(_driver.FindElement(By.Id("InputModel_SelectedConcertHall")));
            _testPerformance.ConcertHall = new ConcertHall() {Name = concerthallName };

            selectElement.SelectByText(concerthallName);
        }

        public void ClickCreate()
        {
            _driver.FindElement(By.ClassName("btn-primary")).Click();
        }

        public List<Tuple<DateTime, String>> FindPerformances()
        {
            List<Tuple<DateTime, String>> performances = new List<Tuple<DateTime, String>>();

            var rows = _driver.FindElements(By.XPath("//div[contains(@class, 'container')]//table//tbody//tr"));
            foreach (var row in rows)
            {
                // Extract the Date, Time, and Hall information from each row
                var date = row.FindElement(By.XPath("./td[1]")).Text;
                var time = row.FindElement(By.XPath("./td[2]")).Text;
                var hall = row.FindElement(By.XPath("./td[3]")).Text;

                // Combine Date and Time and parse into a DateTime
                var dateTimeString = $"{date} {time}";
                var dateTime = DateTime.ParseExact(dateTimeString, "dd MMMM yyyy HH:mm", CultureInfo.InvariantCulture);

                // Add the performance to the list
                performances.Add(new Tuple<DateTime, String>(dateTime, hall));
            }

            return performances;
        }

        public void CreatePerformance()
        {
            NavigateToDetailsPage();
            WriteStarttime();
            WriteConcertHall();
            ClickCreate();
        }

        public void ClickRemovePerformance()
        {
            List<Tuple<DateTime, String>> performances = FindPerformances();

            var isPerformanceMatched = performances.Any(performance =>
                performance.Item1 == _testPerformance.StartTime &&
                performance.Item2 == _testPerformance.ConcertHall.Name);

            // try to remove that performance
            for (int i = 0; i < performances.Count; i++)
            {
                if (performances[i].Item1 == _testPerformance.StartTime &&
                    performances[i].Item2 == _testPerformance.ConcertHall.Name)
                {
                    // Find the "Remove" button in the corresponding row and click it
                    var removeButtons = _driver.FindElements(By.XPath("//div[contains(@class, 'container')][2]//table//tbody//tr//button[contains(@class, 'btn-danger') and contains(text(), 'Remove')]"));
                    if (i < removeButtons.Count)
                    {
                        removeButtons[i].Click();
                        break;
                    }
                }
            }
        }

        public void ClickBookPerformance()
        {
            _driver.FindElement(By.ClassName("btn-success")).Click();
        }
    }
}
