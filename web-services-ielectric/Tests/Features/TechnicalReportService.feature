Feature: Add Report
	As a user
	I want to add a report
	So that I can create new reports for maintenance tasks


@mytag
Scenario: Successfully add a report
	Given the user wants to add a report
	When the user provides the following report information:
	  | Observation      | Diagnosis      | Repair Description | Date       | Image Path      |
	  | Test Observation | Test Diagnosis | Test Repair        | 2023-11-07 | /test/image.png |
	And the user submits the report
	Then the report is added successfully
	And a successful response is returned