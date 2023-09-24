using web_services_ielectric.Appliances.Domain.Models;

namespace web_services_ielectric.Appliances.Domain.Repositories;

public interface IApplianceRepository
{
    Task AddAsync(Appliance appliance);
    Task<Appliance> FindByIdAsync(long id);
    Task<IEnumerable<Appliance>> FindByClientIdAsync(long clientId);
    void Update(Appliance appliance);
    void Remove(Appliance appliance);
}