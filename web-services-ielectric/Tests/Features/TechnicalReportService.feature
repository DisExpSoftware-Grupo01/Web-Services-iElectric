Feature: Add Technical Report
	As a user
	I want to add a technical report
	So that I can create new technical reports for maintenance tasks

@mytag
Scenario: Successfully add a technical report
	Given the user wants to add a technical report
	When the user provides the following technical report information:
	  | Observation         | Diagnosis         | Repair Description | Date       | Image Path          |
	  | Test Observation    | Test Diagnosis    | Test Repair        | 2023-11-07 | /test/image.png     |
	And the user submits the technical report
	Then the technical report is added successfully
	And a successful response is returned

@mytag
Scenario: Attempt to add a technical report with missing information
	Given the user wants to add a technical report
	When the user provides incomplete technical report information:
	  | Observation         | Diagnosis         | Repair Description | Date       | Image Path          |
	  | Test Observation    |                   | Test Repair        | 2023-11-07 | /test/image.png     |
	And the user submits the technical report
	Then the technical report is not added
	And an error message is returned indicating missing information

@mytag
Scenario: Attempt to add a technical report with an invalid date
	Given the user wants to add a technical report
	When the user provides technical report information with an invalid date:
	  | Observation         | Diagnosis         | Repair Description | Date            | Image Path          |
	  | Test Observation    | Test Diagnosis    | Test Repair        | Invalid Date    | /test/image.png     |
	And the user submits the technical report
	Then the technical report is not added
	And an error message is returned indicating an invalid date format

@mytag
Scenario: Attempt to add a technical report with a missing image path
	Given the user wants to add a technical report
	When the user provides technical report information with a missing image path:
	  | Observation         | Diagnosis         | Repair Description | Date       | Image Path          |
	  | Test Observation    | Test Diagnosis    | Test Repair        | 2023-11-07 |                    |
	And the user submits the technical report
	Then the technical report is not added
	And an error message is returned indicating a missing image path