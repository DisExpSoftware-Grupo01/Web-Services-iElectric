using web_services_ielectric.Appliances.Domain.Models;
using web_services_ielectric.Appliances.Domain.Services.Communication;

namespace web_services_ielectric.Appliances.Domain.Services;

public interface IApplianceService
{
    Task<IEnumerable<Appliance>> ListByClientIdAsync(long clientId);
    Task<ApplianceResponse> GetByIdAsync(long id);
    Task<ApplianceResponse> SaveAsync(Appliance appliance);
    Task<ApplianceResponse> UpdateAsync(long id, Appliance appliance);
    Task<ApplianceResponse> DeleteAsync(long id);
}