using web_services_ielectric.Shared.Domain.Models;

namespace web_services_ielectric.Clients.Domain.Models;

public class Client : Person
{
    public long PlanId { get; set; }
}