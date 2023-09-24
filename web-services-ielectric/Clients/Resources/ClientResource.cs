using web_services_ielectric.Shared.Resources;

namespace web_services_ielectric.Clients.Resources;

public class ClientResource : PersonResource
{
    public long PlanId { get; set; }
}