using web_services_ielectric.Clients.Domain.Models;
using web_services_ielectric.Shared.Domain.Services.Communication;

namespace web_services_ielectric.Clients.Domain.Services.Communication.Communication;

public class ClientResponse : BaseResponse<Client>
{
    public ClientResponse(string message) : base(message) { }
    public ClientResponse(Client client) : base(client) { }
}