Feature: Exercise

Get Next Birthday 

@Smoke
Scenario: Get next birthday for valid date
	Given I want to "get" resource /GetNextBirthday with DOB "2001-12-31", api execution returns NextBirthday "2023-12-31"
		  
		

@Smoke @Regression
Scenario Outline: Get next birthday date with DOB in leap year
	Given I want to "get" resource /GetNextBirthday with <DOB>, api execution returns <NextBirthday>

	Examples:
	| DOB       | GetNextBirthday |
	| 2024-02-29 | 2025-03-01      |
	| 2024-02-28 | 2025-02-28      |
	| 2028-02-29 | 2029-03-01      |


@Smoke @Regression
Scenario Outline: Get next birthday with valid year, month and day
	Given I want to "get" resource /GetNextBirthday with <DOB>, api execution returns <NextBirthday>
	
	Examples: 
	| DOB       | GetNextBirthday |
	| 2003-12-31 | 2023-12-31      |
	| 1999-12-31 | 2023-12-31      |
	| 2000-01-31 | 2024-01-31      |
	| 2001-01-01 | 2024-01-01      |
	| 2023-08-02 | 2024-08-02      |

@Regression
Scenario Outline: Get next birthday with Invalid year, month and day
	Given I want to "get" resource /GetNextBirthday with <DOB>, api execution returns status <StatusCode>
		
	Examples: 
	| DOB           | StatusCode |
	| 12-02-2003     | 404        |
	| 30-01-2003     | 404        |
	| 2001-01-01     | 404        |
	| abc_2001-12-31 | 404        |
	| 2001-01-00     | 404        |
	| 2001-01-32     | 404        |
	| 2001-13-31     | 404        |
	| 2023-12-31     | 404        |

@Regression
Scenario: Get next birthday with Invalid method
	Given I want to "post" resource /GetNextBirthday with "2001-12-31", API execution returns status "400"
	
	
	


