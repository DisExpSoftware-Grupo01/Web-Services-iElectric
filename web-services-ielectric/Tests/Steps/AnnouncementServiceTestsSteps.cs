using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using SpecFlow.Internal.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using web_services_ielectric.Announcements.Resources;
using Xunit;

namespace web_services_ielectric.Tests.Steps;

[Binding]
public sealed class AnnouncementServiceTestsSteps : WebApplicationFactory<Program>
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    private Uri _baseUri;
    private ConfiguredTaskAwaitable<HttpResponseMessage> Response
    {
        get; set;
    }

    public AnnouncementServiceTestsSteps(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Given(@"The Endpoint https://localhost:(.*)/api/v(.*)/announcement is available")]
    public void GivenTheEndpointHttpsLocalhostApiVAnnouncementIsAvailable(int port, int version)
    {
        _baseUri = new Uri($"https://localhost:{port}/api/v{version}/announcement");
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = _baseUri });
    }
        
    [When(@"A Announcement Request is sent")]
    public void WhenAAnnouncementRequestIsSent(Table saveAnnouncementResource)
    {
        var resource = saveAnnouncementResource.CreateSet<SaveAnnouncementResource>().First();
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, "application/json");
        Response = _client.PostAsync(_baseUri, content).ConfigureAwait(false);
    }
        
    [Then(@"A Response with Status (.*) is received for the Announcement")]
    public void ThenAResponseWithStatusIsReceivedForTheAnnouncement(int expectedStatus)
    {
        HttpStatusCode statusCode = (HttpStatusCode)expectedStatus;
        Assert.Equal(statusCode.ToString(), Response.GetAwaiter().GetResult().StatusCode.ToString());
    }
}