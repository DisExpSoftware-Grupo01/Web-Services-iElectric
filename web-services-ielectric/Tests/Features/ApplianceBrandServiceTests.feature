Feature: ApplianceBrandServiceTests
As a Developer 
I want to add new ApplianceBrand through API
So that it becomes avaliable for applications.

@applianceBrand-adding
	Scenario: Add ApplianceBrand
		Given The Endpoint https://localhost:44346/api/v1/applianceBrand is available
		When A ApplianceBrand Request is sent
		  | Name    | ImgPath |
		  | Samsung | img1    |
		Then A Response with Status 200 is received for the applianceBrand