Feature: Validate functionality on login page of Applicatoin

@mytag
Scenario: Validate button login
	Given Open the Chrome and launch the application	
	When Enter the Email and Password
	Then the result should be the user logged
