Feature: Add spare request
	As a user
	I Want to add a spare request
	So that I can request necessary spare parts.

@mytag
Scenario: Successfully add a spare request
	Given the user wants to add a spare request
	When the user provides the following request information:
	  | Description           | Date       | Image Path       | Technician ID |
	  | Descripción de prueba | 2023-11-07 | /ruta/imagen.png | 123           |
	And the user submits the spare request
	Then the spare request is added successfully
	And a successful response is returned