using web_services_ielectric.ApplianceBrands.Domain.Models;
using web_services_ielectric.ApplianceBrands.Domain.Services.Communication;

namespace web_services_ielectric.ApplianceBrands.Domain.Services;

public interface IApplianceBrandService
{
    Task<IEnumerable<ApplianceBrand>> ListAsync();
    Task<ApplianceBrandResponse> GetByIdAsync(long id);
    Task<ApplianceBrandResponse> SaveAsync(ApplianceBrand applianceBrand);
    Task<ApplianceBrandResponse> UpdateAsync(long id, ApplianceBrand applianceBrand);
    Task<ApplianceBrandResponse> DeleteAsync(long id);
}