using web_services_ielectric.ApplianceModels.Domain.Models;

namespace web_services_ielectric.ApplianceModels.Domain.Repositories;

public interface IApplianceModelRepository
{
    Task<IEnumerable<ApplianceModel>> ListAsync();
    Task AddAsync(ApplianceModel appliance);
    Task<ApplianceModel> FindByIdAsync(long id);
    Task<IEnumerable<ApplianceModel>> FindByApplianceBrandId(long applianceBrandId);
    void Update(ApplianceModel appliance);
    void Remove(ApplianceModel appliance);
}