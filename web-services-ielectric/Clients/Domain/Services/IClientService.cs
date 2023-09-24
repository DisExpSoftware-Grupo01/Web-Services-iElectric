using web_services_ielectric.Clients.Domain.Models;
using web_services_ielectric.Clients.Domain.Services.Communication.Communication;

namespace web_services_ielectric.Clients.Domain.Services.Communication;

public interface IClientService
{
    Task<IEnumerable<Client>> ListAsync();
    Task<ClientResponse> GetByIdAsync(long id);
    Task<ClientResponse> GetByUserIdAsync(long userId);
    Task<ClientResponse> SaveAsync(Client client);
    Task<ClientResponse> UpdateAsync(long id, Client client);
    Task<ClientResponse> DeleteAsync(long id);
    Task<ClientResponse> UpdateUserPlanAsync(long clientId, long planId);
}