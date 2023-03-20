Feature: Home Appliances Cost To Use

As a cost-conscious consumer
I want to compare the cost of using different home electrical appliances
So that I can make informed decisions about my energy consumption. So the
feature includes a comparison of the cost of using electrical appliances

Background: 
	Given I navigate to electrical appliances cost compare website

@Smoke
Scenario Outline: Get cost of using electrical appliances for England
	When  I am a resident from '<country>'
	And   I add the list of appliances and its average usage and the average rate is '<average rate>' for '<duration>'
	| Appliances       | Hours | Minutes |
	| Dishwasher       | 2     | 30      |
	| Iron             | 1     | 45      |
	| Slow cooker      | 1     | 10      |
	| Immersion heater | 2     | 20      |
	| Vacuum cleaner   | 3     | 40      |
	| Broadband router | 23    | 50      |
	| Games console    | 2     | 20      |
	| Towel rail       | 9     | 20      |
	Then I should get the results table with daily, weekly, monthly, and yearly cost
	Examples:
	| country | average rate | duration |
	| England | 34           | day      |

@Smoke
Scenario Outline: Get cost of using electrical appliances for Scotland
	When  I am a resident from '<country>'
	And   I add the list of appliances and its average usage and the average rate is '<average rate>' for '<duration>'
	| Appliances       | Hours | Minutes |
	| Kettle           | 1     | 30      |
	| Microwave        | 1     | 10      |
	| Electric blanket | 6     | 10      |
	| Toaster          | 1     | 15      |
	| Vacuum cleaner   | 3     | 15      |
	| Games console    | 5     | 15      |
	| Dishwasher       | 2     | 30      |
	| Towel rail       | 1     | 30      |
	| Fan heater       | 7     | 30      |
	| Hairdryer        | 0     | 10      |
	Then I should get the results table with daily, weekly, monthly, and yearly cost
	Examples:
	| country  | average rate | duration |
	| Scotland | 67           | day      |

@Smoke
Scenario Outline: Get cost of using electrical appliances for Wales
	When  I am a resident from '<country>'
	And   I add the list of appliances and its average usage and the average rate is '<average rate>' for '<duration>'
	| Appliances       | Hours | Minutes |
	| Dishwasher       | 2     | 30      |
	| Iron             | 1     | 45      |
	| Slow cooker      | 1     | 10      |
	| Immersion heater | 2     | 20      |
	| Vacuum cleaner   | 3     | 40      |
	Then I should get the results table with daily, weekly, monthly, and yearly cost
	Examples:
	| country | average rate | duration |
	| Wales   | 67           | day      |

@Smoke
Scenario: Get cost of using electrical appliances for Northern Ireland
	When  I am a resident from '<country>'
	Then I should get the results message as '<error message>'
	Examples:
	| country          | error message                                             |
	| Northern Ireland | The advice on this website doesn’t cover Northern Ireland |

