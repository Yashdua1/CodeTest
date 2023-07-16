Feature: Search

A short summary of the feature

@Smoke
Scenario: Search with keyword
	Given HomePage is "http://www.amazon.in"
	When Search with keyword "mobile"
	Then Correct results are shown


 @Regression
Scenario: Search with multiple keywords
	Given HomePage is "http://www.amazon.in"
	When Search with keyword "mobile"
	
