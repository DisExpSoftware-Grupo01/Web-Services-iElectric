Feature: AnnouncementServiceTests
As a Developer
I Want ot add new Announcement through API
So that it becomes available for applicantions.

@mytag
Scenario: Add Announcement
	Given The Endpoint https://localhost:44346/api/v1/announcement is available
	When A Announcement Request is sent
	  | Title               | Description                         | Content                             | UrlToImage | TypeOfAnnouncement | Visible |
	  | New Appliance Brand | New appliance brand available today | New appliance brand available today | image.png  | 1                  | true    |
	Then A Response with Status 200 is received for the Announcement