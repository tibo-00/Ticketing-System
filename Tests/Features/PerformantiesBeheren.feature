Feature: PerformantiesBeheren

A short summary of the feature

@CreatePerformanceUI
Scenario: Performantie toevoegen na het aanmaken van een concert
	Given Er bestaat een concert
	And De baliemedewerker bevindt zich op de detailspagina van dat concert
	When De baliemedewerker voegt een starttijd toe: dag 11, maand 11 jaar 2025, uur 20 en minuut 00
	And De baliemedewerker selecteert de concertzaal Mozart
	And De baliemedewerker drukt op de knop Create Performantie
	Then De performantie is zichtbaar in de lijst van performanties

@DeletePerformanceUI
Scenario: Performantie verwijderen
	Given Er bestaat een concert
	And Het concert heeft een performantie in de toekomst
	And De baliemedewerker bevindt zich op de detailspagina van dat concert
	When De baliemedewerker drukt op de knop Remove om de performance te verwijderen
    Then De performantie is niet meer zichtbaar in de lijst van performanties
