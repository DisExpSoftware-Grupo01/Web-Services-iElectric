using web_services_ielectric.ApplianceBrands.Domain.Models;

namespace web_services_ielectric.ApplianceBrands.Domain.Repositories;

public interface IApplianceBrandRepository
{
    Task<IEnumerable<ApplianceBrand>> ListAsync();
    Task AddAsync(ApplianceBrand applianceBrand);
    Task<ApplianceBrand> FindByIdAsync(long id);
    void Update(ApplianceBrand applianceBrand);
    void Remove(ApplianceBrand applianceBrand);
}