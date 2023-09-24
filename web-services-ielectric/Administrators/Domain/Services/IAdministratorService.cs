using web_services_ielectric.Administrators.Domain.Models;
using web_services_ielectric.Administrators.Domain.Services.Communication;

namespace web_services_ielectric.Administrators.Domain.Services;

public interface IAdministratorService
{
    Task<IEnumerable<Administrator>> ListAsync();
    Task<AdministratorResponse> GetByIdAsync(long id);
    Task<AdministratorResponse> GetByUserIdAsync(long userId);
    Task<AdministratorResponse> SaveAsync(Administrator administrator);
    Task<AdministratorResponse> UpdateAsync(long id, Administrator administrator);
    Task<AdministratorResponse> DeleteAsync(long id);
}