Feature: Test
	I using this feature to demo

@mytag
Scenario: Apply BDD Framework
	Given I navigate to the log in page
	When I input the "Username" and "Password"
		| Username                  | Password |
		| truyentranhtuan@gmail.com | 123456   |
	And I click on the Sign in button
	Then The Page is signed successfully with Title "My account - My Store"
	When I input "T-Shirts" to the textbox search
	Then The Page is search with the value and the title is displayed "Search - My Store"
	And I log out