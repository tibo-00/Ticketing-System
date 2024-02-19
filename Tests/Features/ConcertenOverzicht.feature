Feature: ConcertenOverzicht

A short summary of the feature

@GetConcertOverview
Scenario: Overzicht van alle concerten bekijken 
	Given Er bestaan meerdere concerten
	And De baliemedewerker bevindt zich op de pagina met de lijst van alle concerten
	Then Krijgt de baliemedewerker een overzicht van alle concerten

@GetConcertOverview
Scenario: Overzicht van alle upcoming concerten bekijken 
	Given Er bestaat een concert
	And Het concert heeft een performatie in de toekomst
	And De baliemedewerker bevindt zich op de pagina met de lijst van upcoming concerten
	Then Krijgt de baliemedewerker een overzicht van alle upcoming concerten