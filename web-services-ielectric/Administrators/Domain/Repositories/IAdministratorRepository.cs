using web_services_ielectric.Administrators.Domain.Models;

namespace web_services_ielectric.Administrators.Domain.Repositories;

public interface IAdministratorRepository
{
    Task<IEnumerable<Administrator>> ListAsync();
    Task AddAsync(Administrator administrator);
    Task<Administrator> FindByIdAsync(long id);
    Task<Administrator> FindByUserIdAsync(long userId);
    void Update(Administrator administrator);
    void Remove(Administrator administrator);
}