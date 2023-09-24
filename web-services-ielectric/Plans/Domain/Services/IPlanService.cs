using web_services_ielectric.Plans.Domain.Models;
using web_services_ielectric.Plans.Domain.Services.Communication;

namespace web_services_ielectric.Plans.Domain.Services;

public interface IPlanService
{
    Task<IEnumerable<Plan>> ListAsync();
    Task<PlanResponse> GetByIdAsync(long id);
    Task<PlanResponse> SaveAsync(Plan plan);
    Task<PlanResponse> UpdateAsync(long id, Plan plan);
    Task<PlanResponse> DeleteAsync(long id);
}