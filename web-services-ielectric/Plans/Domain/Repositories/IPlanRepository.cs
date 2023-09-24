using web_services_ielectric.Plans.Domain.Models;

namespace web_services_ielectric.Plans.Domain.Repositories;

public interface IPlanRepository
{
    Task<IEnumerable<Plan>> ListAsync();
    Task<Plan> FindById(long planId);
    Task AddAsync(Plan plan);
    void Update(Plan plan);
    void Remove(Plan plan);
}