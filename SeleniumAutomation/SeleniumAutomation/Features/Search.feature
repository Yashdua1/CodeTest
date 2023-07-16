Feature: Search

A short summary of the feature

@tag1
Scenario: Search with keyword
	Given HomePage is "http://www.amazon.in"
	When Search with keyword "mobile"
	Then Correct results are shown


 