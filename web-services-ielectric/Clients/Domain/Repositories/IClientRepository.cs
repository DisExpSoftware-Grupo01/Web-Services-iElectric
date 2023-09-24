using web_services_ielectric.Clients.Domain.Models;

namespace web_services_ielectric.Clients.Domain.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> ListAsync();
    Task AddAsync(Client client);
    Task<Client> FindByIdAsync(long id);
    Task<Client> FindByUserIdAsync(long userId);
    void Update(Client client);
    void Remove(Client client);
}