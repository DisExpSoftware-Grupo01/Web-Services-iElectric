using web_services_ielectric.Reports.Domain.Models;
using web_services_ielectric.Reports.Domain.Services.Communication;

namespace web_services_ielectric.Reports.Domain.Services;

public interface IReportService
{
    Task<IEnumerable<Report>> ListAsync();
    Task<ReportResponse> GetByIdAsync(long id);
    Task<ReportResponse> SaveAsync(Report report);
    Task<ReportResponse> UpdateAsync(long id, Report report);
    Task<ReportResponse> DeleteAsync(long id);
}