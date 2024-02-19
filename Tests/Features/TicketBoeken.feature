Feature: TicketBoeken

A short summary of the feature

@BookTicket
Scenario: Ticket Boeken voor concert
	Given Er bestaat een concert 
	And Het concert heeft een performantie in de toekomst
	And De baliemedewerker heeft op de Book Now knop voor een performantie gedrukt
	When De baliemedewerker de voornaam Matteo in
	And De baliemedewerker de familienaam Rinalducci in
	And De baliemedewerker de zet net aantal volwassen op 2
	And De baliemedewerker de zet net aantal kinderen op 1
	And De baliemedewerker drukt op de knop Confirm
	Then De baliemedewerker komt op de confirmatiepagina terecht
