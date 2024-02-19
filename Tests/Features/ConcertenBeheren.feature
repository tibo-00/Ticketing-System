Feature: ConcertenBeheren

A short summary of the feature

@CreateConcertUI
Scenario: Een nieuwe concert aanmaken
	Given De baliemedewerker bevindt zich op de pagina om een nieuw concert aan te maken
	When De baliemedewerker de naam van het concert invoert als The Weeknd
	And De baliemedewerker voert een beschrijving in voor het concert als Buy tickets for The Weeknd concerts
	And De baliemedewerker voert de prijs voor volwassenen in als 50
	And De baliemedewerker voert de prijs voor kinderen in als 45
	And De baliemedewerker drukt op de knop Create Concert
	Then De baliemedewerker bevindt zich op de detailspagina van dat concert
	And wordt er een overzicht getoond met de details van het aangemaakte concert

@DeleteConcertUI
Scenario: Een concert verwijderen
	Given Er bestaat een concert
	And De baliemedewerker bevindt zich op de detailspagina van dat concert
	When De baliemedewerker drukt op de knop Delete om het concert te verwijderen
	Then De baliemedewerker bevindt zich op de pagina met de lijst van upcoming concerten

	
