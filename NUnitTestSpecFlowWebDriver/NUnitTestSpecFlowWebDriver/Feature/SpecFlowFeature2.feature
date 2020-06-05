Feature: SpecFlowFeature2
	Validate funcionality of cart

@mytag
Scenario: Validate button add to cart
	Given the user click the button Detalles
	And the user click the button Añadir al Carrito
	When the user goes click the button cart
	Then the game is in the cart details
