Feature: PlanServiceTests
As a Developer
I Want ot add new Plan through API
So that it becomes available for applications.

@mytag
Scenario: Add Plan
	Given The Endpoint https://localhost:44346/api/v1/plans is available
	When A Plan Request is sent
	  | Name     | Price | 
	  | New Plan | 20    | 
	Then A Response with Status 200 is received for the plan