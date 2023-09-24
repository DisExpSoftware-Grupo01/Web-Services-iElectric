using web_services_ielectric.Reports.Domain.Models;

namespace web_services_ielectric.Reports.Domain.Repositories;

public interface IReportRepository
{
    Task<IEnumerable<Report>> ListAsync();
    Task AddAsync(Report report);
    Task<Report> FindByIdAsync(long id);
    void Update(Report report);
    void Remove(Report report);
}