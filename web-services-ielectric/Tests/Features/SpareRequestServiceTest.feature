Feature: Add spare request
	As a user
	I Want to add a spare request
	So that I can request necessary spare parts.

@mytag
Scenario: Successfully add a spare request
	Given the user wants to add a spare request
	When the user provides the following request information:
	  | Description           | Date       | Image Path       | Technician ID |
	  | Descripción de prueba | 2023-11-07 | url.png | 123           |
	And the user submits the spare request
	Then the spare request is added successfully
	And a successful response is returned
	
@mytag
Scenario: Failure to add a spare request
	Given the user wants to add a spare request
	When the user provides invalid information for the request:
	  | Description           | Date       | Image Path          | Technician ID |
	  |                       | 2023-11-07 | url.png | 123           |
	And the user submits the spare request
	Then the spare request is not added
	And an error message containing "An error occurred while saving the spare request" is returned
	
@mytag
Scenario: Successfully update an existing spare request
	Given there is a spare request with ID 123
	And the user wants to update the spare request
	When the user provides the following information for the updated request:
	  | Description         | Date       | Image Path        | Technician ID |
	  | Updated Description | 2023-11-07 | url.png | 456           |
	And the user submits the spare request for update
	Then the spare request is successfully updated
	And a successful response is returned

@mytag
Scenario: Failure to update a non-existent spare request
	Given there is no spare request with ID 789
	And the user wants to update the spare request
	When the user provides the following information for the updated request:
	  | Description         | Date       | Image Path        | Technician ID |
	  | Updated Description | 2023-11-07 | url.png | 456           |
	And the user submits the spare request for update
	Then the spare request is not updated
	And an error message containing "Spare Request not found" is returned
