using Docker.DotNet.Models;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.PageObjects;
using Ticketing_System.Data;
using Ticketing_System.Services;

namespace Tests.StepDefinitions
{
    [Binding]
    public sealed class TicketingSystemUIStepDefinitions
    {
        private Concert _testConcert = new Concert();
        private Performance _testPerformance = new Performance();
        private Reservation _testreservation = new Reservation();
        private List<string> _namesOfConcerts = new List<string>();

        private ChromeDriver _driver;
        private CreateConcertPage _createConcertPage;
        private DetailsPage _detailPage;
        private FutureListPage _futureListPage;
        private CompleteListPage _completeListPage;
        private ReservationPage _reservationPage;
        private ConfirmationPage _confirmationPage;

        [BeforeScenario]
        public void Startup()
        {
            _driver = new ChromeDriver();
            _createConcertPage = new CreateConcertPage(_driver);
            _detailPage = new DetailsPage(_driver);
            _futureListPage = new FutureListPage(_driver);
            _completeListPage = new CompleteListPage(_driver);
            _reservationPage = new ReservationPage(_driver);
            _confirmationPage = new ConfirmationPage(_driver);
        }

        [AfterScenario]
        public void TearDown()
        {
            _detailPage.RemoveTestConcerts();
            _driver.Quit();
        }

        #region Given
        [Given("De baliemedewerker bevindt zich op de pagina om een nieuw concert aan te maken")]
        public void GivenPaginaVoorConcertAanmaken()
        {
            _createConcertPage.NavigateToCreateConcertPage();
        }
        [Given("De baliemedewerker bevindt zich op de pagina met de lijst van alle concerten")]
        public void GivenDelStaatOpConcertenLijstPagina()
        {
            _completeListPage.NavigateToListPage();
        }
        [Given("De baliemedewerker bevindt zich op de pagina met de lijst van upcoming concerten")]
        public void GivenStaatOpUpcomingConcerts()
        {
            _futureListPage.NavigateToFutureListPage();
        }
        [Given("De baliemedewerker heeft op de Book Now knop voor een performantie gedrukt")]
        public void GivenJeBentOpReservatiePagina()
        {
            _detailPage.ClickBookPerformance();
        }
        [Given("De baliemedewerker bevindt zich op de detailspagina van dat concert")]
        public void GivenJeBentOpDetailsPagina()
        {
            _detailPage.NavigateToDetailsPage();
        }
        [Given("Er bestaat een concert")]
        public void GivenEenConcertBestaat()
        {
            _createConcertPage.CreateConcert();
            _detailPage.WaitForDetailsPage();
        }

        [Given("Het concert heeft een performatie in de toekomst")]
        public void GivenEenConcertBestaatMetUpcomingperf()
        {
            _detailPage.CreatePerformance();
        }
        [Given("Er bestaan meerdere concerten")]
        public void GivenMeerdereConcertenBestaan()
        {
            _namesOfConcerts = new List<string> { "The Weeknd", "Drake", "Swift" };

            foreach(string name in _namesOfConcerts)
            {
                _createConcertPage.CreateConcert(name);
                _detailPage.WaitForDetailsPage();
            }
        }
        [Given("Het concert heeft een performantie in de toekomst")]
        public void GivenPerformantieInDeToekomst()
        {
            _detailPage.CreatePerformance();
        }
        #endregion

        #region When 
        [When("De baliemedewerker de naam van het concert invoert als (.*)")]
        public void WhenInvoerenConcertNaam(string concertNaam)
        {
            _testConcert.Name = concertNaam;
            _createConcertPage.WriteName(concertNaam);
        }

        [When("De baliemedewerker voert een beschrijving in voor het concert als (.*)")]
        public void WhenInvoerenConcertBeschrijving(string concertBeschrijving)
        {
            _testConcert.Description = concertBeschrijving;
            _createConcertPage.WriteDescription(concertBeschrijving);
        }

        [When("De baliemedewerker voert de prijs voor volwassenen in als (.*)")]
        public void WhenInvoerenPrijsVolwassenen(decimal prijsVolwassenen)
        {
            _testConcert.AdultPrice = prijsVolwassenen;
            _createConcertPage.WritePriceAdults(prijsVolwassenen.ToString());
        }

        [When("De baliemedewerker voert de prijs voor kinderen in als (.*)")]
        public void WhenInvoerenPrijsKinderen(int prijsKinderen)
        {
            _testConcert.ChildPrice = prijsKinderen;
            _createConcertPage.WritePriceChildren(prijsKinderen.ToString());
        }
        [When("De baliemedewerker drukt op de knop Create Concert")]
        public void WhenDrukOpCreateConcert()
        {
            _createConcertPage.ClickCreate();
        }
        [When("De baliemedewerker drukt op de knop Delete om het concert te verwijderen")]
        public void WhenDruktOpDeleteKnop()
        {
            _detailPage.ClickDeleteConcert();
        }
        [When(@"De baliemedewerker voegt een starttijd toe: dag (\d+), maand (\d+) jaar (\d+), uur (\d+) en minuut (\d+)")]
        public void WhenVoegStarttijdToe(int day, int month, int year, int hour, int minute)
        {
            DateTime starttijd = new DateTime(year, month, day, hour, minute, 0);
            _testPerformance.StartTime = starttijd;
            _detailPage.WriteStarttime(starttijd);
        }

        [When("De baliemedewerker selecteert de concertzaal (.*)")]
        public void WhenSelecteerConcertzaal(string concertzaal)
        {
            _testPerformance.ConcertHall = new ConcertHall() { Name= concertzaal};
            _detailPage.WriteConcertHall(concertzaal);
        }
        [When("De baliemedewerker de voornaam (.*) in")]
        public void WhenFirstNameIngevuld(string firstName)
        {
            _reservationPage.WriteFirstName(firstName);
        }
        [When("De baliemedewerker de familienaam (.*) in")]
        public void WhenLastNameIngevuld(string lastName)
        {
            _reservationPage.WriteLastName(lastName);
        }
        [When("De baliemedewerker de zet net aantal volwassen op (.*)")]
        public void WhenNumberOfAdultsIngevuld(int numberOfAdults)
        {
            _reservationPage.WriteNumberOfAdults(numberOfAdults);
        }
        [When("De baliemedewerker de zet net aantal kinderen op (.*)")]
        public void WhenNumberOfChildrenIngevuld(int numberOfChildren)
        {
            _reservationPage.WriteNumberOfChildren(numberOfChildren);
        }
        [When("De baliemedewerker drukt op de knop Confirm")]
        public void WhenTicketBoekenGedrukt()
        {
            _reservationPage.ClickCreate();
        }
        [When("De baliemedewerker drukt op de knop Create Performantie")]
        public void WhenDrukopCreatePerf()
        {
            _detailPage.ClickCreatePerformace();
        }
        [When("De baliemedewerker drukt op de knop Remove om de performance te verwijderen")]
        public void WhenDruktOpKnopRemove()
        {
            _detailPage.ClickRemovePerformance();
        }
        #endregion

        #region Then
        [Then("wordt er een overzicht getoond met de details van het aangemaakte concert")]
        public void ThenOverzichtConcertDetails()
        {
            List<String> concertDetails = _detailPage.GetDetailsConcert();
            _testConcert.Name.Should().Be(concertDetails[0]);
            _testConcert.Description.Should().Be(concertDetails[1]);
            _testConcert.AdultPrice.Should().Be(decimal.Parse(concertDetails[2]));
            _testConcert.ChildPrice.Should().Be(decimal.Parse(concertDetails[3]));
        }
        [Then("Krijgt de baliemedewerker een overzicht van alle concerten")]
        public void ThenBekijktAlleConcerten()
        {
            _completeListPage.FindConcertNames().Should().Contain(_namesOfConcerts);
        }
        [Then("Krijgt de baliemedewerker een overzicht van alle upcoming concerten")]
        public void ThenBekijktAlleKomendeConcerten()
        {
            _futureListPage.FindConcertNames().Should().Contain("The Weeknd");
        }
        [Then("De baliemedewerker bevindt zich op de pagina met de lijst van upcoming concerten")]
        public void ThenUpcomingPage()
        {
            _futureListPage.GetCurrentUrl().Should().Be("http://localhost:7226/Concert/Summary");
        }
        [Then("De baliemedewerker bevindt zich op de detailspagina van dat concert")]
        public void ThenJeBentOpDetailsPagina()
        {
            _futureListPage.GetCurrentUrl().Should().Contain("http://localhost:7226/Concert/Details/");
        }

        [Then("De baliemedewerker komt op de confirmatiepagina terecht")]
        public void ThenHebJeEenTicket()
        {
            _confirmationPage.WaitForCofirmationPage();
            _confirmationPage.GetCurrentUrl().Should().Be("http://localhost:7226/Concert/Confirmation");
        }
        [Then("De performantie is zichtbaar in de lijst van performanties")]
        public void ThenPerformatieVoorConcert()
        {
            List<Tuple<DateTime, String>> performances = _detailPage.FindPerformances();

            // Find if any performance matches the test performance's start time and concert hall
            var isPerformanceMatched = performances.Any(performance =>
                performance.Item1 == _testPerformance.StartTime &&
                performance.Item2 == _testPerformance.ConcertHall.Name);

            // Assert that the performance is found
            isPerformanceMatched.Should().BeTrue("a performance matching the test performance should exist");
        }

        [Then("De performantie is niet meer zichtbaar in de lijst van performanties")]
        public void ThenPerformatieGedeleteVoorConcert()
        {
            List<Tuple<DateTime, String>> performances = _detailPage.FindPerformances();

            // Find if any performance matches the test performance's start time and concert hall
            var isPerformanceMatched = performances.Any(performance =>
                performance.Item1 == _testPerformance.StartTime &&
                performance.Item2 == _testPerformance.ConcertHall.Name);

            // Assert that the performance is found
            isPerformanceMatched.Should().BeFalse("a performance matching the test performance should not exist");
        }
        #endregion
    }
}
